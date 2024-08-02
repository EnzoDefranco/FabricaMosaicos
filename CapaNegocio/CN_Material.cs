using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Material
    {
        private CD_Material objcd_material = new CD_Material(); // Se crea una instancia de la clase CD_Categoria


        public List<Material> Listar() // Método que devuelve una lista de objetos Usuario
        {
            return objcd_material.Listar(); // Llama al método Listar de la clase CD_Usuario
        }

        public int Registrar(Material obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            // Validaciones
            if (string.IsNullOrWhiteSpace(obj.codigo))
            {
                Mensaje += "Por favor ingrese el codigo\n";
            }
            if (string.IsNullOrWhiteSpace(obj.nombre))
            {
                Mensaje += "Por favor ingrese el nombre\n";
            }
            if (string.IsNullOrWhiteSpace(obj.descripcion))
            {
                Mensaje += "Por favor ingrese la descripcion\n";
            }
            if (!Enum.IsDefined(typeof(TipoMaterial), obj.tipoMaterial))
            {
                Mensaje += "El tipo de material no puede estar vacío.\n";
            }

            // Si hay mensajes de error, retornar 0
            if (!string.IsNullOrEmpty(Mensaje))
            {
                return 0;
            }

            try
            {
                return objcd_material.Registrar(obj, out Mensaje);
            }
            catch (Exception ex)
            {
                Mensaje = "Error al registrar el material: " + ex.Message;
                return 0;
            }
        }


        public bool Editar(Material obj, out string Mensaje) // Método que recibe un objeto de tipo Usuario
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
                return objcd_material.Editar(obj, out Mensaje); // Llama al método Insertar de la clase CD_Usuario
            }




        }

        public bool Eliminar(int id, out string Mensaje) // Método que recibe un objeto de tipo Usuario
        {
            return objcd_material.Eliminar(id, out Mensaje); // Llama al método Eliminar de la clase CD_Usuario
        }
    }
}
