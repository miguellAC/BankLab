using System;

namespace Ejemplo.Comun.Validaciones
{
    public static class ValidadorFecha
    {
        public static ResultadoValidacion ValidarFechaNoFutura(DateTime fecha)
        {
            if (fecha > DateTime.Now)
                return ResultadoValidacion.Invalido("La fecha no puede ser futura.");
            return ResultadoValidacion.Valido();
        }

        public static ResultadoValidacion ValidarRangoFechas(DateTime inicio, DateTime fin)
        {
            if (inicio > fin)
                return ResultadoValidacion.Invalido("La fecha de inicio no puede ser posterior a la fecha de fin.");
            return ResultadoValidacion.Valido();
        }

        public static ResultadoValidacion ValidarEdadMinima(DateTime fechaNacimiento, int edadMinima)
        {
            var hoy = DateTime.Today;
            var edad = hoy.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > hoy.AddYears(-edad)) edad--;

            if (edad < edadMinima)
                return ResultadoValidacion.Invalido($"La edad debe ser mayor o igual a {edadMinima} años.");
            return ResultadoValidacion.Valido();
        }
    }
}
