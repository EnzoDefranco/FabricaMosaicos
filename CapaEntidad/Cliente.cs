using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Cliente
    {
        public int id { get; set; }
        public string documento { get; set; }

        public string clienteTipo { get; set; }
        public string nombreCompleto { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string ciudad { get; set; }
        public bool estado { get; set; }
        public string fechaRegistro { get; set; }
    }
}
