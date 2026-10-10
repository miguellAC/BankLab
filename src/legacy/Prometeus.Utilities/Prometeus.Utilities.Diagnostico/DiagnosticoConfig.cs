using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Sockets;

namespace Prometeus.Utilities.Diagnostico
{
    public class DiagnosticoConfig
    {
        public static bool ProbarConexionBD(out string mensaje)
        {
            string connStr = ConfigurationManager.ConnectionStrings["BankLabDB"]?.ConnectionString;
            if (string.IsNullOrEmpty(connStr))
            {
                mensaje = "Cadena de conexión 'BankLabDB' no encontrada en App.config";
                return false;
            }

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT @@VERSION;", conn))
                    {
                        var version = cmd.ExecuteScalar()?.ToString();
                        mensaje = $"Conexión exitosa a SQL Server. Versión: {version?.Split('\n')[0]}";
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Fallo al conectar con SQL Server: {ex.Message}";
                return false;
            }
        }

        public static bool ProbarPuertoWCF(string host, int puerto, out string mensaje)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    var result = client.BeginConnect(host, puerto, null, null);
                    bool exito = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(2));
                    if (!exito)
                    {
                        mensaje = $"No responde el puerto NetTcp {puerto} en {host}. ¿El servicio host WCF está activo?";
                        return false;
                    }
                    client.EndConnect(result);
                    mensaje = $"Conexión TCP al servicio WCF exitosa ({host}:{puerto}).";
                    return true;
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error comprobando endpoint NetTcp {host}:{puerto}: {ex.Message}";
                return false;
            }
        }
    }
}
