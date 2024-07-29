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

        public int Registrar(Usuario obj, out string Mensaje) // Método que recibe un objeto de tipo Usuario
        {
            Mensaje = string.Empty; // Inicializa la variable Mensaje
            if (obj.razonSocial == string.Empty) // Si la propiedad razonSocial del objeto es igual a vacío
            {
                Mensaje += "Por favor ingrese la razón social \n"; // Asigna un mensaje a la variable Mensaje
            }
            if (obj.correo == string.Empty) // Si la propiedad correo del objeto es igual a vacío
            {
                Mensaje += "Por favor ingrese el correo \n"; // Asigna un mensaje a la variable Mensaje
            }
            if (obj.clave == string.Empty) // Si la propiedad clave del objeto es igual a vacío
            {
                Mensaje += "Por favor ingrese la clave \n"; // Asigna un mensaje a la variable Mensaje

            }

            if (Mensaje != string.Empty) {  // Si la variable Mensaje es diferente de vacío
                return 0; // Retorna 0
            }else
                {
                return objcd_usuario.Registrar(obj, out Mensaje); // Llama al método Insertar de la clase CD_Usuario
            }
        }

        public bool Editar(Usuario obj, out string Mensaje) // Método que recibe un objeto de tipo Usuario
        {
            Mensaje = string.Empty; // Inicializa la variable Mensaje

            if (obj.documento == "") // Si la propiedad documento del objeto es igual a vacío
            {
                Mensaje += "Por favor ingrese el documento \n'"; // Asigna un mensaje a la variable Mensaje
            }
            if (obj.razonSocial == "")
            {
                Mensaje += "Por favor ingrese la razon social del usuario \n";
            }


            if (Mensaje != string.Empty)
            {
                return false;
            }else
            {
                return objcd_usuario.Editar(obj, out Mensaje); // Llama al método Actualizar de la clase CD_Usuario
            }




        }

        public bool Eliminar(int id, out string Mensaje) // Método que recibe un entero
        {
            return objcd_usuario.Eliminar(id, out Mensaje); // Llama al método Eliminar de la clase CD_Usuario
        }
    }
}
