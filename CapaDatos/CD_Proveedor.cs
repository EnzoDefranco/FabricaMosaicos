﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using MySql.Data.MySqlClient;

namespace CapaDatos
{
    public class CD_Proveedor
    {
        public List<Proveedor> Listar() // Método que devuelve una lista de objetos Usuario
        {
            List<Proveedor> lista = new List<Proveedor>(); // Se declara una lista vacía que contendrá los objetos Usuario
            using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena)) // Se crea una nueva instancia de MySqlConnection llamada oconexion
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select id, documento, razonSocial, correo, telefono, estado from proveedor");

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion); // Se crea una nueva instancia de MySqlCommand llamada cmd
                    cmd.CommandType = CommandType.Text; // Es un comando de tipo texto ya que se va a ejecutar una consulta
                    oconexion.Open(); // Se abre la conexión a la base de datos

                    using (MySqlDataReader dr = cmd.ExecuteReader()) // el bloque using se encarga de cerrar automáticamente el MySqlDataReader y liberar los recursos asociados.
                    {
                        if (dr.HasRows) // Verifica que el MySqlDataReader tenga al menos una fila
                        {
                            while (dr.Read()) // Si dr tiene filas, se itera sobre cada fila utilizando un bucle while y se crea un nuevo objeto Usuario con los valores de cada columna de la fila actual.
                            {
                                lista.Add(new Proveedor() // Crea un nuevo objeto Usuario con los valores de cada columna de la fila actual
                                {
                                    id = Convert.ToInt32(dr["id"]), // Convierte el valor de la columna id a entero
                                    documento = dr["documento"].ToString(),
                                    razonSocial = dr["razonSocial"].ToString(),
                                    correo = dr["correo"].ToString(), // Obtiene el valor de la columna documento como cadena
                                    telefono = dr["telefono"].ToString(), // Obtiene el valor de la columna documento como cadena
                                    estado = Convert.ToBoolean(dr["estado"]), // Convierte el valor de la columna estado a booleano
                                    


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
                    lista = new List<Proveedor>();
                }
            }
            return lista;
        }

        public int Registrar(Proveedor obj, out string Mensaje) // Se le pasa un obj de tipo Usuario y devuelve un entero y un string
        {
            int resultado = 0; // Se inicializa la variable idUsuarioGenerado en 0, que se utilizará para almacenar el ID del usuario generado
            Mensaje = string.Empty; // Se inicializa la variable Mensaje en vacío para almacenar mensajes de error o éxito

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
                {
                    MySqlCommand cmd = new MySqlCommand("SP_REGISTRARPROVEEDOR", oconexion); // Se crea una nueva instancia de MySqlCommand llamada cmd
                    cmd.CommandType = CommandType.StoredProcedure; // Es un comando de tipo procedimiento almacenado

                    // Parámetros de entrada

                    cmd.Parameters.AddWithValue("p_documento", obj.documento);
                    cmd.Parameters.AddWithValue("p_razonSocial", obj.razonSocial);
                    cmd.Parameters.AddWithValue("p_correo", obj.correo); // Se añade un parámetro de entrada con el valor de la propiedad documento del objeto Usuario
                    cmd.Parameters.AddWithValue("p_telefono", obj.telefono); // Se añade un parámetro de entrada con el valor de la propiedad documento del objeto Usuario
                    cmd.Parameters.AddWithValue("p_estado", obj.estado); // Se añade un parámetro de entrada con el valor de la propiedad estado del objeto Usuario



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

        public bool Editar(Proveedor obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
                {
                    MySqlCommand cmd = new MySqlCommand("SP_EDITARPROVEEDOR", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    cmd.Parameters.AddWithValue("p_id", obj.id);
                    cmd.Parameters.AddWithValue("p_documento", obj.documento);
                    cmd.Parameters.AddWithValue("p_razonSocial", obj.razonSocial);
                    cmd.Parameters.AddWithValue("p_correo", obj.correo);
                    cmd.Parameters.AddWithValue("p_telefono", obj.telefono);
                    cmd.Parameters.AddWithValue("p_estado", obj.estado);
    


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
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
                {
                    MySqlCommand cmd = new MySqlCommand("SP_ELIMINARPROVEEDOR", oconexion);
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
