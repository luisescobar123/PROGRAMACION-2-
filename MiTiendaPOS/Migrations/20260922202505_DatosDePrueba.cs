using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiTiendaPOS.Migrations
{
    /// <inheritdoc />
    public partial class DatosDePrueba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // inserts guarded to avoid duplicate-key errors if data already exists
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Clientes WHERE Nombre = 'Consumidor Final') INSERT INTO dbo.Clientes (Nombre, Correo, Telefono) VALUES ('Consumidor Final', NULL, NULL);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Clientes WHERE Nombre = 'María Lopez') INSERT INTO dbo.Clientes (Nombre, Correo, Telefono) VALUES ('María Lopez', NULL, '7000-0000');");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Clientes WHERE Nombre = 'Juan Pérez') INSERT INTO dbo.Clientes (Nombre, Correo, Telefono) VALUES ('Juan Pérez', NULL, '7000-0001');");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Clientes WHERE Nombre = 'Carlos Gómez') INSERT INTO dbo.Clientes (Nombre, Correo, Telefono) VALUES ('Carlos Gómez', NULL, '7000-0002');");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Clientes WHERE Nombre = 'Ana Torres') INSERT INTO dbo.Clientes (Nombre, Correo, Telefono) VALUES ('Ana Torres', NULL, '7000-0003');");

            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Nombre = 'Soda 600ml') INSERT INTO dbo.Productos (Activo, CategoriaId, Nombre, PrecioUnitario, Stock) VALUES (1, 1, 'Soda 600ml', 0.90, 48);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Nombre = 'Agua Purificada') INSERT INTO dbo.Productos (Activo, CategoriaId, Nombre, PrecioUnitario, Stock) VALUES (1, 1, 'Agua Purificada', 0.50, 60);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Nombre = 'Jugo Naranja 1L') INSERT INTO dbo.Productos (Activo, CategoriaId, Nombre, PrecioUnitario, Stock) VALUES (1, 1, 'Jugo Naranja 1L', 1.20, 30);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Nombre = 'Cerveza 330ml') INSERT INTO dbo.Productos (Activo, CategoriaId, Nombre, PrecioUnitario, Stock) VALUES (1, 1, 'Cerveza 330ml', 1.50, 40);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Nombre = 'Arroz Blanco') INSERT INTO dbo.Productos (Activo, CategoriaId, Nombre, PrecioUnitario, Stock) VALUES (1, 2, 'Arroz Blanco', 0.85, 40);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Nombre = 'Frijol de Seda') INSERT INTO dbo.Productos (Activo, CategoriaId, Nombre, PrecioUnitario, Stock) VALUES (1, 2, 'Frijol de Seda', 1.10, 20);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Nombre = 'Aceite 1L') INSERT INTO dbo.Productos (Activo, CategoriaId, Nombre, PrecioUnitario, Stock) VALUES (1, 2, 'Aceite 1L', 2.50, 25);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Nombre = 'Azúcar 1kg') INSERT INTO dbo.Productos (Activo, CategoriaId, Nombre, PrecioUnitario, Stock) VALUES (1, 2, 'Azúcar 1kg', 0.95, 35);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Nombre = 'Harina 1kg') INSERT INTO dbo.Productos (Activo, CategoriaId, Nombre, PrecioUnitario, Stock) VALUES (1, 2, 'Harina 1kg', 0.80, 30);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Nombre = 'Leche 1L') INSERT INTO dbo.Productos (Activo, CategoriaId, Nombre, PrecioUnitario, Stock) VALUES (1, 2, 'Leche 1L', 0.99, 50);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Nombre = 'Detergente MAXI ESPUMA') INSERT INTO dbo.Productos (Activo, CategoriaId, Nombre, PrecioUnitario, Stock) VALUES (1, 3, 'Detergente MAXI ESPUMA', 3.25, 15);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Nombre = 'Lejia 1L') INSERT INTO dbo.Productos (Activo, CategoriaId, Nombre, PrecioUnitario, Stock) VALUES (1, 3, 'Lejia 1L', 1.15, 25);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Nombre = 'Jabón en polvo 2kg') INSERT INTO dbo.Productos (Activo, CategoriaId, Nombre, PrecioUnitario, Stock) VALUES (1, 3, 'Jabón en polvo 2kg', 4.50, 10);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Nombre = 'Suavizante 1L') INSERT INTO dbo.Productos (Activo, CategoriaId, Nombre, PrecioUnitario, Stock) VALUES (1, 3, 'Suavizante 1L', 2.10, 18);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Nombre = 'Té Helado 500ml') INSERT INTO dbo.Productos (Activo, CategoriaId, Nombre, PrecioUnitario, Stock) VALUES (1, 1, 'Té Helado 500ml', 0.75, 40);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Nombre = 'Energizante 500ml') INSERT INTO dbo.Productos (Activo, CategoriaId, Nombre, PrecioUnitario, Stock) VALUES (1, 1, 'Energizante 500ml', 1.80, 22);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Nombre = 'Galletas Surtidas') INSERT INTO dbo.Productos (Activo, CategoriaId, Nombre, PrecioUnitario, Stock) VALUES (1, 2, 'Galletas Surtidas', 1.25, 45);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Nombre = 'Cereal 300g') INSERT INTO dbo.Productos (Activo, CategoriaId, Nombre, PrecioUnitario, Stock) VALUES (1, 2, 'Cereal 300g', 2.75, 20);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Nombre = 'Esponja multiuso') INSERT INTO dbo.Productos (Activo, CategoriaId, Nombre, PrecioUnitario, Stock) VALUES (1, 3, 'Esponja multiuso', 0.65, 60);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Nombre = 'Limpiador multiusos 500ml') INSERT INTO dbo.Productos (Activo, CategoriaId, Nombre, PrecioUnitario, Stock) VALUES (1, 3, 'Limpiador multiusos 500ml', 1.45, 28);");

            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM dbo.Usuarios WHERE NombreUsuario = 'cajero1') INSERT INTO dbo.Usuarios (NombreUsuario, Rol) VALUES ('cajero1','Cajero');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
