using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Ejemplo.Componentes.DAT
{
    public static class ConexionHelper
    {
        public static SqlConnection ObtenerConexion()
        {
            // Placeholder: ConfigurationManager normally used, matching prompt requirement
            string connString = "Server=localhost;Database=BankLabDB;User Id=sa;Password=BankLab2024!;TrustServerCertificate=True;";
            return new SqlConnection(connString);
        }

        public static SqlCommand CrearComandoSP(SqlConnection conexion, string spName)
        {
            SqlCommand cmd = new SqlCommand(spName, conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        public static SqlCommand CrearComandoSP(SqlConnection conexion, SqlTransaction transaccion, string spName)
        {
            SqlCommand cmd = new SqlCommand(spName, conexion, transaccion);
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        public static void AgregarParametro(SqlCommand cmd, string nombre, object valor)
        {
            cmd.Parameters.AddWithValue(nombre, valor ?? DBNull.Value);
        }

        public static T LeerValor<T>(IDataReader reader, string columna)
        {
            int index = reader.GetOrdinal(columna);
            if (reader.IsDBNull(index))
            {
                return default(T);
            }
            return (T)Convert.ChangeType(reader.GetValue(index), typeof(T));
        }

        public static DateTime? LeerFechaNullable(IDataReader reader, string columna)
        {
            int index = reader.GetOrdinal(columna);
            if (reader.IsDBNull(index))
            {
                return null;
            }
            return reader.GetDateTime(index);
        }
    }
}
