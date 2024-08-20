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
