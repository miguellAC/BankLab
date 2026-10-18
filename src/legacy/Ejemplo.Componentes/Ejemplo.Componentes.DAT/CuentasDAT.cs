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
                                NombreCliente = ConexionHelper.LeerValor<string>(reader, "NombreCliente"),
                                ProductoId = ConexionHelper.LeerValor<int>(reader, "ProductoId"),
                                NombreProducto = ConexionHelper.LeerValor<string>(reader, "NombreProducto"),
                                TipoCuentaId = ConexionHelper.LeerValor<int>(reader, "TipoCuentaId"),
                                NombreTipoCuenta = ConexionHelper.LeerValor<string>(reader, "NombreTipoCuenta"),
                                SucursalId = ConexionHelper.LeerValor<int>(reader, "SucursalId"),
                                NombreSucursal = ConexionHelper.LeerValor<string>(reader, "NombreSucursal"),
                                Saldo = ConexionHelper.LeerValor<decimal>(reader, "Saldo"),
                                SaldoDisponible = ConexionHelper.LeerValor<decimal>(reader, "SaldoDisponible"),
                                Estado = ConexionHelper.LeerValor<string>(reader, "Estado"),
                                FechaApertura = ConexionHelper.LeerValor<DateTime>(reader, "FechaApertura"),
                                Moneda = ConexionHelper.LeerValor<string>(reader, "Moneda")
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
                            result.Items = new List<MovimientoDTO>();
                            while (reader.Read())
                            {
                                result.Items.Add(new MovimientoDTO
                                {
                                    MovimientoId = ConexionHelper.LeerValor<long>(reader, "MovimientoId"),
                                    CuentaId = ConexionHelper.LeerValor<int>(reader, "CuentaId"),
                                    TipoTransaccionId = ConexionHelper.LeerValor<int>(reader, "TipoTransaccionId"),
                                    NombreTipoTransaccion = ConexionHelper.LeerValor<string>(reader, "NombreTipoTransaccion"),
                                    Naturaleza = ConexionHelper.LeerValor<char>(reader, "Naturaleza"),
                                    Monto = ConexionHelper.LeerValor<decimal>(reader, "Monto"),
                                    SaldoAnterior = ConexionHelper.LeerValor<decimal>(reader, "SaldoAnterior"),
                                    SaldoPosterior = ConexionHelper.LeerValor<decimal>(reader, "SaldoPosterior"),
                                    Descripcion = ConexionHelper.LeerValor<string>(reader, "Descripcion"),
                                    Referencia = ConexionHelper.LeerValor<string>(reader, "Referencia"),
                                    CanalOrigen = ConexionHelper.LeerValor<string>(reader, "CanalOrigen"),
                                    FechaMovimiento = ConexionHelper.LeerValor<DateTime>(reader, "FechaMovimiento"),
                                    FechaValor = ConexionHelper.LeerValor<DateTime>(reader, "FechaValor")
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
                                CuentaId = ConexionHelper.LeerValor<int>(reader, "CuentaId"),
                                NumeroCuenta = ConexionHelper.LeerValor<string>(reader, "NumeroCuenta"),
                                Saldo = ConexionHelper.LeerValor<decimal>(reader, "Saldo"),
                                SaldoDisponible = ConexionHelper.LeerValor<decimal>(reader, "SaldoDisponible"),
                                Estado = ConexionHelper.LeerValor<string>(reader, "Estado"),
                                FechaUltimoMovimiento = ConexionHelper.LeerFechaNullable(reader, "FechaUltimoMovimiento")
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
