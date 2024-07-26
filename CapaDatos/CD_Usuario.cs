using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Usuario
    {
        static string conexionstring = "Data Source=DESKTOP-JPUHD28\\SQLEXPRESS;Initial Catalog=DBSISTEMA_VENTA;Integrated Security=True;Encrypt=False";
        public List<Usuario> Listar() // Método que devuelve una lista de objetos Usuario
        {
            List<Usuario> lista = new List<Usuario>(); // Se declara una lista vacía que contendrá los objetos Usuario
            using (SqlConnection oconexion = new SqlConnection(conexionstring)) // Se crea una nueva instancia de SqlConnection llamada oconexion
            {
                try
                {
                    string query = "SELECT id,documento,razonSocial,correo,clave,estado FROM Usuario"; // Consulta SQL para seleccionar los campos de la tabla Usuario
                    SqlCommand cmd = new SqlCommand(query, oconexion); // Se crea una nueva instancia de SqlCommand llamada cmd
                    cmd.CommandType = CommandType.Text; // Es un comando de tipo texto ya que se va a ejecutar una consulta
                    oconexion.Open(); // Se abre la conexión a la base de datos

                    using (SqlDataReader dr = cmd.ExecuteReader()) // el bloque using se encarga de cerrar automáticamente el SqlDataReader y liberar los recursos asociados.
                    {
                        if (dr.HasRows) // Verifica que el SqlDataReader tenga al menos una fila
                        {
                            while (dr.Read()) // Si dr tiene filas, se itera sobre cada fila utilizando un bucle while y se crea un nuevo objeto Usuario con los valores de cada columna de la fila actual.
                            {
                                lista.Add(new Usuario() // Crea un nuevo objeto Usuario con los valores de cada columna de la fila actual
                                {
                                    id = Convert.ToInt32(dr["id"]), // Convierte el valor de la columna id a entero
                                    documento = dr["documento"].ToString(), // Obtiene el valor de la columna documento como cadena
                                    razonSocial = dr["razonSocial"].ToString(), // Obtiene el valor de la columna razonSocial como cadena
                                    correo = dr["correo"].ToString(), // Obtiene el valor de la columna correo como cadena
                                    clave = dr["clave"].ToString(), // Obtiene el valor de la columna clave como cadena
                                    estado = Convert.ToBoolean(dr["estado"]) // Convierte el valor de la columna estado a booleano
                                });
                            }
                        }
                        else
                        {
                            Console.WriteLine("No se encontraron registros en la consulta.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    lista = new List<Usuario>();
                }
            }
            return lista;
        }
    }
}

//Aquí está el desglose del código:
//1.Se declara una lista vacía llamada lista que contendrá los objetos Usuario.
//2.	Se crea una nueva instancia de la clase SqlConnection llamada oconexion utilizando la cadena de conexión conexionstring.
//3.	Se utiliza un bloque try-catch para manejar cualquier excepción que pueda ocurrir durante la ejecución del código.
//4.	Se define una cadena de consulta SQL que selecciona los campos id, documento, razonSocial, correo, clave y estado de la tabla Usuario.
//5.	Se crea una nueva instancia de la clase SqlCommand llamada cmd pasando la consulta SQL y la conexión oconexion.
//6.	Se establece el tipo de comando de cmd como CommandType.Text, lo que indica que se ejecutará una consulta de texto.
//7.	Se abre la conexión oconexion utilizando el método Open().
//8.	Se utiliza un bloque using con un SqlDataReader llamado dr para ejecutar la consulta y leer los resultados.
//9.	Se verifica si dr tiene filas utilizando el método HasRows.
//10.	Si dr tiene filas, se itera sobre cada fila utilizando un bucle while y se crea un nuevo objeto Usuario con los valores de cada columna de la fila actual. Luego, se agrega este objeto a la lista lista.
//11.	Si dr no tiene filas, se muestra un mensaje indicando que no se encontraron registros en la consulta.
//12.	Después de leer todos los resultados, el bloque using se encarga de cerrar automáticamente el SqlDataReader y liberar los recursos asociados.
//13.	Si se produce una excepción durante la ejecución del código, se captura en el bloque catch y se muestra un mensaje de error. Luego, se asigna una nueva lista vacía a lista.
//14.	Finalmente, se devuelve la lista lista que contiene los objetos Usuario.