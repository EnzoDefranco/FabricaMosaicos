using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Cliente
    {
        private CD_Cliente objcd_cliente = new CD_Cliente(); // Se crea una instancia de la clase CD_Categoria

        //public List<Cliente> Listar(ClienteFiltro filtro) // Método que devuelve una lista de objetos Usuario
        //{
        //    return objcd_cliente.Listar(filtro); // Llama al método Listar de la clase CD_Usuario
        //}


        public List<Cliente> Listar(ClienteFiltro filtro = null) // Método que devuelve una lista de objetos Cliente
        {
            return objcd_cliente.Listar(filtro); // Llama al método Listar de la clase CD_Cliente
        }

        public int Registrar(Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            // Validaciones


            if (string.IsNullOrWhiteSpace(obj.nombreCompleto))
            {
                Mensaje += "Por favor ingrese el nombre completo\n";
            }

            if (string.IsNullOrWhiteSpace(obj.direccion))
            {
                Mensaje += "Por favor ingrese la direccion\n";
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
                return objcd_cliente.Registrar(obj, out Mensaje);
            }
            catch (Exception ex)
            {
                Mensaje = "Error al registrar el cliente: " + ex.Message;
                return 0;
            }
        }
        public bool Editar(Cliente obj, out string Mensaje) // Método que recibe un objeto de tipo Usuario
        {
            Mensaje = string.Empty; // Inicializa la variable Mensaje
           
            if (obj.nombreCompleto == string.Empty) // Si la propiedad nombreCompleto del objeto es igual a vacío
            {
                Mensaje += "Por favor ingrese la nombreCompleto \n"; // Asigna un mensaje a la variable Mensaje
            }

            if (obj.direccion == string.Empty) // Si la propiedad nombreCompleto del objeto es igual a vacío
            {
                Mensaje += "Por favor ingrese la direccion \n"; // Asigna un mensaje a la variable Mensaje
            }

            if (obj.telefono == string.Empty) // Si la propiedad nombreCompleto del objeto es igual a vacío
            {
                Mensaje += "Por favor ingrese el telefono \n"; // Asigna un mensaje a la variable Mensaje
            }
           

            if (Mensaje != string.Empty)
            {  // Si la variable Mensaje es diferente de vacío
                return false; // Retorna 0
            }
            else
            {
                return objcd_cliente.Editar(obj, out Mensaje); // Llama al método Insertar de la clase CD_Usuario
            }




        }

        public bool Eliminar(int id, out string Mensaje) // Método que recibe un objeto de tipo Usuario
        {
            return objcd_cliente.Eliminar(id, out Mensaje); // Llama al método Eliminar de la clase CD_Usuario
        }
    }
}
