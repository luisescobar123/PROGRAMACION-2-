using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiTiendaPOS.Migrations
{
    public partial class SeedClientesProductos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insertar clientes si no existen
            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM dbo.Clientes WHERE Nombre = 'Consumidor Final')
    INSERT INTO dbo.Clientes (Nombre, Correo, Telefono) VALUES ('Consumidor Final', NULL, NULL);
IF NOT EXISTS (SELECT 1 FROM dbo.Clientes WHERE Nombre = 'María Lopez')
    INSERT INTO dbo.Clientes (Nombre, Correo, Telefono) VALUES ('María Lopez', NULL, '7000-0000');
IF NOT EXISTS (SELECT 1 FROM dbo.Clientes WHERE Nombre = 'Juan Pérez')
    INSERT INTO dbo.Clientes (Nombre, Correo, Telefono) VALUES ('Juan Pérez', NULL, '7000-0001');
IF NOT EXISTS (SELECT 1 FROM dbo.Clientes WHERE Nombre = 'Carlos Gómez')
    INSERT INTO dbo.Clientes (Nombre, Correo, Telefono) VALUES ('Carlos Gómez', NULL, '7000-0002');
IF NOT EXISTS (SELECT 1 FROM dbo.Clientes WHERE Nombre = 'Ana Torres')
    INSERT INTO dbo.Clientes (Nombre, Correo, Telefono) VALUES ('Ana Torres', NULL, '7000-0003');

-- Actualizar stock por nombre de producto
UPDATE dbo.Productos SET Stock = 20 WHERE Nombre = 'Soda 600ml' OR Nombre LIKE '%Soda%';
UPDATE dbo.Productos SET Stock = 30 WHERE Nombre = 'Arroz Blanco' OR Nombre LIKE '%Arroz%';
" );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DELETE FROM dbo.Clientes WHERE Nombre IN ('Consumidor Final','María Lopez','Juan Pérez','Carlos Gómez','Ana Torres');
-- No revertir stocks por seguridad
" );
        }
    }
}
