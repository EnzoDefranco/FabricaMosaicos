using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Venta
    {
        public int id { get; set; }
        public Usuario oUsuario { get; set; } 
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public Cliente oCliente { get; set; }
        public string documentoCliente { get; set; }
        public string nombreCliente { get; set; }
        public decimal montoPago { get; set; }
        public decimal montoTotal { get; set; }
        public decimal montoCambio { get; set; }
        public List<DetalleVenta> oDetalleVenta { get; set; } //Dentro de una venta hay varios detalles de venta 
        public string fechaRegistro { get; set; }
    }
}
