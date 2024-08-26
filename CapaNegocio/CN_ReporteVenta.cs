using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaNegocio
{
    public class CN_ReporteVenta
    {
        private CD_ReporteVenta objcd_reporteVenta = new CD_ReporteVenta(); // Se crea una instancia de la clase CD_ReporteCompra


        public List<ReporteVenta> ObtenerReporteVenta(VentaFiltro filtro = null)
        {
            return objcd_reporteVenta.ObtenerReporteVenta(filtro);
        }
    }
}
