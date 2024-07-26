using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuario
    {
        private CD_Usuario objcd_usuario = new CD_Usuario(); // Se crea una instancia de la clase CD_Usuario

        public List <Usuario> Listar() // Método que devuelve una lista de objetos Usuario
        {
           return objcd_usuario.Listar(); // Llama al método Listar de la clase CD_Usuario
        }
    }
}
