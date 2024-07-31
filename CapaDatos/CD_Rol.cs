using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace CapaDatos
{
    public class CD_Rol
    {
        public List<Rol> Listar()
        {
            List<Rol> lista = new List<Rol>();
            using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.Append("SELECT id, descripcion FROM Rol");
                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                lista.Add(new Rol()
                                {
                                    id = Convert.ToInt32(dr["id"]),
                                    descripcion = dr["descripcion"].ToString(),
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
                    lista = new List<Rol>();
                }
                return lista;
            }
        }
    }
}
//public class CD_Permiso
//{
//    static string conexionstring = "Data Source=DESKTOP-JPUHD28\\SQLEXPRESS;Initial Catalog=DBSISTEMA_VENTA;Integrated Security=True;Encrypt=False";
//    public List<Permiso> Listar(int idUsuario) // Método que devuelve una lista de objetos Usuario
//    {
//        List<Permiso> lista = new List<Permiso>(); // Se declara una lista vacía que contendrá los objetos Usuario
//        using (SqlConnection oconexion = new SqlConnection(conexionstring)) // Se crea una nueva instancia de SqlConnection llamada oconexion
//        {
//            try
//            {
//                StringBuilder query = new StringBuilder();
//                query.Append("SELECT p.idRol, p.nombreMenu FROM Permiso p ");
//                query.Append("INNER JOIN Rol r ON r.id = p.idRol ");
//                query.Append("INNER JOIN Usuario u ON u.idRol = r.id ");
//                query.Append("WHERE u.id = @idUsuario");




//                SqlCommand cmd = new SqlCommand(query.ToString(), oconexion); // Se crea una nueva instancia de SqlCommand llamada cmd
//                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
//                cmd.CommandType = CommandType.Text; // Es un comando de tipo texto ya que se va a ejecutar una consulta
//                oconexion.Open(); // Se abre la conexión a la base de datos

//                using (SqlDataReader dr = cmd.ExecuteReader()) // el bloque using se encarga de cerrar automáticamente el SqlDataReader y liberar los recursos asociados.
//                {
//                    if (dr.HasRows) // Verifica que el SqlDataReader tenga al menos una fila
//                    {
//                        while (dr.Read()) // Si dr tiene filas, se itera sobre cada fila utilizando un bucle while y se crea un nuevo objeto Usuario con los valores de cada columna de la fila actual.
//                        {
//                            lista.Add(new Permiso() // Crea un nuevo objeto Usuario con los valores de cada columna de la fila actual
//                            {
//                                oRol = new Rol() { id = Convert.ToInt32(dr["idRol"]) }, // Convierte el valor de la columna id a entero
//                                nombreMenu = dr["nombreMenu"].ToString() // Obtiene el valor de la columna documento como cadena
//                            });

//                        }
//                    }
//                    else
//                    {
//                        Console.WriteLine("No se encontraron registros en la consulta.");
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error: {ex.Message}");
//                lista = new List<Permiso>(); // Si ocurre una excepción, se crea una nueva lista vacía
//            }
//        }
//        return lista;
//    }
//}