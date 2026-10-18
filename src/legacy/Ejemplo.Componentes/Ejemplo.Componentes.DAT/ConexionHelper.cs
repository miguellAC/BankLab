using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Ejemplo.Componentes.DAT
{
    public static class ConexionHelper
    {
        /// <summary>Nombre de la cadena en connectionStrings (App.config del proceso que aloja el DAT).</summary>
        public const string NombreCadenaConexion = "BankLabDB";

        /// <summary>Variable de entorno opcional que tiene prioridad sobre el archivo de configuracion.</summary>
        public const string VariableEntornoCadena = "BANKLAB_CONNECTION_STRING";

        private const string MarcadorPlantilla = "COMPLETAR";

        /// <summary>
        /// Crea la conexion usando la cadena de configuracion (sin credenciales en el codigo).
        /// Orden: variable de entorno BANKLAB_CONNECTION_STRING, luego connectionStrings["BankLabDB"].
        /// </summary>
        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(ObtenerCadenaConexion());
        }

        public static string ObtenerCadenaConexion()
        {
            string cadena = Environment.GetEnvironmentVariable(VariableEntornoCadena);

            if (string.IsNullOrWhiteSpace(cadena))
            {
                ConnectionStringSettings cfg = ConfigurationManager.ConnectionStrings[NombreCadenaConexion];
                cadena = cfg != null ? cfg.ConnectionString : null;
            }

            if (string.IsNullOrWhiteSpace(cadena))
                throw new InvalidOperationException(
                    "No se encontro la cadena de conexion '" + NombreCadenaConexion + "'. Defina connectionStrings en el " +
                    "App.config del proceso o la variable de entorno " + VariableEntornoCadena + ".");

            if (cadena.IndexOf(MarcadorPlantilla, StringComparison.OrdinalIgnoreCase) >= 0)
                throw new InvalidOperationException(
                    "La cadena de conexion '" + NombreCadenaConexion + "' aun es la plantilla. Reemplace el marcador " +
                    MarcadorPlantilla + " por la contrasena local o use la variable de entorno " + VariableEntornoCadena + ".");

            return cadena;
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
