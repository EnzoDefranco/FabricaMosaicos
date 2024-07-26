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
        public Venta oVenta { get; set; } // o de objeto
        public decimal precioVenta { get; set; }

        public int cantidad { get; set; }

        public decimal subTotal { get; set; }


        public string fechaRegistro { get; set; }
    }
}
