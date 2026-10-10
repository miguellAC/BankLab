using System;
using System.Text.RegularExpressions;

namespace Ejemplo.Comun.Utilidades
{
    public static class ValidacionHelper
    {
        public static bool EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool EsTelefonoValido(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono)) return false;
            return Regex.IsMatch(telefono, @"^\+?[0-9\-\s]{7,15}$");
        }

        public static bool EsIdentificacionValida(string tipo, string numero)
        {
            if (string.IsNullOrWhiteSpace(numero)) return false;
            // Basic validation
            return numero.Length >= 5 && numero.Length <= 20;
        }

        public static bool EsFechaValida(string fecha, out DateTime resultado)
        {
            return DateTime.TryParse(fecha, out resultado);
        }
    }
}
