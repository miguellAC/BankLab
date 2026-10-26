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
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, "sp_ReporteCartera"))
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
                                NombreProducto = ConexionHelper.LeerValor<string>(reader, "TipoProducto"),
                                CategoriaRiesgo = ConexionHelper.LeerValor<string>(reader, "CategoriaRiesgo"),
                                SaldoTotal = ConexionHelper.LeerValor<decimal>(reader, "SaldoTotal"),
                                SaldoCapitalVigente = ConexionHelper.LeerValor<decimal>(reader, "CapitalVigente"),
                                SaldoCapitalVencido = ConexionHelper.LeerValor<decimal>(reader, "CapitalVencido")
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
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, "sp_ReporteCarteraVencida"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@DiasVencidoMinimo", diasMinimo);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new ReporteCarteraDTO
                            {
                                NumeroOperacion = ConexionHelper.LeerValor<string>(reader, "NumeroOperacion"),
                                NombreCliente = ConexionHelper.LeerValor<string>(reader, "Cliente"),
                                NumeroIdentificacion = ConexionHelper.LeerValor<string>(reader, "NumeroIdentificacion"),
                                NombreSucursal = ConexionHelper.LeerValor<string>(reader, "Sucursal"),
                                DiasEnMora = ConexionHelper.LeerValor<int>(reader, "DiasEnMora"),
                                SaldoCapitalVencido = ConexionHelper.LeerValor<decimal>(reader, "SaldoCapitalVencido"),
                                SaldoTotal = ConexionHelper.LeerValor<decimal>(reader, "TotalVencido")
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
