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
    public class CD_Compra
    {
        public int obtenerCorrelativo()
        {
            int idCorrerelativo = 0;


            using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena)) // Se crea una nueva instancia de MySqlConnection llamada oconexion
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*) + 1 from compra ");
  

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion); // Se crea una nueva instancia de MySqlCommand llamada cmd
                    cmd.CommandType = CommandType.Text; // Es un comando de tipo texto ya que se va a ejecutar una consulta
                    oconexion.Open(); // Se abre la conexión a la base de datos

                    idCorrerelativo = Convert.ToInt32(cmd.ExecuteScalar());

                }
                catch (Exception ex)
                {
                    idCorrerelativo = 0;
                }
            }


           return idCorrerelativo;

        }

        public Compra ObtenerCompra(string numero)
        {
            Compra obj = new Compra();
            using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena)) // Se crea una nueva instancia de MySqlConnection llamada oconexion
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select c.id, pr.documento, pr.razonSocial, pr.telefono, c.tipoDocumento,c.numeroDocumento,c.montoTotal,DATE_FORMAT(c.fechaRegistro, '%d/%m/%Y') AS fechaRegistro from compra c ");
                    query.AppendLine("inner join proveedor pr on pr.id = c.idProveedor");
                    query.AppendLine("where c.numeroDocumento = @numeroDocumento");

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion); // Se crea una nueva instancia de MySqlCommand llamada cmd
                    cmd.Parameters.AddWithValue("@numeroDocumento", numero);
                    cmd.CommandType = CommandType.Text; // Es un comando de tipo texto ya que se va a ejecutar una consulta
                    oconexion.Open(); // Se abre la conexión a la base de datos

                    using (MySqlDataReader dr = cmd.ExecuteReader()) // el bloque using se encarga de cerrar automáticamente el MySqlDataReader y liberar los recursos asociados.
                    {
                        if (dr.HasRows) // Verifica que el MySqlDataReader tenga al menos una fila
                        {
                            while (dr.Read()) // Si dr tiene filas, se itera sobre cada fila utilizando un bucle while y se crea un nuevo objeto Usuario con los valores de cada columna de la fila actual.
                            {
                                obj = new Compra() // Crea un nuevo objeto Usuario con los valores de cada columna de la fila actual
                                {
                                    id = Convert.ToInt32(dr["id"]), // Convierte el valor de la columna id a entero
                                    oProveedor = new Proveedor()
                                    {
                                        documento = dr["documento"].ToString(),
                                        razonSocial = dr["razonSocial"].ToString(),
                                        telefono = dr["telefono"].ToString()
                                    },
                                    tipoDocumento = dr["tipoDocumento"].ToString(),
                                    numeroDocumento = dr["numeroDocumento"].ToString(),
                                    montoTotal = Convert.ToDecimal(dr["montoTotal"]),
                                    fechaRegistro = dr["fechaRegistro"].ToString()
                                };
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
                    obj = new Compra();
                }
            }
            return obj;
        }

        public List<DetalleCompra> ObtenerDetalleCompra(int idCompra)
        {
            List<DetalleCompra> oLista = new List<DetalleCompra>(); // Se declara una lista vacía que contendrá los objetos Usuario
            using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena)) // Se crea una nueva instancia de MySqlConnection llamada oconexion
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select m.nombre, dc.precioCompra,dc.cantidad, dc.montoTotal from detallecompra dc ");
                    query.AppendLine("inner join material m on m.id = dc.idMaterial");
                    query.AppendLine("where dc.idCompra = @idCompra");

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion); // Se crea una nueva instancia de MySqlCommand llamada cmd
                    cmd.Parameters.AddWithValue("@idCompra", idCompra);
                    cmd.CommandType = CommandType.Text; // Es un comando de tipo texto ya que se va a ejecutar una consulta
                    oconexion.Open(); // Se abre la conexión a la base de datos

                    using (MySqlDataReader dr = cmd.ExecuteReader()) // el bloque using se encarga de cerrar automáticamente el MySqlDataReader y liberar los recursos asociados.
                    {
                        if (dr.HasRows) // Verifica que el MySqlDataReader tenga al menos una fila
                        {
                            while (dr.Read()) // Si dr tiene filas, se itera sobre cada fila utilizando un bucle while y se crea un nuevo objeto Usuario con los valores de cada columna de la fila actual.
                            {
                                oLista.Add(new DetalleCompra() // Crea un nuevo objeto Usuario con los valores de cada columna de la fila actual
                                {
                                    oMaterial = new Material()
                                    {
                                        nombre = dr["nombre"].ToString()
                                    },
                                    precioCompra = Convert.ToDecimal(dr["precioCompra"]),
                                    cantidad = Convert.ToInt32(dr["cantidad"]),
                                    montoTotal = Convert.ToDecimal(dr["montoTotal"])
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
                    oLista = new List<DetalleCompra>();
                }
            }
            return oLista;
        }


        public List<Compra> Listar() // Método que devuelve una lista de objetos Usuario
        {
            List<Compra> lista = new List<Compra>(); // Se declara una lista vacía que contendrá los objetos Usuario
            using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena)) // Se crea una nueva instancia de MySqlConnection llamada oconexion
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select c.id, pr.documento, pr.razonSocial, pr.telefono, c.tipoDocumento,c.numeroDocumento,c.montoTotal,DATE_FORMAT(c.fechaRegistro, '%d/%m/%Y') AS fechaRegistro from compra c ");
                    query.AppendLine("inner join  proveedor pr on pr.id = c.idProveedor");

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion); // Se crea una nueva instancia de MySqlCommand llamada cmd
                    cmd.CommandType = CommandType.Text; // Es un comando de tipo texto ya que se va a ejecutar una consulta
                    oconexion.Open(); // Se abre la conexión a la base de datos

                    using (MySqlDataReader dr = cmd.ExecuteReader()) // el bloque using se encarga de cerrar automáticamente el MySqlDataReader y liberar los recursos asociados.
                    {
                        if (dr.HasRows) // Verifica que el MySqlDataReader tenga al menos una fila
                        {
                            while (dr.Read()) // Si dr tiene filas, se itera sobre cada fila utilizando un bucle while y se crea un nuevo objeto Usuario con los valores de cada columna de la fila actual.
                            {
                                lista.Add(new Compra() // Crea un nuevo objeto Usuario con los valores de cada columna de la fila actual
                                {
                                    id = Convert.ToInt32(dr["id"]), // Convierte el valor de la columna id a entero
                                    oProveedor = new Proveedor()
                                    {
                                        documento = dr["documento"].ToString(),
                                        razonSocial = dr["razonSocial"].ToString(),
                                        telefono = dr["telefono"].ToString()
                                    },
                                    tipoDocumento = dr["tipoDocumento"].ToString(),
                                    numeroDocumento = dr["numeroDocumento"].ToString(),
                                    montoTotal = Convert.ToDecimal(dr["montoTotal"]),
                                    fechaRegistro = dr["fechaRegistro"].ToString()



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
                    lista = new List<Compra>();
                }
            }
            return lista;
        }
        public bool Registrar(Compra obj, string detallesJson, out string Mensaje)
        {
            bool resultado = true;
            Mensaje = string.Empty;

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
                {
                    oconexion.Open();
                    using (MySqlTransaction transaction = oconexion.BeginTransaction())
                    {
                        try
                        {
                            // Insertar la compra principal
                            using (MySqlCommand cmd = new MySqlCommand("SP_REGISTRARCOMPRA", oconexion, transaction))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("p_idUsuario", obj.oUsuario.id);
                                cmd.Parameters.AddWithValue("p_idProveedor", obj.oProveedor.id);
                                cmd.Parameters.AddWithValue("p_tipoDocumento", obj.tipoDocumento);
                                cmd.Parameters.AddWithValue("p_numeroDocumento", obj.numeroDocumento);
                                cmd.Parameters.AddWithValue("p_montoTotal", obj.montoTotal);

                                cmd.Parameters.AddWithValue("p_detalles", detallesJson); // Usar la cadena JSON
                                cmd.Parameters.Add("p_resultado", MySqlDbType.Int32).Direction = ParameterDirection.Output;
                                cmd.Parameters.Add("p_mensaje", MySqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                                cmd.ExecuteNonQuery();

                                int idCompra = Convert.ToInt32(cmd.Parameters["p_resultado"].Value);
                                Mensaje = cmd.Parameters["p_mensaje"].Value.ToString();

                                if (idCompra <= 0)
                                {
                                    resultado = false;
                                    transaction.Rollback();
                                    return resultado;
                                }

                                transaction.Commit();
                            }
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }

            return resultado;
        }
    }
}