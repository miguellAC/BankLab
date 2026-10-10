using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Ejemplo.Comun.DTOs.Cuentas;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.Componentes.DAT
{
    public class CuentasDAT
    {
        public List<CuentaDTO> ConsultarCuentasCliente(int clienteId)
        {
            var result = new List<CuentaDTO>();
            SqlConnection conn = null;
            try
            {
                conn = ConexionHelper.ObtenerConexion();
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, "sp_ConsultarCuentasCliente"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@ClienteId", clienteId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new CuentaDTO
                            {
                                CuentaId = ConexionHelper.LeerValor<int>(reader, "CuentaId"),
                                ClienteId = ConexionHelper.LeerValor<int>(reader, "ClienteId"),
                                NumeroCuenta = ConexionHelper.LeerValor<string>(reader, "NumeroCuenta"),
                                TipoCuenta = ConexionHelper.LeerValor<string>(reader, "TipoCuenta"),
                                Estado = ConexionHelper.LeerValor<string>(reader, "Estado")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en ConsultarCuentasCliente", ex);
                throw;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            return result;
        }

        public PaginacionDTO<MovimientoDTO> ConsultarMovimientosCuenta(int cuentaId, DateTime? desde, DateTime? hasta, int pageNum, int pageSize)
        {
            var result = new PaginacionDTO<MovimientoDTO>();
            SqlConnection conn = null;
            try
            {
                conn = ConexionHelper.ObtenerConexion();
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, "sp_ConsultarMovimientosCuenta"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@CuentaId", cuentaId);
                    ConexionHelper.AgregarParametro(cmd, "@Desde", desde);
                    ConexionHelper.AgregarParametro(cmd, "@Hasta", hasta);
                    ConexionHelper.AgregarParametro(cmd, "@PageNumber", pageNum);
                    ConexionHelper.AgregarParametro(cmd, "@PageSize", pageSize);
                    
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result.TotalRecords = ConexionHelper.LeerValor<int>(reader, "Total");
                        }
                        
                        if (reader.NextResult())
                        {
                            result.Data = new List<MovimientoDTO>();
                            while (reader.Read())
                            {
                                result.Data.Add(new MovimientoDTO
                                {
                                    MovimientoId = ConexionHelper.LeerValor<int>(reader, "MovimientoId"),
                                    CuentaId = ConexionHelper.LeerValor<int>(reader, "CuentaId"),
                                    Fecha = ConexionHelper.LeerValor<DateTime>(reader, "Fecha"),
                                    Monto = ConexionHelper.LeerValor<decimal>(reader, "Monto"),
                                    TipoMovimiento = ConexionHelper.LeerValor<string>(reader, "TipoMovimiento")
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en ConsultarMovimientosCuenta", ex);
                throw;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            return result;
        }

        public SaldoDTO ConsultarSaldoCuenta(int cuentaId)
        {
            SqlConnection conn = null;
            try
            {
                conn = ConexionHelper.ObtenerConexion();
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, "sp_ConsultarSaldoCuenta"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@CuentaId", cuentaId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SaldoDTO
                            {
                                CuentaId = cuentaId,
                                SaldoDisponible = ConexionHelper.LeerValor<decimal>(reader, "SaldoDisponible"),
                                SaldoContable = ConexionHelper.LeerValor<decimal>(reader, "SaldoContable")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en ConsultarSaldoCuenta", ex);
                throw;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            return null;
        }
    }
}
