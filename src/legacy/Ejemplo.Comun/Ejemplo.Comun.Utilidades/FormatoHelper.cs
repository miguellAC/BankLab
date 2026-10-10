using System;
using Ejemplo.Comun.Enumeraciones;

namespace Ejemplo.Comun.Utilidades
{
    public static class FormatoHelper
    {
        public static string FormatearMonto(decimal monto)
        {
            return monto.ToString(Constantes.FormatoMonto);
        }

        public static string FormatearFecha(DateTime fecha)
        {
            return fecha.ToString(Constantes.FormatoFecha);
        }

        public static string FormatearIdentificacion(string tipo, string numero)
        {
            return $"{tipo}-{numero}";
        }

        public static string TruncarTexto(string texto, int maxLength)
        {
            if (string.IsNullOrEmpty(texto)) return texto;
            return texto.Length <= maxLength ? texto : texto.Substring(0, maxLength);
        }
    }
}
