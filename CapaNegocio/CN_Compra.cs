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
    }
}





