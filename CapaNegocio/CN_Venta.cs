using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Venta
    {
        private CD_Venta objcd_venta = new CD_Venta(); // Se crea una instancia de la clase CD_Compra

        public int obtenerCorrelativo()
        {
            return objcd_venta.obtenerCorrelativo();
        }

        public bool Registrar(Venta obj, string detallesJson, out string Mensaje)
        {

            return objcd_venta.Registrar(obj, detallesJson, out Mensaje);

        }
        public Venta ObtenerVenta(string numero)
        {
            Venta oVenta = objcd_venta.ObtenerVenta(numero);


            if (oVenta.id != 0)
            {
                List<DetalleVenta> oDetalleVenta = objcd_venta.ObtenerDetalleVenta(oVenta.id);
                oVenta.oDetalleVenta = oDetalleVenta;
            }
            return oVenta;
        }

        public bool Editar(Venta obj, out string Mensaje) // Método que devuelve una lista de objetos Usuario
        {
            return objcd_venta.Editar(obj, out Mensaje); // Llama al método Listar de la clase CD_Usuario
        }
        public (List<Venta>, decimal) Listar(VentaFiltro filtro = null)
        {
            return objcd_venta.Listar(filtro);
        }

        public bool EliminarVenta(int idVenta) // Método que devuelve una lista de objetos Usuario
        {
            return objcd_venta.EliminarVenta(idVenta); // Llama al método Listar de la clase CD_Usuario

        }

        public bool EliminarDetalleVenta(int idVenta) // Método que devuelve una lista de objetos Usuario
        {
            return objcd_venta.EliminarDetalleVenta(idVenta); // Llama al método Listar de la clase CD_Usuario
        }


    }
}
