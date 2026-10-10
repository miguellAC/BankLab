using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Ejemplo.Comun.DTOs.Cartera;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.Componentes.DAT
{
    public class CarteraDAT
    {
        public List<ReporteCarteraDTO> ObtenerReporteCartera(DateTime? fechaCorte, int? sucursalId, string estado, string catRiesgo)
        {
            var result = new List<ReporteCarteraDTO>();
            SqlConnection conn = null;
            try
            {
                conn = ConexionHelper.ObtenerConexion();
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, "sp_ObtenerReporteCartera"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@FechaCorte", fechaCorte);
                    ConexionHelper.AgregarParametro(cmd, "@SucursalId", sucursalId);
                    ConexionHelper.AgregarParametro(cmd, "@Estado", estado);
                    ConexionHelper.AgregarParametro(cmd, "@CategoriaRiesgo", catRiesgo);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new ReporteCarteraDTO
                            {
                                OperacionId = ConexionHelper.LeerValor<int>(reader, "OperacionId"),
                                NumeroOperacion = ConexionHelper.LeerValor<string>(reader, "NumeroOperacion"),
                                ClienteId = ConexionHelper.LeerValor<int>(reader, "ClienteId"),
                                SaldoCapital = ConexionHelper.LeerValor<decimal>(reader, "SaldoCapital"),
                                DiasMora = ConexionHelper.LeerValor<int>(reader, "DiasMora"),
                                CategoriaRiesgo = ConexionHelper.LeerValor<string>(reader, "CategoriaRiesgo")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en ObtenerReporteCartera", ex);
                throw;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            return result;
        }

        public List<ReporteCarteraDTO> ObtenerReporteCarteraVencida(int diasMinimo)
        {
            var result = new List<ReporteCarteraDTO>();
            SqlConnection conn = null;
            try
            {
                conn = ConexionHelper.ObtenerConexion();
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, "sp_ObtenerReporteCarteraVencida"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@DiasMinimoMora", diasMinimo);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new ReporteCarteraDTO
                            {
                                OperacionId = ConexionHelper.LeerValor<int>(reader, "OperacionId"),
                                NumeroOperacion = ConexionHelper.LeerValor<string>(reader, "NumeroOperacion"),
                                ClienteId = ConexionHelper.LeerValor<int>(reader, "ClienteId"),
                                SaldoCapital = ConexionHelper.LeerValor<decimal>(reader, "SaldoCapital"),
                                DiasMora = ConexionHelper.LeerValor<int>(reader, "DiasMora"),
                                CategoriaRiesgo = ConexionHelper.LeerValor<string>(reader, "CategoriaRiesgo")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en ObtenerReporteCarteraVencida", ex);
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
