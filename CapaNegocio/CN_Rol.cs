using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Rol
    {
        private CD_Rol objcn_ROL = new CD_Rol(); // Se crea una instancia de la clase CD_Rol

        public List<Rol> Listar() // Método que devuelve una lista de objetos Rol
        {
            return objcn_ROL.Listar(); // Llama al método Listar de la clase CN_Rol
        }
    }
}
