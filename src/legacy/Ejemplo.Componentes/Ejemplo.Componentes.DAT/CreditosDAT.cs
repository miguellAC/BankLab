using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Ejemplo.Comun.DTOs.Creditos;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.Componentes.DAT
{
    public class CreditosDAT
    {
        public OperacionCreditoDTO ConsultarOperacionCredito(int? opId, string numOp)
        {
            SqlConnection conn = null;
            try
            {
                conn = ConexionHelper.ObtenerConexion();
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, "sp_ConsultarOperacionCredito"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@OperacionId", opId);
                    ConexionHelper.AgregarParametro(cmd, "@NumeroOperacion", numOp);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new OperacionCreditoDTO
                            {
                                OperacionId = ConexionHelper.LeerValor<int>(reader, "OperacionId"),
                                NumeroOperacion = ConexionHelper.LeerValor<string>(reader, "NumeroOperacion"),
                                ClienteId = ConexionHelper.LeerValor<int>(reader, "ClienteId"),
                                MontoDesembolsado = ConexionHelper.LeerValor<decimal>(reader, "MontoDesembolsado"),
                                TasaAnual = ConexionHelper.LeerValor<decimal>(reader, "TasaAnual"),
                                Estado = ConexionHelper.LeerValor<string>(reader, "Estado")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en ConsultarOperacionCredito", ex);
                throw;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            return null;
        }

        public SaldoOperacionDTO ConsultarSaldosVigenteVencido(int opId)
        {
            SqlConnection conn = null;
            try
            {
                conn = ConexionHelper.ObtenerConexion();
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, "sp_ConsultarSaldosVigenteVencido"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@OperacionId", opId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SaldoOperacionDTO
                            {
                                OperacionId = opId,
                                SaldoCapitalVigente = ConexionHelper.LeerValor<decimal>(reader, "SaldoCapitalVigente"),
                                SaldoCapitalVencido = ConexionHelper.LeerValor<decimal>(reader, "SaldoCapitalVencido")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en ConsultarSaldosVigenteVencido", ex);
                throw;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            return null;
        }

        public List<CuotaDTO> ConsultarCuotasOperacion(int opId, bool soloVencidas, bool soloPendientes)
        {
            var result = new List<CuotaDTO>();
            SqlConnection conn = null;
            try
            {
                conn = ConexionHelper.ObtenerConexion();
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, "sp_ConsultarCuotasOperacion"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@OperacionId", opId);
                    ConexionHelper.AgregarParametro(cmd, "@SoloVencidas", soloVencidas);
                    ConexionHelper.AgregarParametro(cmd, "@SoloPendientes", soloPendientes);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new CuotaDTO
                            {
                                CuotaId = ConexionHelper.LeerValor<int>(reader, "CuotaId"),
                                OperacionId = ConexionHelper.LeerValor<int>(reader, "OperacionId"),
                                NumeroCuota = ConexionHelper.LeerValor<int>(reader, "NumeroCuota"),
                                FechaVencimiento = ConexionHelper.LeerValor<DateTime>(reader, "FechaVencimiento"),
                                CapitalCuota = ConexionHelper.LeerValor<decimal>(reader, "CapitalCuota"),
                                InteresCuota = ConexionHelper.LeerValor<decimal>(reader, "InteresCuota"),
                                Estado = ConexionHelper.LeerValor<string>(reader, "Estado")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en ConsultarCuotasOperacion", ex);
                throw;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            return result;
        }

        public List<CuotaDTO> ConsultarCuotasPendientesVencidas(int? clienteId)
        {
            var result = new List<CuotaDTO>();
            SqlConnection conn = null;
            try
            {
                conn = ConexionHelper.ObtenerConexion();
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, "sp_ConsultarCuotasPendientesVencidas"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@ClienteId", clienteId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new CuotaDTO
                            {
                                CuotaId = ConexionHelper.LeerValor<int>(reader, "CuotaId"),
                                OperacionId = ConexionHelper.LeerValor<int>(reader, "OperacionId"),
                                NumeroCuota = ConexionHelper.LeerValor<int>(reader, "NumeroCuota"),
                                FechaVencimiento = ConexionHelper.LeerValor<DateTime>(reader, "FechaVencimiento"),
                                CapitalCuota = ConexionHelper.LeerValor<decimal>(reader, "CapitalCuota"),
                                InteresCuota = ConexionHelper.LeerValor<decimal>(reader, "InteresCuota"),
                                Estado = ConexionHelper.LeerValor<string>(reader, "Estado")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en ConsultarCuotasPendientesVencidas", ex);
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
