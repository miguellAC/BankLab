using System;
using System.Security.Cryptography;
using System.Text;

namespace Ejemplo.Comun.Utilidades
{
    public static class CifradoHelper
    {
        public static string CalcularHash(string input)
        {
            if (input == null) return null;
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder();
                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static string EnmascararNumero(string numero)
        {
            if (string.IsNullOrEmpty(numero) || numero.Length <= 4)
                return numero;
            return new string('*', numero.Length - 4) + numero.Substring(numero.Length - 4);
        }
    }
}
