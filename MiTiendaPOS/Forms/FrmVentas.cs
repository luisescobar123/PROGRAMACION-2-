using System.ComponentModel;
using MiTiendaPOS.Dtos;
using MiTiendaPOS.Models;
using MiTiendaPOS.Services;
using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace MiTiendaPOS.Forms
{
    public partial class FrmVentas : Form
    {
        // Usar los controles definidos por el diseñador (comboBox1/2/3, numericUpDown1, dataGridView1/2, button1-5, label3/6)
        private readonly VentaService _servicio = new();
        private readonly BindingList<LineaVenta> _lineas = new();
        private int _ventaId = 0;   // 0 = venta nueva

        public FrmVentas()
        {
            InitializeComponent();
            // No suscribimos eventos manualmente aquí: el Designer ya los enlaza.
            // Mantener constructor mínimo para evitar duplicar manejadores.
        }

        // Crea una venta de demostración si la base de datos está vacía de ventas
        private void RegistrarVentaDemoSiNoHay()
        {
            try
            {
                var ventas = _servicio.ListarVentas();
                if (ventas != null && ventas.Any()) return;

                var clientes = _servicio.ListarClientes();
                var usuarios = _servicio.ListarUsuarios();
                var productos = _servicio.ListarProductosActivos();

                if (clientes == null || clientes.Count == 0 ||
                    usuarios == null || usuarios.Count == 0 ||
                    productos == null || productos.Count == 0) return;

                int clienteId = clientes.First().Id;
                int usuarioId = usuarios.First().Id;
                var producto = productos.FirstOrDefault(p => p.Stock > 0) ?? productos.First();

                var linea = new LineaVenta
                {
                    ProductoId = producto.Id,
                    Producto = producto.Nombre,
                    Cantidad = 1,
                    PrecioUnitario = producto.PrecioUnitario
                };

                _servicio.Registrar(clienteId, usuarioId, new[] { linea });

                // recargar catálogos e historial para mostrar la venta creada
                CargarCatalogos();
                CargarHistorial();
            }
            catch (Exception ex)
            {
                // No bloquear la carga del formulario por errores en la creación automática
                MessageBox.Show($"No se pudo crear venta de demostración: {ex.Message}\n{ex.GetBaseException().Message}", "MiTiendaPOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCatalogos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando catálogos: {ex.Message}", "MiTiendaPOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // enlazar la lista al DataGridView del diseñador
            dgvDetalle.AutoGenerateColumns = false;
            dgvDetalle.DataSource = _lineas;
            // mapear columnas existentes a las propiedades de LineaVenta
            if (dgvDetalle.Columns.Contains("Producto"))
                dgvDetalle.Columns["Producto"].DataPropertyName = "Producto";
            if (dgvDetalle.Columns.Contains("Cantidad"))
                dgvDetalle.Columns["Cantidad"].DataPropertyName = "Cantidad";
            if (dgvDetalle.Columns.Contains("Stock"))
            {
                dgvDetalle.Columns["Stock"].DataPropertyName = "Stock";
            }
            if (dgvDetalle.Columns.Contains("Precio"))
            {
                dgvDetalle.Columns["Precio"].DataPropertyName = "PrecioUnitario";
                dgvDetalle.Columns["Precio"].DefaultCellStyle.Format = "N2";
                dgvDetalle.Columns["Precio"].HeaderText = "Precio";
            }
            if (dgvDetalle.Columns.Contains("Subtotal"))
            {
                dgvDetalle.Columns["Subtotal"].DataPropertyName = "Subtotal";
                dgvDetalle.Columns["Subtotal"].DefaultCellStyle.Format = "N2";
            }

            try
            {
                CargarHistorial();
                // Si no hay ventas, crear una de ejemplo automáticamente para que aparezca en el historial.
                RegistrarVentaDemoSiNoHay();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando historial: {ex.Message}", "MiTiendaPOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // asignar manejadores a botones del diseñador
            // Los eventos deben estar conectados en el Designer; no volver a añadirlos aquí.

            NuevaVenta();

            // Ajustes de apariencia: colores similares a la referencia
            try
            {
                dgvVentas.BackgroundColor = Color.FromArgb(211, 211, 211); // light gray
                dgvVentas.DefaultCellStyle.BackColor = Color.White; // filas blancas
                dgvDetalle.BackgroundColor = Color.WhiteSmoke;
            }
            catch
            {
                // ignorar si el control no está listo
            }
        }

        // ComboBox enlazados a tablas: se muestra un texto y se guarda el Id (FK)
        private void CargarCatalogos()
        {
            var clientes = _servicio.ListarClientes();
            if (clientes == null || !clientes.Any())
            {
                // Crear clientes de prueba en la base para evitar conflictos FK
                var prueba = new System.Collections.Generic.List<Cliente>
                {
                    new Cliente { Nombre = "Consumidor Final" },
                    new Cliente { Nombre = "María Lopez", Telefono = "7000-0000" },
                    new Cliente { Nombre = "Juan Pérez", Telefono = "7000-0001" },
                    new Cliente { Nombre = "Carlos Gómez", Telefono = "7000-0002" },
                    new Cliente { Nombre = "Ana Torres", Telefono = "7000-0003" }
                };
                try
                {
                    _servicio.CrearClientesPruebaSiNoExisten(prueba);
                }
                catch
                {
                    // ignorar si falla; la UI seguirá con clientes en memoria
                }
                clientes = _servicio.ListarClientes();
            }
            cboCliente.DisplayMember = "Nombre";
            cboCliente.ValueMember = "Id";
            cboCliente.DataSource = clientes;

            var usuarios = _servicio.ListarUsuarios();
            if (usuarios == null || !usuarios.Any())
            {
                usuarios = new System.Collections.Generic.List<Usuario>
                {
                    new Usuario { Id = 1, NombreUsuario = "amin" , Rol = "Administrador"},
                    new Usuario { Id = 2, NombreUsuario = "cajero1" , Rol = "Cajero"}
                };
            }
            cboUsuario.DisplayMember = "NombreUsuario";
            cboUsuario.ValueMember = "Id";
            cboUsuario.DataSource = usuarios;

            var productos = _servicio.ListarProductosActivos();
            if (productos == null || !productos.Any())
            {
                // Si no hay productos en la BD, crear algunos de prueba y recargar
                var pruebaProd = new System.Collections.Generic.List<Producto>
                {
                    new Producto { Nombre = "Soda 600ml", PrecioUnitario = 0.90m, Stock = 48, Activo = true, CategoriaId = 1 },
                    new Producto { Nombre = "Agua Purificada", PrecioUnitario = 0.50m, Stock = 60, Activo = true, CategoriaId = 1 },
                    new Producto { Nombre = "Arroz Blanco", PrecioUnitario = 0.85m, Stock = 40, Activo = true, CategoriaId = 2 },
                    new Producto { Nombre = "Frijol de Seda", PrecioUnitario = 1.10m, Stock = 20, Activo = true, CategoriaId = 2 },
                    new Producto { Nombre = "Detergente MAXI ESPUMA", PrecioUnitario = 3.25m, Stock = 15, Activo = true, CategoriaId = 3 }
                };
                try
                {
                    _servicio.CrearProductosPruebaSiNoExisten(pruebaProd);
                    productos = _servicio.ListarProductosActivos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"No se pudieron crear productos de prueba: {ex.Message}", "MiTiendaPOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // en caso de fallo, asignar lista vacía para evitar null refs
                    productos = new System.Collections.Generic.List<Producto>();
                }
            }
            else
            {
                // Ajustar stock para arroz y soda según petición del usuario
                // Si hay un error queremos verlo en la UI para corregirlo
                try
                {
                    _servicio.AjustarStockPorNombre("arroz", 30);
                    _servicio.AjustarStockPorNombre("soda", 20);
                    // volver a cargar la lista tras ajustar
                    productos = _servicio.ListarProductosActivos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"No se pudo ajustar stock automáticamente: {ex.Message}", "MiTiendaPOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            // No se mantiene stock en las líneas (se eliminó la propiedad Stock). Nada que actualizar aquí.
            cboProducto.DisplayMember = "Nombre";
            cboProducto.ValueMember = "Id";
            cboProducto.DataSource = productos;
        }

        private void CargarHistorial()
        {
            dgvVentas.DataSource = _servicio.ListarVentas()
                .Select(v => new
                {
                    v.Id,
                    Fecha = v.Fecha.ToString("dd/MM/yyyy HH:mm"),
                    Cliente = v.Cliente.Nombre,   // disponible gracias a Include
                    v.Total
                })
                .ToList();
        }

        private void NuevaVenta()
        {
            _ventaId = 0;
            _lineas.Clear();
            nudCantidad.Value = 1;
            lblVenta.Text = "Venta nueva";
            btnEliminar.Enabled = false;
            dgvVentas.ClearSelection();
            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            lblTotal.Text = $"Total: ${_lineas.Sum(l => l.Subtotal):N2}";
        }

        // Agrega una línea en memoria; si el producto ya está, suma la cantidad
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (AgregarLineaDesdeSeleccion())
            {
                nudCantidad.Value = 1;
                ActualizarTotal();
            }
        }

        // Intento de agregar la línea actual (desde cboProducto/nudCantidad). Devuelve true si se agregó.
        private bool AgregarLineaDesdeSeleccion()
        {
            // Buscar siempre por el texto mostrado (más robusto que depender de SelectedItem)
            string nombre = cboProducto.Text?.Trim() ?? string.Empty;
            Producto? producto = null;
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Selecciona o escribe un producto.", "MiTiendaPOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            var lista = _servicio.ListarProductosActivos();
            producto = lista?.FirstOrDefault(x => x.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            // Si no se encuentra el producto en la lista activa, avisar y no crear automáticamente.
            if (producto == null)
            {
                MessageBox.Show($"Producto '{nombre}' no existe. Selecciona un producto de la lista.", "MiTiendaPOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (producto == null) return false;
            int cantidad = (int)nudCantidad.Value;
            LineaVenta? existente = _lineas.FirstOrDefault(l => l.ProductoId == producto.Id);
            if (existente != null)
            {
                existente.Cantidad += cantidad;
                _lineas.ResetBindings();   // refresca la grilla
            }
            else
            {
                _lineas.Add(new LineaVenta
                {
                    ProductoId = producto.Id,
                    Producto = producto.Nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.PrecioUnitario
                });
            }
            return true;
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.CurrentRow?.DataBoundItem is LineaVenta linea)
            {
                _lineas.Remove(linea);
                ActualizarTotal();
            }
        }

        // Compatibilidad: algunas plantillas generan controladores con firmas no-nullable
        private void Cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // handler vacío para asegurar que los combo respondan a la interacción
        }

        // Compatibilidad: algunas plantillas generan controladores con firmas no-nullable
        private void NudCantidad_ValueChanged(object sender, EventArgs e)
        {
            // handler vacío para asegurar respuesta del control
        }

        // Enlaces con el diseñador (wrappers)
        // wrapper legacy removido: los eventos del diseñador usan los nombres actuales

        private void btnNueva_Click(object sender, EventArgs e) => NuevaVenta();

        // CREATE o UPDATE del maestro-detalle
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Comprobar al menos una línea antes de intentar guardar
            if (_lineas.Count == 0)
            {
                MessageBox.Show("Agrega al menos un producto antes de guardar.", "MiTiendaPOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboCliente.SelectedValue is not int clienteId ||
                cboUsuario.SelectedValue is not int usuarioId)
            {
                MessageBox.Show("Selecciona el cliente y el usuario.", "MiTiendaPOS");
                return;
            }
            // Si hay líneas con ProductoId == 0, intentar crear los productos automáticamente
            var lineasProcesadas = _lineas.Select(l => new LineaVenta
            {
                ProductoId = l.ProductoId,
                Producto = l.Producto,
                Cantidad = l.Cantidad,
                PrecioUnitario = l.PrecioUnitario
            }).ToList();

            for (int i = 0; i < lineasProcesadas.Count; i++)
            {
                if (lineasProcesadas[i].ProductoId == 0 && !string.IsNullOrWhiteSpace(lineasProcesadas[i].Producto))
                {
                    // crear o recuperar producto por nombre
                    // si el usuario escribió el producto y solicitó una cantidad, creamos el producto con stock inicial
                    int initialStock = lineasProcesadas[i].Cantidad;
                    int nuevoId = _servicio.ObtenerOCrearProductoPorNombre(lineasProcesadas[i].Producto, initialStock);
                    lineasProcesadas[i].ProductoId = nuevoId;
                }
                else
                {
                    // no hacemos validación de stock aquí: permitimos guardar siempre para continuar con flujo
                }
            }

            try
            {
                if (_ventaId == 0)
                {
                    int id = _servicio.Registrar(clienteId, usuarioId, lineasProcesadas);
                    MessageBox.Show($"Venta #{id} registrada.", "MiTiendaPOS",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _servicio.Actualizar(_ventaId, clienteId, usuarioId, lineasProcesadas);
                    MessageBox.Show($"Venta #{_ventaId} actualizada.", "MiTiendaPOS",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                CargarCatalogos();   // refresca productos (stock nuevo)
                CargarHistorial();
                NuevaVenta();
            }
            catch (Exception ex)
            {
                // Mostrar la excepción interna para diagnosticar el fallo y facilitar la corrección
                var inner = ex.InnerException?.Message ?? ex.GetBaseException().Message;
                MessageBox.Show($"No se pudo guardar\n\n{inner}", "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Detalles completos en otra caja si el usuario los necesita
                var detalle = ex.ToString();
                var dlg = MessageBox.Show("¿Ver detalles técnicos?", "Detalles", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlg == DialogResult.Yes)
                {
                    // mostrar detalle largo en formulario de texto simple
                    using var f = new Form();
                    f.Text = "Detalles de la excepción";
                    f.StartPosition = FormStartPosition.CenterParent;
                    f.Size = new Size(800, 600);
                    var tb = new TextBox();
                    tb.Multiline = true;
                    tb.ReadOnly = true;
                    tb.ScrollBars = ScrollBars.Both;
                    tb.Dock = DockStyle.Fill;
                    tb.Text = detalle;
                    f.Controls.Add(tb);
                    f.ShowDialog(this);
                }
            }
        }

        // READ (uno): cargar una venta del historial para editarla
        private void dgvVentas_CellClick(object sender,
                                         DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int id = Convert.ToInt32(dgvVentas.Rows[e.RowIndex].Cells["id"].Value);

            Venta? venta = _servicio.ObtenerConDetalle(id);
            if (venta == null) return;

            _ventaId = venta.Id;
            cboCliente.SelectedValue = venta.ClienteId;
            cboUsuario.SelectedValue = venta.UsuarioId;

            _lineas.Clear();
            foreach (var d in venta.Detalles)
            {
                _lineas.Add(new LineaVenta
                {
                    ProductoId = d.ProductoId,
                    Producto = d.Producto.Nombre,   // gracias a ThenInclude
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario
                });
            }
            lblVenta.Text = $"Editando venta #{venta.Id}";
            btnEliminar.Enabled = true;
            ActualizarTotal();
        }

        // DELETE de tabla maestra (Cascade borra sus detalles)
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_ventaId == 0) return;
            var respuesta = MessageBox.Show(
                $"¿Eliminar la venta #{_ventaId}? Se devolverá el stock.",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;

            try
            {
                _servicio.Eliminar(_ventaId);
                CargarCatalogos();
                CargarHistorial();
                NuevaVenta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo eliminar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
