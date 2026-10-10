using System;
using System.Linq;

namespace Ejemplo.Comun.Validaciones
{
    public static class ValidadorIdentificacion
    {
        public static ResultadoValidacion Validar(string tipo, string numero)
        {
            var resultado = new ResultadoValidacion();
            if (string.IsNullOrWhiteSpace(numero))
            {
                resultado.AgregarError("El número de identificación es requerido.");
                return resultado;
            }

            switch (tipo.ToUpperInvariant())
            {
                case "CC":
                    if (numero.Length < 6 || numero.Length > 10 || !numero.All(char.IsDigit))
                        resultado.AgregarError("La CC debe tener entre 6 y 10 dígitos numéricos.");
                    break;
                case "NIT":
                    if (!numero.Contains("-") || numero.Length < 8)
                        resultado.AgregarError("El NIT debe incluir el dígito de verificación.");
                    break;
                case "CE":
                    if (!numero.All(char.IsLetterOrDigit))
                        resultado.AgregarError("La CE debe ser alfanumérica.");
                    break;
                default:
                    resultado.AgregarError("Tipo de identificación no soportado.");
                    break;
            }
            return resultado;
        }
    }
}
