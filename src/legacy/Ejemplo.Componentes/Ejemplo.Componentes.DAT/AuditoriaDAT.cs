using System;
using System.Data;
using System.Data.SqlClient;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.Componentes.DAT
{
    public class AuditoriaDAT
    {
        public void RegistrarAuditoria(SqlConnection conn, SqlTransaction tx, string tipoEntidad, int entidadId, string accion, string valorAnt, string valorNuevo, string ip, string equipo, string usuario)
        {
            try
            {
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, tx, "sp_RegistrarAuditoria"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@TipoEntidad", tipoEntidad);
                    ConexionHelper.AgregarParametro(cmd, "@EntidadId", entidadId);
                    ConexionHelper.AgregarParametro(cmd, "@Accion", accion);
                    ConexionHelper.AgregarParametro(cmd, "@ValorAnterior", valorAnt);
                    ConexionHelper.AgregarParametro(cmd, "@ValorNuevo", valorNuevo);
                    ConexionHelper.AgregarParametro(cmd, "@DireccionIP", ip);
                    ConexionHelper.AgregarParametro(cmd, "@Equipo", equipo);
                    ConexionHelper.AgregarParametro(cmd, "@Usuario", usuario);
                    
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en RegistrarAuditoria (Transaccional)", ex);
                throw;
            }
        }

        public void RegistrarAuditoria(string tipoEntidad, int entidadId, string accion, string valorAnt, string valorNuevo, string ip, string equipo, string usuario)
        {
            SqlConnection conn = null;
            try
            {
                conn = ConexionHelper.ObtenerConexion();
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, "sp_RegistrarAuditoria"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@TipoEntidad", tipoEntidad);
                    ConexionHelper.AgregarParametro(cmd, "@EntidadId", entidadId);
                    ConexionHelper.AgregarParametro(cmd, "@Accion", accion);
                    ConexionHelper.AgregarParametro(cmd, "@ValorAnterior", valorAnt);
                    ConexionHelper.AgregarParametro(cmd, "@ValorNuevo", valorNuevo);
                    ConexionHelper.AgregarParametro(cmd, "@DireccionIP", ip);
                    ConexionHelper.AgregarParametro(cmd, "@Equipo", equipo);
                    ConexionHelper.AgregarParametro(cmd, "@Usuario", usuario);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en RegistrarAuditoria", ex);
                throw;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        public DataTable ConsultarAuditoriaEntidad(string tipoEntidad, int entidadId, DateTime? desde, DateTime? hasta)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = null;
            try
            {
                conn = ConexionHelper.ObtenerConexion();
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, "sp_ConsultarAuditoriaEntidad"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@TipoEntidad", tipoEntidad);
                    ConexionHelper.AgregarParametro(cmd, "@EntidadId", entidadId);
                    ConexionHelper.AgregarParametro(cmd, "@Desde", desde);
                    ConexionHelper.AgregarParametro(cmd, "@Hasta", hasta);
                    
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en ConsultarAuditoriaEntidad", ex);
                throw;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            return dt;
        }
    }
}
