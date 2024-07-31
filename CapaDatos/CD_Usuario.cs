using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;
using System.Collections;
using System.Xml.Linq;
using MySql.Data.MySqlClient;   

namespace CapaDatos
{
    public class CD_Usuario
    {

        public List<Usuario> Listar() // Método que devuelve una lista de objetos Usuario
        {
            List<Usuario> lista = new List<Usuario>(); // Se declara una lista vacía que contendrá los objetos Usuario
            using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena)) // Se crea una nueva instancia de MySqlConnection llamada oconexion
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select u.id, u.documento, u.razonSocial,u.correo, u.clave,u.idRol, u.estado, r.descripcion from usuario u");
                    query.AppendLine("inner join Rol r on r.id = u.idRol");
                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion); // Se crea una nueva instancia de MySqlCommand llamada cmd
                    cmd.CommandType = CommandType.Text; // Es un comando de tipo texto ya que se va a ejecutar una consulta
                    oconexion.Open(); // Se abre la conexión a la base de datos

                    using (MySqlDataReader dr = cmd.ExecuteReader()) // el bloque using se encarga de cerrar automáticamente el MySqlDataReader y liberar los recursos asociados.
                    {
                        if (dr.HasRows) // Verifica que el MySqlDataReader tenga al menos una fila
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
                                    estado = Convert.ToBoolean(dr["estado"]), // Convierte el valor de la columna estado a booleano
                                    oRol = new Rol() { id = Convert.ToInt32(dr["id"]), descripcion = dr["descripcion"].ToString() }
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

        public int Registrar(Usuario obj, out string Mensaje) // Se le pasa un obj de tipo Usuario y devuelve un entero y un string
        {
            int idUsuarioGenerado = 0; // Se inicializa la variable idUsuarioGenerado en 0, que se utilizará para almacenar el ID del usuario generado
            Mensaje = string.Empty; // Se inicializa la variable Mensaje en vacío para almacenar mensajes de error o éxito

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
                {
                    MySqlCommand cmd = new MySqlCommand("SP_REGISTRARUSUARIO", oconexion); // Se crea una nueva instancia de MySqlCommand llamada cmd
                    cmd.CommandType = CommandType.StoredProcedure; // Es un comando de tipo procedimiento almacenado

                    // Parámetros de entrada
                    cmd.Parameters.AddWithValue("p_documento", obj.documento); // Se añade un parámetro de entrada con el valor de la propiedad documento del objeto Usuario
                    cmd.Parameters.AddWithValue("p_razonSocial", obj.razonSocial); // Se añade un parámetro de entrada con el valor de la propiedad razonSocial del objeto Usuario
                    cmd.Parameters.AddWithValue("p_correo", obj.correo); // Se añade un parámetro de entrada con el valor de la propiedad correo del objeto Usuario
                    cmd.Parameters.AddWithValue("p_clave", obj.clave);  // Se añade un parámetro de entrada con el valor de la propiedad clave del objeto Usuario
                    cmd.Parameters.AddWithValue("p_idRol", obj.oRol.id); // Se añade un parámetro de entrada con el valor de la propiedad id del objeto Rol
                    cmd.Parameters.AddWithValue("p_estado", obj.estado); // Se añade un parámetro de entrada con el valor de la propiedad estado del objeto Usuario

                    // Parámetros de salida
                    cmd.Parameters.Add("p_idUsuarioResultado", MySqlDbType.Int32).Direction = ParameterDirection.Output; // Se añade un parámetro de salida para almacenar el ID del usuario generado
                    cmd.Parameters.Add("p_mensaje", MySqlDbType.VarChar, 500).Direction = ParameterDirection.Output; // Se añade un parámetro de salida para almacenar mensajes de error o éxito

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    idUsuarioGenerado = Convert.ToInt32(cmd.Parameters["p_idUsuarioResultado"].Value); // Se obtiene el valor del parámetro de salida p_idUsuarioResultado y se convierte a entero
                    Mensaje = cmd.Parameters["p_mensaje"].Value.ToString(); // Se obtiene el valor del parámetro de salida p_mensaje como cadena
                }
            }
            catch (Exception ex)
            {
                idUsuarioGenerado = 0; // Se asigna 0 a idUsuarioGenerado en caso de error
                Mensaje = ex.Message; // Se asigna el mensaje de error a la variable Mensaje
            }

            return idUsuarioGenerado;
        }


        public bool Editar(Usuario obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
                {
                    MySqlCommand cmd = new MySqlCommand("SP_EDITARUSUARIO", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    cmd.Parameters.AddWithValue("p_id", obj.id);
                    cmd.Parameters.AddWithValue("p_documento", obj.documento);
                    cmd.Parameters.AddWithValue("p_razonSocial", obj.razonSocial);
                    cmd.Parameters.AddWithValue("p_correo", obj.correo);
                    cmd.Parameters.AddWithValue("p_clave", obj.clave);
                    cmd.Parameters.AddWithValue("p_idRol", obj.oRol.id);
                    cmd.Parameters.AddWithValue("p_estado", obj.estado);

                    // Parámetros de salida
                    cmd.Parameters.Add("p_respuesta", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_mensaje", MySqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["p_respuesta"].Value);
                    Mensaje = cmd.Parameters["p_mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }

            return respuesta;
        }


        public bool Eliminar(int id, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
                {
                    MySqlCommand cmd = new MySqlCommand("SP_ELIMINARUSUARIO", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    cmd.Parameters.AddWithValue("p_id", id);

                    // Parámetros de salida
                    cmd.Parameters.Add("p_respuesta", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_mensaje", MySqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["p_respuesta"].Value);
                    Mensaje = cmd.Parameters["p_mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }

            return respuesta;
        }

    }


}


//create PROC SP_ELIMINARUSUARIO(
//@id int,
//@respuesta bit output,
//@mensaje varchar(500) output
//)
//as
//begin
//	set @respuesta = 0
//	set @mensaje = ''
//	declare @pasoreglas bit = 1

//	if exists (select * from Compra c
//	inner join Usuario u on u.id = c.id
//	where u.id = @id)
//	begin
//		set @pasoreglas = 0
//		set @respuesta = 0
//		set @mensaje = @mensaje + 'No se puede eliminar el usuario porque se encuentra relacionado a una compra \n'
//	end
//	if exists (select * from Venta v
//	inner join Usuario u on u.id = v.id
//	where u.id = @id)
//	begin
//		set @pasoreglas = 0
//		set @respuesta = 0
//		set @mensaje = @mensaje + 'No se puede eliminar el usuario porque se encuentra relacionado a una venta \n'
//	end
//	if (@pasoreglas = 1)
//	begin
//		delete from Usuario where id = @id
//		set @respuesta = 1

//	end
//end



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