using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Compra
    {
        public int id { get; set; }
        public Usuario oUsuario { get; set; } 
        public Proveedor oProveedor { get; set; }
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public decimal montoTotal { get; set; }
        public List<DetalleCompra> oDetalleCompra { get; set; } //Dentro de una compra hay varios detalles de compra
        public string fechaRegistro { get; set; }

        
    }
}
