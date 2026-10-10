using System;

namespace Ejemplo.Comun.Validaciones
{
    public static class ValidadorMonto
    {
        public static ResultadoValidacion ValidarMontoPositivo(decimal monto)
        {
            if (monto <= 0)
                return ResultadoValidacion.Invalido("El monto debe ser mayor a cero.");
            return ResultadoValidacion.Valido();
        }

        public static ResultadoValidacion ValidarMontoEnRango(decimal monto, decimal min, decimal max)
        {
            if (monto < min || monto > max)
                return ResultadoValidacion.Invalido($"El monto debe estar entre {min} y {max}.");
            return ResultadoValidacion.Valido();
        }

        public static ResultadoValidacion ValidarMontoPago(decimal monto, decimal saldoPendiente)
        {
            var resultado = ValidarMontoPositivo(monto);
            if (!resultado.EsValido) return resultado;

            if (monto > saldoPendiente)
                return ResultadoValidacion.Invalido("El monto de pago no puede ser mayor al saldo pendiente.");
            return ResultadoValidacion.Valido();
        }
    }
}
