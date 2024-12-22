using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
                    query.AppendLine("select c.id, pr.documento,pr.correo, pr.razonSocial, pr.telefono, c.tipoDocumento,c.numeroDocumento,c.montoTotal,DATE_FORMAT(c.fechaRegistro, '%d/%m/%Y') AS fechaRegistro from compra c ");
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
                                        telefono = dr["telefono"].ToString(),
                                        correo = dr["correo"].ToString()
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

        //Editar compra sin su detalle y su fechaRegistro
        public decimal CalcularTotalCompras()
        {
            decimal totalMonto = 0;
            using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT SUM(c.montoTotal) AS totalMonto");
                    query.AppendLine("FROM compra c");


                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    object result = cmd.ExecuteScalar();
                    totalMonto = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al calcular el total de ventas: {ex.Message}");
                }
            }
            return totalMonto;
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


        //Eliminar detalle venta
        public bool EliminarDetalleCompra(int idCompra)
        {
            bool resultado = true;
            using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("delete from detallecompra where idCompra = @idCompra");

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idCompra", idCompra);
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

        //Eliminar compra
        public bool EliminarCompra(int idCompra)
        {
            bool resultado = true;
            using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("delete from compra where id = @idCompra");

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idCompra", idCompra);
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


        //public List<Compra> ListarPorFechas(DateTime fechaInicio, DateTime fechaFin, string razonSocial = null)
        //{
        //    List<Compra> lista = new List<Compra>();
        //    using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
        //    {
        //        try
        //        {
        //            StringBuilder query = new StringBuilder();
        //            query.AppendLine("SELECT c.id, pr.documento, pr.razonSocial, pr.telefono, c.tipoDocumento, c.numeroDocumento, c.montoTotal, DATE_FORMAT(c.fechaRegistro, '%d/%m/%Y') AS fechaRegistro");
        //            query.AppendLine("FROM compra c");
        //            query.AppendLine("INNER JOIN proveedor pr ON pr.id = c.idProveedor");
        //            query.AppendLine("WHERE c.fechaRegistro BETWEEN @fechaInicio AND @fechaFin");

        //            // Añadir condición opcional para filtrar por razón social
        //            if (!string.IsNullOrEmpty(razonSocial))
        //            {
        //                query.AppendLine("AND pr.razonSocial = @razonSocial");
        //            }

        //            MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion);
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
        //            cmd.Parameters.AddWithValue("@fechaFin", fechaFin);

        //            if (!string.IsNullOrEmpty(razonSocial))
        //            {
        //                cmd.Parameters.AddWithValue("@razonSocial", razonSocial);
        //            }

        //            oconexion.Open();

        //            using (MySqlDataReader dr = cmd.ExecuteReader())
        //            {
        //                if (dr.HasRows)
        //                {
        //                    while (dr.Read())
        //                    {
        //                        lista.Add(new Compra()
        //                        {
        //                            id = Convert.ToInt32(dr["id"]),
        //                            oProveedor = new Proveedor()
        //                            {
        //                                documento = dr["documento"].ToString(),
        //                                razonSocial = dr["razonSocial"].ToString(),
        //                                telefono = dr["telefono"].ToString()
        //                            },
        //                            tipoDocumento = dr["tipoDocumento"].ToString(),
        //                            numeroDocumento = dr["numeroDocumento"].ToString(),
        //                            montoTotal = Convert.ToDecimal(dr["montoTotal"]),
        //                            fechaRegistro = dr["fechaRegistro"].ToString()
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
        //            lista = new List<Compra>();
        //        }
        //    }
        //    return lista;
        //}

        //editar compra


        public bool Editar(Compra obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
                {
                    MySqlCommand cmd = new MySqlCommand("SP_EDITARCOMPRA", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("p_id", obj.id);
                    cmd.Parameters.AddWithValue("p_tipoDocumento", obj.tipoDocumento);
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

        //public List<Compra> Listar() // Método que devuelve una lista de objetos Usuario
        //{
        //    List<Compra> lista = new List<Compra>(); // Se declara una lista vacía que contendrá los objetos Usuario
        //    using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena)) // Se crea una nueva instancia de MySqlConnection llamada oconexion
        //    {
        //        try
        //        {

        //            StringBuilder query = new StringBuilder();
        //            query.AppendLine("select c.id, pr.documento, pr.correo, pr.razonSocial, pr.telefono, c.tipoDocumento,c.numeroDocumento,c.montoTotal,DATE_FORMAT(c.fechaRegistro, '%d/%m/%Y') AS fechaRegistro from compra c ");
        //            query.AppendLine("inner join  proveedor pr on pr.id = c.idProveedor");

        //            MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion); // Se crea una nueva instancia de MySqlCommand llamada cmd
        //            cmd.CommandType = CommandType.Text; // Es un comando de tipo texto ya que se va a ejecutar una consulta
        //            oconexion.Open(); // Se abre la conexión a la base de datos

        //            using (MySqlDataReader dr = cmd.ExecuteReader()) // el bloque using se encarga de cerrar automáticamente el MySqlDataReader y liberar los recursos asociados.
        //            {
        //                if (dr.HasRows) // Verifica que el MySqlDataReader tenga al menos una fila
        //                {
        //                    while (dr.Read()) // Si dr tiene filas, se itera sobre cada fila utilizando un bucle while y se crea un nuevo objeto Usuario con los valores de cada columna de la fila actual.
        //                    {
        //                        lista.Add(new Compra() // Crea un nuevo objeto Usuario con los valores de cada columna de la fila actual
        //                        {
        //                            id = Convert.ToInt32(dr["id"]), // Convierte el valor de la columna id a entero
        //                            oProveedor = new Proveedor()
        //                            {
        //                                documento = dr["documento"].ToString(),
        //                                razonSocial = dr["razonSocial"].ToString(),
        //                                telefono = dr["telefono"].ToString(),
        //                                correo = dr["correo"].ToString()
        //                            },
        //                            tipoDocumento = dr["tipoDocumento"].ToString(),
        //                            numeroDocumento = dr["numeroDocumento"].ToString(),
        //                            montoTotal = Convert.ToDecimal(dr["montoTotal"]),
        //                            fechaRegistro = dr["fechaRegistro"].ToString()



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
        //            lista = new List<Compra>();
        //        }
        //    }
        //    return lista;
        //}

        public (List<Compra>, decimal) Listar(CompraFiltro filtro = null)
        {
            List<Compra> lista = new List<Compra>();
            decimal totalMonto = 0;
            using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT c.id, pr.documento, pr.correo, pr.razonSocial, pr.telefono, c.tipoDocumento, c.numeroDocumento, c.montoTotal, DATE_FORMAT(c.fechaRegistro, '%d/%m/%Y') AS fechaRegistro");
                    query.AppendLine("FROM compra c");
                    query.AppendLine("INNER JOIN proveedor pr ON pr.id = c.idProveedor");

                    List<string> condiciones = new List<string>();

                    if (filtro != null)
                    {
                        if (filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
                        {
                            condiciones.Add("c.fechaRegistro BETWEEN @fechaInicio AND @fechaFin");
                        }

                        if (!string.IsNullOrEmpty(filtro.RazonSocial))
                        {
                            condiciones.Add("pr.razonSocial = @razonSocial");
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

                        if (tiposDocumento.Count > 0)
                        {
                            condiciones.Add("(" + string.Join(" OR ", tiposDocumento) + ")");
                        }
                    }

                    if (condiciones.Count > 0)
                    {
                        query.AppendLine("WHERE " + string.Join(" AND ", condiciones));
                    }

                    query.AppendLine("ORDER BY c.fechaRegistro DESC");

                    // Consulta para calcular el total de monto
                    StringBuilder queryTotal = new StringBuilder();
                    queryTotal.AppendLine("SELECT SUM(c.montoTotal) AS totalMonto");
                    queryTotal.AppendLine("FROM compra c");

                    if (condiciones.Count > 0)
                    {
                        queryTotal.AppendLine("WHERE " + string.Join(" AND ", condiciones));
                    }


                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion);
                    MySqlCommand cmdTotal = new MySqlCommand(queryTotal.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmdTotal.CommandType = CommandType.Text;


                    if (filtro != null)
                    {

                        if (filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@fechaInicio", filtro.FechaInicio.Value);
                            cmd.Parameters.AddWithValue("@fechaFin", filtro.FechaFin.Value);
                            cmdTotal.Parameters.AddWithValue("@fechaInicio", filtro.FechaInicio.Value);
                            cmdTotal.Parameters.AddWithValue("@fechaFin", filtro.FechaFin.Value);
                        }

                        if (!string.IsNullOrEmpty(filtro.RazonSocial))
                        {
                            cmd.Parameters.AddWithValue("@razonSocial", filtro.RazonSocial);
                            cmdTotal.Parameters.AddWithValue("@razonSocial", filtro.RazonSocial);
                        }
                    }



                    oconexion.Open();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                lista.Add(new Compra()
                                {
                                    id = Convert.ToInt32(dr["id"]),
                                    oProveedor = new Proveedor()
                                    {
                                        documento = dr["documento"].ToString(),
                                        razonSocial = dr["razonSocial"].ToString(),
                                        telefono = dr["telefono"].ToString(),
                                        correo = dr["correo"].ToString()
                                    },
                                    tipoDocumento = dr["tipoDocumento"].ToString(),
                                    numeroDocumento = dr["numeroDocumento"].ToString(),
                                    montoTotal = Convert.ToDecimal(dr["montoTotal"]),
                                    fechaRegistro = dr["fechaRegistro"].ToString()
                                });
                            }
                        }
                        //else
                        //{
                        //    Console.WriteLine("No se encontraron registros en la consulta.");
                        //}
                    }
                    //Ejecutar consulta para calcular el total de monto
                    object result = cmdTotal.ExecuteScalar();
                    totalMonto = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    lista = new List<Compra>();
                }
            }
            return (lista, totalMonto);
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

                                cmd.Parameters.AddWithValue("p_idUsuario", 1);
                                cmd.Parameters.AddWithValue("p_idProveedor", obj.oProveedor.id);
                                cmd.Parameters.AddWithValue("p_tipoDocumento", obj.tipoDocumento);
                                cmd.Parameters.AddWithValue("p_numeroDocumento", obj.numeroDocumento);
                                cmd.Parameters.AddWithValue("p_montoTotal", obj.montoTotal);
                                cmd.Parameters.AddWithValue("p_fechaRegistro", obj.fechaRegistro);

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
