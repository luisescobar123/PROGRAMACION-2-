using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiTiendaPOS.Dtos
{
    // Línea de venta en pantalla (aún no es un registro de la base de datos) 
    public class LineaVenta
    {
        public int ProductoId { get; set; }
        public string Producto { get; set; } = "";
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;   // propiedad calculada 
    }
}