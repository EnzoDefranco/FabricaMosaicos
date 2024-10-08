﻿using System;
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
    public class CD_ReporteCompra
    {
        public List<ReporteCompra> ObtenerReporteCompra(CompraFiltro filtro = null)
        {
            List<ReporteCompra> lstReporteCompra = new List<ReporteCompra>();
            using (MySqlConnection oconexion = new MySqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT m.nombre AS nombreMaterial, SUM(dc.cantidad) AS totalCantidad");
                    query.AppendLine("FROM detallecompra dc");
                    query.AppendLine("INNER JOIN material m ON m.id = dc.idMaterial");
                    query.AppendLine("INNER JOIN compra c ON c.id = dc.idCompra");
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
                            tiposDocumento.Add("c.tipoDocumento = 'Factura'");
                        }
                        if (filtro.filtrarPorBoleta)
                        {
                            tiposDocumento.Add("c.tipoDocumento = 'Boleta'");
                        }
                        if (filtro.filtrarPorPresupuesto)
                        {
                            tiposDocumento.Add("c.tipoDocumento = 'Presupuesto'");
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

                    query.AppendLine("GROUP BY m.nombre;");

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    if (filtro != null)
                    {
                        if (filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@fechaInicio", filtro.FechaInicio.Value);
                            cmd.Parameters.AddWithValue("@fechaFin", filtro.FechaFin.Value);
                        }

                        if (!string.IsNullOrEmpty(filtro.RazonSocial))
                        {
                            cmd.Parameters.AddWithValue("@razonSocial", filtro.RazonSocial);
                        }
                    }

                    oconexion.Open();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                lstReporteCompra.Add(new ReporteCompra()
                                {
                                    nombreMaterial = dr["nombreMaterial"].ToString(),
                                    totalCantidad = Convert.ToDecimal(dr["totalCantidad"])
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
                    lstReporteCompra = new List<ReporteCompra>();
                }
            }
            return lstReporteCompra;
        }


    }
}
