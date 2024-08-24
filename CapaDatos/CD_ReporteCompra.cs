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
    public class CD_ReporteCompra
    {
        public List<ReporteCompra> ObtenerReporteCompra(DateTime fechaInicio, DateTime fechaFin, string razonSocial)
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
                    query.AppendLine("WHERE c.fechaRegistro BETWEEN @fechaInicio AND @fechaFin");

                    // Condición opcional para filtrar por razón social
                    if (!string.IsNullOrEmpty(razonSocial))
                    {
                        query.AppendLine("AND pr.razonSocial = @razonSocial");
                    }

                    query.AppendLine("GROUP BY m.nombre;");

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fechaFin", fechaFin);

                    if (!string.IsNullOrEmpty(razonSocial))
                    {
                        cmd.Parameters.AddWithValue("@razonSocial", razonSocial);
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
