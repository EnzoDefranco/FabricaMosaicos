using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using MySql.Data.MySqlClient;


namespace CapaDatos
{
    public class CD_Cliente
    {
        public int Registrar(Cliente obj, out string Mensaje) // Se le pasa un obj de tipo Usuario y devuelve un entero y un string
        {
            int resultado = 0; // Se inicializa la variable idUsuarioGenerado en 0, que se utilizará para almacenar el ID del usuario generado
            Mensaje = string.Empty; // Se inicializa la variable Mensaje en vacío para almacenar mensajes de error o éxito

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
                {
                    MySqlCommand cmd = new MySqlCommand("SP_REGISTRARCLIENTE", oconexion); // Se crea una nueva instancia de MySqlCommand llamada cmd
                    cmd.CommandType = CommandType.StoredProcedure; // Es un comando de tipo procedimiento almacenado

                    // Parámetros de entrada

                    cmd.Parameters.AddWithValue("p_documento", obj.documento);
                    cmd.Parameters.AddWithValue("p_nombreCompleto", obj.nombreCompleto);
                    cmd.Parameters.AddWithValue("p_telefono", obj.telefono); // Se añade un parámetro de entrada con el valor de la propiedad documento del objeto Usuario
                    cmd.Parameters.AddWithValue("p_estado", obj.estado); // Se añade un parámetro de entrada con el valor de la propiedad estado del objeto Usuario
                    cmd.Parameters.AddWithValue("p_clienteTipo", obj.clienteTipo);
                    cmd.Parameters.AddWithValue("p_direccion", obj.direccion);
                    cmd.Parameters.AddWithValue("p_ciudad", obj.ciudad);



                    // Parámetros de salida
                    cmd.Parameters.Add("p_resultado", MySqlDbType.Int32).Direction = ParameterDirection.Output; // Se añade un parámetro de salida para almacenar el ID del usuario generado
                    cmd.Parameters.Add("p_mensaje", MySqlDbType.VarChar, 500).Direction = ParameterDirection.Output; // Se añade un parámetro de salida para almacenar mensajes de error o éxito

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToInt32(cmd.Parameters["p_resultado"].Value); // Se obtiene el valor del parámetro de salida p_idUsuarioResultado y se convierte a entero
                    Mensaje = cmd.Parameters["p_mensaje"].Value.ToString(); // Se obtiene el valor del parámetro de salida p_mensaje como cadena
                }
            }
            catch (Exception ex)
            {
                resultado = 0; // Se asigna 0 a idUsuarioGenerado en caso de error
                Mensaje = ex.Message; // Se asigna el mensaje de error a la variable Mensaje
            }

            return resultado;
        }

        public bool Editar( Cliente obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
                {
                    MySqlCommand cmd = new MySqlCommand("SP_EDITARCLIENTE", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    cmd.Parameters.AddWithValue("p_id", obj.id);
                    cmd.Parameters.AddWithValue("p_documento", obj.documento);
                    cmd.Parameters.AddWithValue("p_nombreCompleto", obj.nombreCompleto);
                    cmd.Parameters.AddWithValue("p_telefono", obj.telefono);
                    cmd.Parameters.AddWithValue("p_estado", obj.estado);
                    cmd.Parameters.AddWithValue("p_clienteTipo", obj.clienteTipo);
                    cmd.Parameters.AddWithValue("p_direccion", obj.direccion);
                    cmd.Parameters.AddWithValue("p_ciudad", obj.ciudad);



                    // Parámetros de salida
                    cmd.Parameters.Add("p_resultado", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_mensaje", MySqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["p_resultado"].Value);
                    Mensaje = cmd.Parameters["p_mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }

            return resultado;
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
                {
                    MySqlCommand cmd = new MySqlCommand("SP_ELIMINARCLIENTE", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    cmd.Parameters.AddWithValue("p_id", id);

                    // Parámetros de salida
                    cmd.Parameters.Add("p_resultado", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_mensaje", MySqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["p_resultado"].Value);
                    Mensaje = cmd.Parameters["p_mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;

            }

            return resultado;
        }

        //public List<Cliente> Listar(ClienteFiltro filtro) // Método que devuelve una lista de objetos Usuario
        //{
        //    List<Cliente> lista = new List<Cliente>(); // Se declara una lista vacía que contendrá los objetos Usuario
        //    using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena)) // Se crea una nueva instancia de MySqlConnection llamada oconexion
        //    {
        //        try
        //        {
        //            StringBuilder query = new StringBuilder();
        //            query.AppendLine("select id, documento, nombreCompleto, telefono, estado, clienteTipo, direccion, ciudad  from cliente");
        //            // Añadir condiciones opcionales para filtrar
        //            List<string> condiciones = new List<string>();

        //            // Añadir condición opcional para filtrar por tipo de cliente "Empresa"
        //            if (filtro.FiltrarPorEmpresa)
        //            {
        //                condiciones.Add("clienteTipo = 'Empresa'");
        //            }

        //            // Añadir condición opcional para filtrar por tipo de cliente "Particular"
        //            if (filtro.FiltrarPorParticular)
        //            {
        //                condiciones.Add("clienteTipo = 'Particular'");
        //            }

        //            // Si hay condiciones, añadir la cláusula WHERE
        //            if (condiciones.Count > 0)
        //            {
        //                query.AppendLine("WHERE " + string.Join(" AND ", condiciones));
        //            }

        //            MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion); // Se crea una nueva instancia de MySqlCommand llamada cmd
        //            cmd.CommandType = CommandType.Text; // Es un comando de tipo texto ya que se va a ejecutar una consulta
        //            oconexion.Open(); // Se abre la conexión a la base de datos

        //            using (MySqlDataReader dr = cmd.ExecuteReader()) // el bloque using se encarga de cerrar automáticamente el MySqlDataReader y liberar los recursos asociados.
        //            {
        //                if (dr.HasRows) // Verifica que el MySqlDataReader tenga al menos una fila
        //                {
        //                    while (dr.Read()) // Si dr tiene filas, se itera sobre cada fila utilizando un bucle while y se crea un nuevo objeto Usuario con los valores de cada columna de la fila actual.
        //                    {
        //                        lista.Add(new Cliente() // Crea un nuevo objeto Usuario con los valores de cada columna de la fila actual
        //                        {
        //                            id = Convert.ToInt32(dr["id"]), // Convierte el valor de la columna id a entero
        //                            documento = dr["documento"].ToString(),
        //                            nombreCompleto = dr["nombreCompleto"].ToString(),
        //                            telefono = dr["telefono"].ToString(), // Obtiene el valor de la columna documento como cadena
        //                            estado = Convert.ToBoolean(dr["estado"]), // Convierte el valor de la columna estado a booleano
        //                            clienteTipo = dr["clienteTipo"].ToString(),
        //                            direccion = dr["direccion"].ToString(),
        //                            ciudad = dr["ciudad"].ToString()



        //                        });
        //                    }
        //                }
        //                else
        //                {
        //                    Console.WriteLine("No se encontraron registros en la consulta.");
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Error: {ex.Message}");
        //            lista = new List<Cliente>();
        //        }
        //    }
        //    return lista;
        //}

        public List<Cliente> Listar(ClienteFiltro filtro = null) // Método que devuelve una lista de objetos Cliente
        {
            List<Cliente> lista = new List<Cliente>(); // Se declara una lista vacía que contendrá los objetos Cliente
            using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena)) // Se crea una nueva instancia de MySqlConnection llamada oconexion
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select id, documento, nombreCompleto, telefono, estado, clienteTipo, direccion, ciudad from cliente");

                    // Añadir condiciones opcionales para filtrar
                    List<string> condiciones = new List<string>();

                    if (filtro != null)
                    {
                        // Añadir condición opcional para filtrar por tipo de cliente "Empresa"
                        if (filtro.FiltrarPorEmpresa)
                        {
                            condiciones.Add("clienteTipo = 'Empresa'");
                        }

                        // Añadir condición opcional para filtrar por tipo de cliente "Particular"
                        if (filtro.FiltrarPorParticular)
                        {
                            condiciones.Add("clienteTipo = 'Particular'");
                        }
                    }

                    // Si hay condiciones, añadir la cláusula WHERE
                    if (condiciones.Count > 0)
                    {
                        query.AppendLine("WHERE " + string.Join(" AND ", condiciones));
                    }

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion); // Se crea una nueva instancia de MySqlCommand llamada cmd
                    cmd.CommandType = CommandType.Text; // Es un comando de tipo texto ya que se va a ejecutar una consulta
                    oconexion.Open(); // Se abre la conexión a la base de datos

                    using (MySqlDataReader dr = cmd.ExecuteReader()) // el bloque using se encarga de cerrar automáticamente el MySqlDataReader y liberar los recursos asociados.
                    {
                        if (dr.HasRows) // Verifica que el MySqlDataReader tenga al menos una fila
                        {
                            while (dr.Read()) // Si dr tiene filas, se itera sobre cada fila utilizando un bucle while y se crea un nuevo objeto Cliente con los valores de cada columna de la fila actual.
                            {
                                lista.Add(new Cliente() // Crea un nuevo objeto Cliente con los valores de cada columna de la fila actual
                                {
                                    id = Convert.ToInt32(dr["id"]), // Convierte el valor de la columna id a entero
                                    documento = dr["documento"].ToString(),
                                    nombreCompleto = dr["nombreCompleto"].ToString(),
                                    telefono = dr["telefono"].ToString(), // Obtiene el valor de la columna documento como cadena
                                    estado = Convert.ToBoolean(dr["estado"]), // Convierte el valor de la columna estado a booleano
                                    clienteTipo = dr["clienteTipo"].ToString(),
                                    direccion = dr["direccion"].ToString(),
                                    ciudad = dr["ciudad"].ToString()
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
                    lista = new List<Cliente>();
                }
            }
            return lista;
        }







    }
}
