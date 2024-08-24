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
    public class CN_ReporteCompra
    {
        private CD_ReporteCompra objcd_reporteCompra = new CD_ReporteCompra(); // Se crea una instancia de la clase CD_ReporteCompra


        public List<ReporteCompra> ObtenerReporteCompra(DateTime fechaInicio, DateTime fechaFin, string razonSocial)
        {
            return objcd_reporteCompra.ObtenerReporteCompra(fechaInicio, fechaFin, razonSocial);
        }

    }
}
