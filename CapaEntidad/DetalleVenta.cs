using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DetalleVenta
    {
        public int id { get; set; }
        public Material oMaterial { get; set; } // o de objeto
        public decimal precioCompra { get; set; }
        public decimal precioVenta { get; set; }
        public decimal cantidad { get; set; }
        public decimal montoTotal { get; set; }
        public string fechaRegistro { get; set; }
    }
}
