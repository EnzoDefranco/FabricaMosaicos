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
        public Cliente oCliente { get; set; }
        public string tipoDocumento { get; set; }

        public string cumplimiento { get; set; }

        public string pago { get; set; }

        public string infoAdicional { get; set; }
        public string numeroDocumento { get; set; }
        public decimal montoTotal { get; set; }
        public List<DetalleVenta> oDetalleVenta { get; set; } //Dentro de una compra hay varios detalles de compra
        public string fechaRegistro { get; set; }

        public string formaPago { get; set; }
        public string condicionPago { get; set; }
    }
}
