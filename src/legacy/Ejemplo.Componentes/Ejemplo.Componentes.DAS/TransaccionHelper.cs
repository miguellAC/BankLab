using System;
using System.Data.SqlClient;
using Ejemplo.Componentes.DAT;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.Componentes.DAS
{
    public static class TransaccionHelper
    {
        public static T EjecutarEnTransaccion<T>(Func<SqlConnection, SqlTransaction, T> accion)
        {
            SqlConnection conn = null;
            SqlTransaction tx = null;
            try
            {
                conn = ConexionHelper.ObtenerConexion();
                conn.Open();
                tx = conn.BeginTransaction();

                T resultado = accion(conn, tx);

                tx.Commit();
                return resultado;
            }
            catch (Exception ex)
            {
                if (tx != null)
                {
                    try
                    {
                        tx.Rollback();
                    }
                    catch (Exception rbEx)
                    {
                        LogHelper.Error("Error al hacer rollback de la transacción", rbEx);
                    }
                }
                LogHelper.Error("Error en EjecutarEnTransaccion<T>", ex);
                throw;
            }
            finally
            {
                if (conn != null && conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        public static void EjecutarEnTransaccion(Action<SqlConnection, SqlTransaction> accion)
        {
            EjecutarEnTransaccion<object>((conn, tx) => 
            {
                accion(conn, tx);
                return null;
            });
        }
    }
}
