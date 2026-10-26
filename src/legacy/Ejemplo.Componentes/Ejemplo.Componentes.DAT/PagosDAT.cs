using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Ejemplo.Comun.DTOs.Creditos;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.Componentes.DAT
{
    public class PagosDAT
    {
        public long RegistrarPago(SqlConnection conn, SqlTransaction tx, int opId, decimal monto, string tipoPago, string canalPago, int? cuentaOrigenId, string referencia, string usuario)
        {
            try
            {
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, tx, "sp_RegistrarPago"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@OperacionId", opId);
                    ConexionHelper.AgregarParametro(cmd, "@MontoPago", monto);
                    ConexionHelper.AgregarParametro(cmd, "@TipoPago", tipoPago);
                    ConexionHelper.AgregarParametro(cmd, "@CanalPago", canalPago);
                    ConexionHelper.AgregarParametro(cmd, "@CuentaOrigenId", cuentaOrigenId);
                    ConexionHelper.AgregarParametro(cmd, "@Referencia", referencia);
                    ConexionHelper.AgregarParametro(cmd, "@Usuario", usuario);

                    SqlParameter outputId = new SqlParameter("@PagoId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(outputId);

                    cmd.ExecuteNonQuery();

                    return (long)outputId.Value;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en RegistrarPago", ex);
                throw;
            }
        }

        public List<PagoDTO> ConsultarPagosOperacion(int opId, DateTime? desde, DateTime? hasta)
        {
            var result = new List<PagoDTO>();
            SqlConnection conn = null;
            try
            {
                conn = ConexionHelper.ObtenerConexion();
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, "sp_ConsultarPagosOperacion"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@OperacionId", opId);
                    ConexionHelper.AgregarParametro(cmd, "@FechaDesde", desde);
                    ConexionHelper.AgregarParametro(cmd, "@FechaHasta", hasta);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new PagoDTO
                            {
                                PagoId = ConexionHelper.LeerValor<long>(reader, "PagoId"),
                                OperacionId = ConexionHelper.LeerValor<int>(reader, "OperacionId"),
                                FechaPago = ConexionHelper.LeerValor<DateTime>(reader, "FechaPago"),
                                MontoPago = ConexionHelper.LeerValor<decimal>(reader, "MontoPago"),
                                TipoPago = ConexionHelper.LeerValor<string>(reader, "TipoPago"),
                                Referencia = ConexionHelper.LeerValor<string>(reader, "Referencia")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en ConsultarPagosOperacion", ex);
                throw;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            return result;
        }
    }
}
