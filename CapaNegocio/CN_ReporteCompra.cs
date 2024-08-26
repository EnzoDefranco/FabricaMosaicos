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


        public List<ReporteCompra> ObtenerReporteCompra(CompraFiltro filtro = null)
        {
            return objcd_reporteCompra.ObtenerReporteCompra(filtro);
        }

    }
}
