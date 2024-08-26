using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class VentaFiltro
    {
        public string nombreCompleto { get; set; }

        public string filtrarPorPago { get; set; }

        public string filtrarPorCumplimiento { get; set; }

        public bool filtrarPorBoleta { get; set; }

        public bool filtrarPorFactura { get; set; }

        public bool filtrarPorPresupuesto { get; set; }

        public bool filtrarPorEmpresa { get; set; }

        public bool filtrarPorParticular { get; set; }

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
