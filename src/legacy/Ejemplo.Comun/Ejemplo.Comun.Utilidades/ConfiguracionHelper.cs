using System.Configuration;

namespace Ejemplo.Comun.Utilidades
{
    public static class ConfiguracionHelper
    {
        public static string ObtenerCadenaConexion(string nombre)
        {
            var setting = ConfigurationManager.ConnectionStrings[nombre];
            return setting?.ConnectionString;
        }

        public static string ObtenerConfiguracion(string clave, string valorPredeterminado = null)
        {
            var valor = ConfigurationManager.AppSettings[clave];
            return !string.IsNullOrEmpty(valor) ? valor : valorPredeterminado;
        }
    }
}
