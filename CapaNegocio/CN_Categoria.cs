using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_Categoria
    {
        private CD_Categoria objcd_categoria = new CD_Categoria(); // Se crea una instancia de la clase CD_Categoria


        public List<Categoria> Listar() // Método que devuelve una lista de objetos Usuario
        {
            return objcd_categoria.Listar(); // Llama al método Listar de la clase CD_Usuario
        }

        public int Registrar(Categoria obj, out string Mensaje) // Método que recibe un objeto de tipo Usuario
        {
            Mensaje = string.Empty; // Inicializa la variable Mensaje
            if (obj.descripcion == string.Empty) // Si la propiedad nombreCompleto del objeto es igual a vacío
            {
                Mensaje += "Por favor ingrese la descripcion \n"; // Asigna un mensaje a la variable Mensaje
            }
            if (Mensaje != string.Empty)
            {  // Si la variable Mensaje es diferente de vacío
                return 0; // Retorna 0
            }
            else
            {
                return objcd_categoria.Registrar(obj, out Mensaje); // Llama al método Insertar de la clase CD_Usuario
            }
        }

        public bool Editar(Categoria obj, out string Mensaje) // Método que recibe un objeto de tipo Usuario
        {
            Mensaje = string.Empty; // Inicializa la variable Mensaje
            if (obj.descripcion == string.Empty) // Si la propiedad nombreCompleto del objeto es igual a vacío
            {
                Mensaje += "Por favor ingrese la descripcion \n"; // Asigna un mensaje a la variable Mensaje
            }
            if (Mensaje != string.Empty)
            {  // Si la variable Mensaje es diferente de vacío
                return false; // Retorna 0
            }
            else
            {
                return objcd_categoria.Editar(obj, out Mensaje); // Llama al método Insertar de la clase CD_Usuario
            }




        }

        public bool Eliminar(int id, out string Mensaje) // Método que recibe un objeto de tipo Usuario
        {
            Mensaje = string.Empty; // Inicializa la variable Mensaje
            if (id == 0) // Si la propiedad nombreCompleto del objeto es igual a vacío
            {
                Mensaje += "Por favor seleccione una categoria \n"; // Asigna un mensaje a la variable Mensaje
            }
            if (Mensaje != string.Empty)
            {  // Si la variable Mensaje es diferente de vacío
                return false; // Retorna 0
            }
            else
            {
                return objcd_categoria.Eliminar(id, out Mensaje); // Llama al método Insertar de la clase CD_Usuario
            }
        }
    }
}
