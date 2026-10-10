using System;
using System.Data;
using System.Data.SqlClient;
using Ejemplo.Comun.DTOs.Integracion;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.Componentes.DAT
{
    public class ImportacionDAT
    {
        public int CrearLoteImportacion(SqlConnection conn, SqlTransaction tx, string archivo, string tipo, int total, string usuario)
        {
            try
            {
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, tx, "sp_CrearLoteImportacion"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@NombreArchivo", archivo);
                    ConexionHelper.AgregarParametro(cmd, "@TipoImportacion", tipo);
                    ConexionHelper.AgregarParametro(cmd, "@TotalRegistros", total);
                    ConexionHelper.AgregarParametro(cmd, "@UsuarioRegistro", usuario);

                    SqlParameter outputId = new SqlParameter("@LoteId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(outputId);

                    cmd.ExecuteNonQuery();

                    return (int)outputId.Value;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en CrearLoteImportacion", ex);
                throw;
            }
        }

        public void InsertarDetalleImportacion(SqlConnection conn, SqlTransaction tx, int loteId, int numLinea, string contenido)
        {
            try
            {
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, tx, "sp_InsertarDetalleImportacion"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@LoteId", loteId);
                    ConexionHelper.AgregarParametro(cmd, "@NumeroLinea", numLinea);
                    ConexionHelper.AgregarParametro(cmd, "@Contenido", contenido);
                    
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en InsertarDetalleImportacion", ex);
                throw;
            }
        }

        public void ProcesarLoteImportacion(SqlConnection conn, SqlTransaction tx, int loteId, string usuario)
        {
            try
            {
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, tx, "sp_ProcesarLoteImportacion"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@LoteId", loteId);
                    ConexionHelper.AgregarParametro(cmd, "@UsuarioProceso", usuario);
                    
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en ProcesarLoteImportacion", ex);
                throw;
            }
        }

        public LoteImportacionDTO ConsultarEstadoLote(int loteId)
        {
            SqlConnection conn = null;
            try
            {
                conn = ConexionHelper.ObtenerConexion();
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, "sp_ConsultarEstadoLote"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@LoteId", loteId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new LoteImportacionDTO
                            {
                                LoteId = ConexionHelper.LeerValor<int>(reader, "LoteId"),
                                NombreArchivo = ConexionHelper.LeerValor<string>(reader, "NombreArchivo"),
                                Estado = ConexionHelper.LeerValor<string>(reader, "Estado"),
                                TotalRegistros = ConexionHelper.LeerValor<int>(reader, "TotalRegistros"),
                                RegistrosProcesados = ConexionHelper.LeerValor<int>(reader, "RegistrosProcesados"),
                                FechaRegistro = ConexionHelper.LeerValor<DateTime>(reader, "FechaRegistro")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en ConsultarEstadoLote", ex);
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
