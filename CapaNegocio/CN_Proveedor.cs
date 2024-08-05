using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{

    public class CN_Proveedor
    {

        private CD_Proveedor objcd_proveedor = new CD_Proveedor(); // Se crea una instancia de la clase CD_Categoria

        public List<Proveedor> Listar() // Método que devuelve una lista de objetos Usuario
        {
            return objcd_proveedor.Listar(); // Llama al método Listar de la clase CD_Usuario
        }

        public int Registrar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            // Validaciones
            if (string.IsNullOrWhiteSpace(obj.documento))
            {
                Mensaje += "Por favor ingrese el documento\n";
            }
            if (string.IsNullOrWhiteSpace(obj.razonSocial))
            {
                Mensaje += "Por favor ingrese el razonSocial\n";
            }
            if (string.IsNullOrWhiteSpace(obj.correo))
            {
                Mensaje += "Por favor ingrese la correo\n";
            }
            if (string.IsNullOrWhiteSpace(obj.telefono))
            {
                Mensaje += "Por favor ingrese el telefono\n";
            }
         

            // Si hay mensajes de error, retornar 0
            if (!string.IsNullOrEmpty(Mensaje))
            {
                return 0;
            }

            try
            {
                return objcd_proveedor.Registrar(obj, out Mensaje);
            }
            catch (Exception ex)
            {
                Mensaje = "Error al registrar el proveedor: " + ex.Message;
                return 0;
            }
        }

        public bool Editar(Proveedor obj, out string Mensaje) // Método que recibe un objeto de tipo Usuario
        {
            Mensaje = string.Empty; // Inicializa la variable Mensaje
            if (obj.documento == string.Empty) // Si la propiedad nombreCompleto del objeto es igual a vacío
            {
                Mensaje += "Por favor ingrese la documento \n"; // Asigna un mensaje a la variable Mensaje
            }
            if (obj.razonSocial == string.Empty) // Si la propiedad nombreCompleto del objeto es igual a vacío
            {
                Mensaje += "Por favor ingrese la razonSocial \n"; // Asigna un mensaje a la variable Mensaje
            }
            if (obj.correo == string.Empty) // Si la propiedad nombreCompleto del objeto es igual a vacío
            {
                Mensaje += "Por favor ingrese la correo \n"; // Asigna un mensaje a la variable Mensaje
            }
            if (obj.telefono == string.Empty) // Si la propiedad nombreCompleto del objeto es igual a vacío
            {
                Mensaje += "Por favor ingrese la telefono \n"; // Asigna un mensaje a la variable Mensaje
            }

            if (Mensaje != string.Empty)
            {  // Si la variable Mensaje es diferente de vacío
                return false; // Retorna 0
            }
            else
            {
                return objcd_proveedor.Editar(obj, out Mensaje); // Llama al método Insertar de la clase CD_Usuario
            }




        }

        public bool Eliminar(int id, out string Mensaje) // Método que recibe un objeto de tipo Usuario
        {
            return objcd_proveedor.Eliminar(id, out Mensaje); // Llama al método Eliminar de la clase CD_Usuario
        }
    }
}
