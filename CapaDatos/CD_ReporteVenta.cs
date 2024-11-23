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
    public class CD_ReporteVenta
    {
        //public List<ReporteVenta> ObtenerReporteVenta(VentaFiltro filtro = null)
        //{
        //    List<ReporteVenta> lstReporteVenta = new List<ReporteVenta>();
        //    using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
        //    {
        //        try
        //        {
        //            StringBuilder query = new StringBuilder();
        //            query.AppendLine("SELECT m.nombre AS Producto, SUM(dv.cantidad) AS CantidadTotal");
        //            query.AppendLine("FROM detalleventa dv");
        //            query.AppendLine("INNER JOIN material m ON m.id = dv.idMaterial");
        //            query.AppendLine("INNER JOIN venta v ON v.id = dv.idVenta");
        //            query.AppendLine("INNER JOIN cliente cl ON cl.id = v.idCliente");

        //            List<string> condiciones = new List<string>();

        //            if (filtro != null)
        //            {
        //                if (filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
        //                {
        //                    condiciones.Add("v.fechaRegistro BETWEEN @fechaInicio AND @fechaFin");
        //                }

        //                if (!string.IsNullOrEmpty(filtro.nombreCompleto))
        //                {
        //                    condiciones.Add("cl.nombreCompleto = @nombreCompleto");
        //                }
        //                if (!string.IsNullOrEmpty(filtro.filtrarPorCumplimiento))
        //                {
        //                    condiciones.Add("v.cumplimiento = @filtrarPorCumplimiento");
        //                }

        //                if (!string.IsNullOrEmpty(filtro.filtrarPorPago))
        //                {
        //                    condiciones.Add("v.pago = @filtrarPorPago");
        //                }

        //                List<string> tiposDocumento = new List<string>();
        //                if (filtro.filtrarPorFactura)
        //                {
        //                    tiposDocumento.Add("tipoDocumento = 'Empresa'");
        //                }
        //                if (filtro.filtrarPorBoleta)
        //                {
        //                    tiposDocumento.Add("tipoDocumento = 'Boleta'");
        //                }
        //                if (filtro.filtrarPorPresupuesto)
        //                {
        //                    tiposDocumento.Add("tipoDocumento = 'Presupuesto'");
        //                }

        //                if (filtro.filtrarPorEmpresa)
        //                {
        //                    tiposDocumento.Add("clienteTipo = 'Empresa'");
        //                }

        //                if (filtro.filtrarPorParticular)
        //                {
        //                    tiposDocumento.Add("clienteTipo = 'Particular'");
        //                }

        //                if (tiposDocumento.Count > 0)
        //                {
        //                    condiciones.Add("(" + string.Join(" OR ", tiposDocumento) + ")");
        //                }
        //            }



        //            if (condiciones.Count > 0)
        //            {
        //                query.AppendLine("WHERE " + string.Join(" AND ", condiciones));
        //            }

        //            query.AppendLine("GROUP BY m.nombre;");

        //            MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion);
        //            cmd.CommandType = CommandType.Text;
        //            if (filtro != null)
        //            {
        //                if (filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
        //                {
        //                    cmd.Parameters.AddWithValue("@fechaInicio", filtro.FechaInicio.Value);
        //                    cmd.Parameters.AddWithValue("@fechaFin", filtro.FechaFin.Value);
        //                }

        //                if (!string.IsNullOrEmpty(filtro.nombreCompleto))
        //                {
        //                    cmd.Parameters.AddWithValue("@nombreCompleto", filtro.nombreCompleto);
        //                }
        //                if (!string.IsNullOrEmpty(filtro.filtrarPorCumplimiento))
        //                {
        //                    cmd.Parameters.AddWithValue("@filtrarPorCumplimiento", filtro.filtrarPorCumplimiento);
        //                }
        //                if (!string.IsNullOrEmpty(filtro.filtrarPorPago))
        //                {
        //                    cmd.Parameters.AddWithValue("@filtrarPorPago", filtro.filtrarPorPago);
        //                }
        //            }

        //            oconexion.Open();

        //            using (MySqlDataReader dr = cmd.ExecuteReader())
        //            {
        //                if (dr.HasRows)
        //                {
        //                    while (dr.Read())
        //                    {
        //                        lstReporteVenta.Add(new ReporteVenta()
        //                        {
        //                            Producto = dr["Producto"].ToString(),
        //                            CantidadTotal = Convert.ToDecimal(dr["CantidadTotal"])
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
        //            lstReporteVenta = new List<ReporteVenta>();
        //        }
        //    }
        //    return lstReporteVenta;
        //}
        public List<ReporteVenta> ObtenerReporteVenta(VentaFiltro filtro = null)
        {
            List<ReporteVenta> lstReporteVenta = new List<ReporteVenta>();
            using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT m.nombre AS Producto, SUM(dv.cantidad) AS CantidadTotal");
                    query.AppendLine("FROM detalleventa dv");
                    query.AppendLine("INNER JOIN material m ON m.id = dv.idMaterial");
                    query.AppendLine("INNER JOIN venta v ON v.id = dv.idVenta");
                    query.AppendLine("INNER JOIN cliente cl ON cl.id = v.idCliente");

                    List<string> condiciones = new List<string>();

                    if (filtro != null)
                    {
                        // Filtro por rango de fechas
                        if (filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
                        {
                            condiciones.Add("v.fechaRegistro BETWEEN @fechaInicio AND @fechaFin");
                        }

                        // Filtro por nombre del cliente
                        if (!string.IsNullOrEmpty(filtro.nombreCompleto))
                        {
                            condiciones.Add("cl.nombreCompleto = @nombreCompleto");
                        }

                        // Filtro por cumplimiento
                        if (!string.IsNullOrEmpty(filtro.filtrarPorCumplimiento))
                        {
                            condiciones.Add("v.cumplimiento = @filtrarPorCumplimiento");
                        }

                        // Filtro por pago
                        if (!string.IsNullOrEmpty(filtro.filtrarPorPago))
                        {
                            condiciones.Add("v.pago = @filtrarPorPago");
                        }

                        // Filtros dinámicos para tipo de documento
                        List<string> tiposDocumento = new List<string>();
                        if (filtro.filtrarPorFactura)
                        {
                            tiposDocumento.Add("v.tipoDocumento = 'Factura'");
                        }
                        if (filtro.filtrarPorBoleta)
                        {
                            tiposDocumento.Add("v.tipoDocumento = 'Boleta'");
                        }
                        if (filtro.filtrarPorPresupuesto)
                        {
                            tiposDocumento.Add("v.tipoDocumento = 'Presupuesto'");
                        }

                        // Filtros dinámicos para tipo de cliente
                        if (filtro.filtrarPorEmpresa)
                        {
                            tiposDocumento.Add("cl.clienteTipo = 'Empresa'");
                        }
                        if (filtro.filtrarPorParticular)
                        {
                            tiposDocumento.Add("cl.clienteTipo = 'Particular'");
                        }

                        if (tiposDocumento.Count > 0)
                        {
                            condiciones.Add("(" + string.Join(" OR ", tiposDocumento) + ")");
                        }

                        // Filtro por finalizado
                        if (filtro.filtrarPorFinalizado)
                        {
                            condiciones.Add("(v.cumplimiento = 'Entregado' AND v.pago = 'Pago')");
                        }
                    }

                    // Agregar las condiciones a la consulta
                    if (condiciones.Count > 0)
                    {
                        query.AppendLine("WHERE " + string.Join(" AND ", condiciones));
                    }

                    query.AppendLine("GROUP BY m.nombre;");

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Añadir parámetros dinámicos
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

                    // Ejecutar la consulta principal
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                lstReporteVenta.Add(new ReporteVenta()
                                {
                                    Producto = dr["Producto"].ToString(),
                                    CantidadTotal = Convert.ToDecimal(dr["CantidadTotal"])
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    lstReporteVenta = new List<ReporteVenta>();
                }
            }
            return lstReporteVenta;
        }



    }
    }
