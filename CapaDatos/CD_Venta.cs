using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management.Instrumentation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using MySql.Data.MySqlClient;


namespace CapaDatos
{
    public class CD_Venta
    {
        public int obtenerCorrelativo()
        {
            int idCorrerelativo = 0;


            using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena)) // Se crea una nueva instancia de MySqlConnection llamada oconexion
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*) + 1 from venta ");


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
        public bool Editar(Venta obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
                {
                    MySqlCommand cmd = new MySqlCommand("SP_EDITARVENTA", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("p_id", obj.id);
                    cmd.Parameters.AddWithValue("p_tipoDocumento", obj.tipoDocumento);
                    cmd.Parameters.AddWithValue("p_cumplimiento", obj.cumplimiento);
                    cmd.Parameters.AddWithValue("p_pago", obj.pago);
                    cmd.Parameters.AddWithValue("p_infoAdicional", obj.infoAdicional);
                    cmd.Parameters.AddWithValue("p_fechaRegistro", obj.fechaRegistro);

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

        public Venta ObtenerVenta(string numero)
        {
            Venta obj = new Venta();
            using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena)) // Se crea una nueva instancia de MySqlConnection llamada oconexion
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select v.id, c.documento, c.nombreCompleto, c.telefono, c.clienteTipo, c.direccion, c.ciudad, v.tipoDocumento,v.cumplimiento, v.pago,v.numeroDocumento,v.montoTotal, v.infoAdicional ,DATE_FORMAT(v.fechaRegistro, '%d/%m/%Y') AS fechaRegistro from venta v\r\n");
                    query.AppendLine("inner join cliente c on c.id = v.idCliente");
                    query.AppendLine("where v.numeroDocumento = @numeroDocumento");

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
                                obj = new Venta() // Crea un nuevo objeto Usuario con los valores de cada columna de la fila actual
                                {
                                    id = Convert.ToInt32(dr["id"]), // Convierte el valor de la columna id a entero
                                    oCliente = new Cliente()
                                    {
                                        documento = dr["documento"].ToString(),
                                        nombreCompleto = dr["nombreCompleto"].ToString(),
                                        telefono = dr["telefono"].ToString(),
                                        clienteTipo = dr["clienteTipo"].ToString(),
                                        direccion = dr["direccion"].ToString(),
                                        ciudad = dr["ciudad"].ToString(),
                                    },
                                    tipoDocumento = dr["tipoDocumento"].ToString(),
                                    cumplimiento = dr["cumplimiento"].ToString(),
                                    pago = dr["pago"].ToString(),
                                    numeroDocumento = dr["numeroDocumento"].ToString(),
                                    montoTotal = Convert.ToDecimal(dr["montoTotal"]),
                                    infoAdicional = dr["infoAdicional"].ToString(),
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
                    obj = new Venta();
                }
            }
            return obj;
        }


        public List<DetalleVenta> ObtenerDetalleVenta(int idVenta)
        {
            List<DetalleVenta> oLista = new List<DetalleVenta>(); // Se declara una lista vacía que contendrá los objetos Usuario
            using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena)) // Se crea una nueva instancia de MySqlConnection llamada oconexion
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select m.nombre AS nombre_Material,c.descripcion AS nombre_categoria,dv.precioVenta,dv.cantidad, dv.montoTotal from detalleventa dv ");
                    query.AppendLine("inner join material m on m.id = dv.idMaterial");
                    query.AppendLine("inner join categoria c on c.id = m.idCategoria");
                    query.AppendLine("where dv.idVenta = @idVenta");

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion); // Se crea una nueva instancia de MySqlCommand llamada cmd
                    cmd.Parameters.AddWithValue("@idVenta", idVenta);
                    cmd.CommandType = CommandType.Text; // Es un comando de tipo texto ya que se va a ejecutar una consulta
                    oconexion.Open(); // Se abre la conexión a la base de datos

                    using (MySqlDataReader dr = cmd.ExecuteReader()) // el bloque using se encarga de cerrar automáticamente el MySqlDataReader y liberar los recursos asociados.
                    {
                        if (dr.HasRows) // Verifica que el MySqlDataReader tenga al menos una fila
                        {
                            while (dr.Read()) // Si dr tiene filas, se itera sobre cada fila utilizando un bucle while y se crea un nuevo objeto Usuario con los valores de cada columna de la fila actual.
                            {
                                oLista.Add(new DetalleVenta() // Crea un nuevo objeto Usuario con los valores de cada columna de la fila actual
                                {
                                    oMaterial = new Material()
                                    {
                                        nombre = dr["nombre_material"].ToString(),
                                        oCategoria = new Categoria()
                                        {
                                            descripcion = dr["nombre_categoria"].ToString()
                                        }
                                    },
                                    precioVenta = Convert.ToDecimal(dr["precioVenta"]),
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
                    oLista = new List<DetalleVenta>();
                }
            }
            return oLista;
        }

        public bool Registrar(Venta obj, string detallesJson, out string Mensaje)
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
                            using (MySqlCommand cmd = new MySqlCommand("SP_REGISTRARVENTA", oconexion, transaction))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("p_idUsuario", 1);
                                cmd.Parameters.AddWithValue("p_idCliente", obj.oCliente.id);
                                cmd.Parameters.AddWithValue("p_tipoDocumento", obj.tipoDocumento);
                                cmd.Parameters.AddWithValue("p_cumplimiento", obj.cumplimiento);
                                cmd.Parameters.AddWithValue("p_pago", obj.pago);
                                cmd.Parameters.AddWithValue("p_numeroDocumento", obj.numeroDocumento);
                                cmd.Parameters.AddWithValue("p_montoTotal", obj.montoTotal);
                                cmd.Parameters.AddWithValue("p_infoAdicional", obj.infoAdicional);
                                cmd.Parameters.AddWithValue("p_fechaRegistro", obj.fechaRegistro);

                                cmd.Parameters.AddWithValue("p_detalles", detallesJson); // Usar la cadena JSON
                                cmd.Parameters.Add("p_resultado", MySqlDbType.Int32).Direction = ParameterDirection.Output;
                                cmd.Parameters.Add("p_mensaje", MySqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                                cmd.ExecuteNonQuery();

                                int idVenta = Convert.ToInt32(cmd.Parameters["p_resultado"].Value);
                                Mensaje = cmd.Parameters["p_mensaje"].Value.ToString();

                                if (idVenta <= 0)
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

        public List<Venta> Listar(VentaFiltro filtro = null)
        {
            List<Venta> lista = new List<Venta>();
            using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT v.id, v.cumplimiento, v.pago, v.infoAdicional, cl.documento,cl.nombreCompleto, cl.telefono,cl.clienteTipo, cl.direccion, cl.ciudad, v.tipoDocumento, v.numeroDocumento, v.montoTotal, DATE_FORMAT(v.fechaRegistro, '%d/%m/%Y') AS fechaRegistro");
                    query.AppendLine("FROM venta v");
                    query.AppendLine("INNER JOIN cliente cl ON cl.id = v.idCliente");

                    List<string> condiciones = new List<string>();

                    if (filtro != null)
                    {
                        if (filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
                        {
                            condiciones.Add("v.fechaRegistro BETWEEN @fechaInicio AND @fechaFin");
                        }

                        if (!string.IsNullOrEmpty(filtro.nombreCompleto))
                        {
                            condiciones.Add("cl.nombreCompleto = @nombreCompleto");
                        }

                        if (!string.IsNullOrEmpty(filtro.filtrarPorCumplimiento))
                        {
                            condiciones.Add("v.cumplimiento = @filtrarPorCumplimiento");
                        }
                        
                        if (!string.IsNullOrEmpty(filtro.filtrarPorPago))
                        {
                            condiciones.Add("v.pago = @filtrarPorPago");
                        }

                        List<string> tiposDocumento = new List<string>();
                        if (filtro.filtrarPorFactura)
                        {
                            tiposDocumento.Add("tipoDocumento = 'Empresa'");
                        }
                        if (filtro.filtrarPorBoleta)
                        {
                            tiposDocumento.Add("tipoDocumento = 'Boleta'");
                        }
                        if (filtro.filtrarPorPresupuesto)
                        {
                            tiposDocumento.Add("tipoDocumento = 'Presupuesto'");
                        }

                        if (filtro.filtrarPorEmpresa)
                        {
                            tiposDocumento.Add("clienteTipo = 'Empresa'");
                        }

                        if (filtro.filtrarPorParticular)
                        {
                            tiposDocumento.Add("clienteTipo = 'Particular'");
                        }


                        if (tiposDocumento.Count > 0)
                        {
                            condiciones.Add("(" + string.Join(" OR ", tiposDocumento) + ")");
                        }
                    }

                    if (condiciones.Count > 0)
                    {
                        query.AppendLine("WHERE " + string.Join(" AND ", condiciones));
                    }

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    if (filtro != null)
                    {

                        if (filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@fechaInicio", filtro.FechaInicio.Value);
                            cmd.Parameters.AddWithValue("@fechaFin", filtro.FechaFin.Value);
                        }

                        if (!string.IsNullOrEmpty(filtro.nombreCompleto))
                        {
                            cmd.Parameters.AddWithValue("@nombreCompleto", filtro.nombreCompleto);
                        }
                        if (!string.IsNullOrEmpty(filtro.filtrarPorCumplimiento))
                        {
                            cmd.Parameters.AddWithValue("@filtrarPorCumplimiento", filtro.filtrarPorCumplimiento);
                        }
                        if (!string.IsNullOrEmpty(filtro.filtrarPorPago))
                        {
                            cmd.Parameters.AddWithValue("@filtrarPorPago", filtro.filtrarPorPago);
                        }

                    }



                    oconexion.Open();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                lista.Add(new Venta()
                                {
                                    id = Convert.ToInt32(dr["id"]),
                                    oCliente = new Cliente()
                                    {
                                        documento = dr["documento"].ToString(),
                                        nombreCompleto = dr["nombreCompleto"].ToString(),
                                        telefono = dr["telefono"].ToString(),
                                        clienteTipo = dr["clienteTipo"].ToString(),
                                        direccion = dr["direccion"].ToString(),
                                        ciudad = dr["ciudad"].ToString()
                                    },
                                    tipoDocumento = dr["tipoDocumento"].ToString(),
                                    cumplimiento = dr["cumplimiento"].ToString(),
                                    pago = dr["pago"].ToString(),
                                    numeroDocumento = dr["numeroDocumento"].ToString(),
                                    montoTotal = Convert.ToDecimal(dr["montoTotal"]),
                                    infoAdicional = dr["infoAdicional"].ToString(),
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
                    lista = new List<Venta>();
                }
            }
            return lista;
        }

        public bool EliminarDetalleVenta(int idVenta)
        {
            bool resultado = true;
            using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("delete from detalleventa where idVenta = @idVenta");

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idVenta", idVenta);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    resultado = false;
                }
            }
            return resultado;
        }

        public bool EliminarVenta(int idVenta)
        {
            bool resultado = true;
            using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("delete from venta where id = @idVenta");

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idVenta", idVenta);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    resultado = false;
                }
            }
            return resultado;
        }

    }

}

