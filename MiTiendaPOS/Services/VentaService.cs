using Microsoft.EntityFrameworkCore;
using MiTiendaPOS.Data;
using MiTiendaPOS.Dtos;
using MiTiendaPOS.Models;

namespace MiTiendaPOS.Services
{
    public class VentaService
    {
        // ----- Catálogos para los ComboBox (tablas de las que depende) ----- 
        public List<Cliente> ListarClientes()
        {
            using var ctx = new MiTiendaPOSContext();
            return ctx.Clientes.AsNoTracking().OrderBy(c => c.Nombre).ToList();
        }

        public List<Usuario> ListarUsuarios()
        {
            using var ctx = new MiTiendaPOSContext();
            return ctx.Usuarios.AsNoTracking().OrderBy(u => u.NombreUsuario).ToList();
        }

        public List<Producto> ListarProductosActivos()
        {
            using var ctx = new MiTiendaPOSContext();
            return ctx.Productos.AsNoTracking()
                      .Where(p => p.Activo)
                      .OrderBy(p => p.Nombre)
                      .ToList();
        }

        // ----- READ: historial (maestro + cliente) ----- 
        public List<Venta> ListarVentas()
        {
            using var ctx = new MiTiendaPOSContext();
            return ctx.Ventas.AsNoTracking()
                      .Include(v => v.Cliente)          // JOIN con Clientes 
                      .OrderByDescending(v => v.Fecha)
                      .ToList();
        }

        // ----- READ: una venta con sus líneas(detalles) y el producto de cada línea ----- 
        public Venta? ObtenerConDetalle(int id)
        {
            using var ctx = new MiTiendaPOSContext();
            return ctx.Ventas.AsNoTracking()
                      .Include(v => v.Detalles)         // JOIN con DetallesVenta 
                          .ThenInclude(d => d.Producto) // JOIN con Productos 
                      .FirstOrDefault(v => v.Id == id);
        }

        // ----- CREATE: encabezado de la venta + detalles + stock en un solo evento SaveChanges ----- 
        public int Registrar(int clienteId, int usuarioId,
                             IEnumerable<LineaVenta> lineas)
        {
            var lista = lineas.ToList();
            if (lista.Count == 0)
                throw new InvalidOperationException("La venta no tiene productos.");

            using var ctx = new MiTiendaPOSContext();
            var venta = new Venta
            {
                Fecha = DateTime.Now,
                ClienteId = clienteId,     // FK hacia tabla Cliente 
                UsuarioId = usuarioId      // FK hacia tabla Usuario 
            };

            venta.Detalles.AddRange(CrearDetalles(ctx, lista));
            venta.Total = venta.Detalles.Sum(d => d.Subtotal);

            ctx.Ventas.Add(venta);   // Added: la venta Y sus detalles 
            ctx.SaveChanges();       // INSERT en Ventas y en Detalles, UPDATE en Productos 
            return venta.Id;         // EF asigna el Id generado por SQL Server 
        }

        // ----- UPDATE: devolver stock, reemplazar líneas, descontar de nuevo ----- 
        public void Actualizar(int ventaId, int clienteId, int usuarioId,
                               IEnumerable<LineaVenta> lineas)
        {
            var lista = lineas.ToList();
            if (lista.Count == 0)
                throw new InvalidOperationException("La venta no tiene productos.");

            using var ctx = new MiTiendaPOSContext();
            Venta venta = ctx.Ventas.Include(v => v.Detalles)
                                    .FirstOrDefault(v => v.Id == ventaId)
                ?? throw new InvalidOperationException("La venta ya no existe.");

            // 1) Borrar las líneas viejas (sin tocar inventario)
            var anteriores = venta.Detalles.ToList();
            ctx.DetallesVenta.RemoveRange(anteriores);

            // 2) Crear las líneas nuevas (valida y descuenta stock otra vez) 
            var nuevos = CrearDetalles(ctx, lista);
            foreach (var d in nuevos) d.VentaId = venta.Id;
            ctx.DetallesVenta.AddRange(nuevos);

            // 3) Actualizar el encabezado 
            venta.ClienteId = clienteId;
            venta.UsuarioId = usuarioId;
            venta.Total = nuevos.Sum(d => d.Subtotal);

            ctx.SaveChanges();   // DELETE/INSERT en Detalles, UPDATE en Ventas y Productos 
        }

        // ----- DELETE: devolver stock y borrar (OJO:Cascade borra los detalles) ----- 
        public void Eliminar(int ventaId)
        {
            using var ctx = new MiTiendaPOSContext();
            Venta venta = ctx.Ventas.Include(v => v.Detalles)
                                    .FirstOrDefault(v => v.Id == ventaId)
                ?? throw new InvalidOperationException("La venta ya no existe.");

            foreach (var d in venta.Detalles)
            {
                Producto? producto = ctx.Productos.Find(d.ProductoId);
                if (producto != null) producto.Stock += d.Cantidad;
            }

            ctx.Ventas.Remove(venta);   // Cascade -> también sus DetalleVenta 
            ctx.SaveChanges();
        }

        // ----- Lógica compartida: validamos stock, lo descontamos y armamos las líneas ----- 
        private static List<DetalleVenta> CrearDetalles(MiTiendaPOSContext ctx,
                                                        List<LineaVenta> lineas)
        {
            var detalles = new List<DetalleVenta>();
            foreach (var linea in lineas)
            {
                Producto producto = ctx.Productos.Find(linea.ProductoId)
                    ?? throw new InvalidOperationException("Producto no encontrado.");

                // No validar ni modificar stock aquí: permitir ventas independientemente del inventario
                detalles.Add(new DetalleVenta
                {
                    ProductoId = producto.Id,
                    Cantidad = linea.Cantidad,
                    PrecioUnitario = producto.PrecioUnitario,   // precio de la BD 
                    Subtotal = linea.Cantidad * producto.PrecioUnitario
                });
            }
            return detalles;
        }

        // Busca un producto por nombre (case-insensitive) y lo crea si no existe.
        // Devuelve el Id del producto existente o creado.
        public int ObtenerOCrearProductoPorNombre(string nombre, int initialStock = 0)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("Nombre de producto inválido.", nameof(nombre));

            using var ctx = new MiTiendaPOSContext();
            var producto = ctx.Productos.FirstOrDefault(p => p.Nombre.ToLower() == nombre.ToLower());
            if (producto != null) return producto.Id;

            // intentar usar la primera categoría existente como predeterminada
            var categoria = ctx.Categorias.FirstOrDefault();
            if (categoria == null)
            {
                // crear una categoría por defecto si no existe ninguna para evitar errores
                categoria = new Categoria { Nombre = "Sin categoría" };
                ctx.Categorias.Add(categoria);
                ctx.SaveChanges();
            }

            producto = new Producto
            {
                Nombre = nombre,
                Activo = true,
                CategoriaId = categoria.Id,
                PrecioUnitario = 0m,
                Stock = Math.Max(0, initialStock)
            };
            ctx.Productos.Add(producto);
            ctx.SaveChanges();
            return producto.Id;
        }

        // Devuelve el stock actual de un producto por su Id.
        public int ObtenerStockProducto(int productoId)
        {
            using var ctx = new MiTiendaPOSContext();
            var producto = ctx.Productos.Find(productoId);
            return producto?.Stock ?? 0;
        }

        public Producto? ObtenerProductoPorId(int id)
        {
            using var ctx = new MiTiendaPOSContext();
            return ctx.Productos.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        // Ajusta el stock de productos cuyo nombre contiene la cadena indicada (case-insensitive).
        public void AjustarStockPorNombre(string nameContains, int nuevoStock)
        {
            if (string.IsNullOrWhiteSpace(nameContains)) return;
            using var ctx = new MiTiendaPOSContext();
            var q = ctx.Productos.Where(p => p.Nombre.ToLower().Contains(nameContains.ToLower()));
            var list = q.ToList();
            if (!list.Any()) return;
            foreach (var p in list)
            {
                p.Stock = nuevoStock;
            }
            ctx.SaveChanges();
        }

        // Crea clientes de prueba en la base si no hay ninguno. Devuelve true si creó datos.
        public bool CrearClientesPruebaSiNoExisten(IEnumerable<Cliente> clientes)
        {
            using var ctx = new MiTiendaPOSContext();
            if (ctx.Clientes.Any()) return false;
            foreach (var c in clientes)
            {
                // asegurarse de no propagar Ids conflictivos: dejar que EF genere el Id
                var newC = new Cliente { Nombre = c.Nombre, Correo = c.Correo, Telefono = c.Telefono };
                ctx.Clientes.Add(newC);
            }
            ctx.SaveChanges();
            return true;
        }

        // Crea productos de prueba en la base si no hay ninguno. Evita duplicados por nombre.
        public bool CrearProductosPruebaSiNoExisten(IEnumerable<Producto> productos)
        {
            using var ctx = new MiTiendaPOSContext();
            if (ctx.Productos.Any()) return false;
            foreach (var p in productos)
            {
                if (ctx.Productos.Any(x => x.Nombre == p.Nombre)) continue;
                var np = new Producto
                {
                    Nombre = p.Nombre,
                    Activo = p.Activo,
                    CategoriaId = p.CategoriaId,
                    PrecioUnitario = p.PrecioUnitario,
                    Stock = p.Stock
                };
                ctx.Productos.Add(np);
            }
            ctx.SaveChanges();
            return true;
        }
    }
}