using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_Permiso
    {
        private CD_Permiso objcd_permiso = new CD_Permiso(); // Se crea una instancia de la clase CD_Permiso

        public List<Permiso> Listar(int idUsuario) // Método que devuelve una lista de objetos Permiso
        {
            return objcd_permiso.Listar(idUsuario); // Llama al método Listar de la clase CD_Permiso
        }
    }
}
