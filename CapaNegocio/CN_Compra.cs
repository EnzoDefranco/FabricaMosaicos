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
    public class CN_Compra
    {
        private CD_Compra objcd_compra = new CD_Compra(); // Se crea una instancia de la clase CD_Compra

        public int obtenerCorrelativo()
        {
            return objcd_compra.obtenerCorrelativo();
        }

        public bool Registrar(Compra obj, string detallesJson, out string Mensaje)
        {

                return objcd_compra.Registrar(obj, detallesJson, out Mensaje);
            
        }

        public List<Compra> Listar(CompraFiltro filtro = null)
        {
            return objcd_compra.Listar(filtro);
        }

        //public List<DetalleCompra> ObtenerDetalleCompra(int idCompra) // Método que devuelve una lista de objetos Usuario
        //{
        //    return objcd_compra.ObtenerDetalleCompra(idCompra); // Llama al método Listar de la clase CD_Usuario
        //}

        public Compra ObtenerCompra(string numero)
        {
            Compra oCompra = objcd_compra.ObtenerCompra(numero);


            if (oCompra.id != 0)
            {
                List<DetalleCompra> oDetalleCompra = objcd_compra.ObtenerDetalleCompra(oCompra.id);
                oCompra.oDetalleCompra = oDetalleCompra;
            }
            return oCompra;
        }

        //public List<Compra> ListarPorFechas(DateTime fechaInicio, DateTime fechaFin, string razonSocial) // Método que devuelve una lista de objetos Usuario
        //{
        //    return objcd_compra.ListarPorFechas(fechaInicio, fechaFin, razonSocial); // Llama al método Listar de la clase CD_Usuario
        //}

        public bool EliminarCompra(int idCompra) // Método que devuelve una lista de objetos Usuario
        {
            return objcd_compra.EliminarCompra(idCompra); // Llama al método Listar de la clase CD_Usuario

        }

        public bool EliminarDetalleCompra(int idCompra) // Método que devuelve una lista de objetos Usuario
        {
            return objcd_compra.EliminarDetalleCompra(idCompra); // Llama al método Listar de la clase CD_Usuario
        }

        public bool Editar(Compra obj, out string Mensaje) // Método que devuelve una lista de objetos Usuario
        {
            return objcd_compra.Editar(obj, out Mensaje); // Llama al método Listar de la clase CD_Usuario
        }


    }
}





