using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CompraFiltro
    {

        public string RazonSocial { get; set; }

        public bool filtrarPorBoleta { get; set; }

        public bool filtrarPorFactura { get; set; }

        public bool filtrarPorPresupuesto { get; set; }

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        // Agrega más propiedades según los filtros adicionales
    }
}
