using System;
using System.Collections.Generic;
using Ejemplo.Componentes.DAS;
using Ejemplo.Comun.DTOs.Creditos;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Comun.Validaciones;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.Componentes.BLL
{
    public class PagosBLL
    {
        private readonly PagosDAS _pagosDAS = new PagosDAS();

        public RespuestaOperacion<long> RegistrarPago(int opId, decimal monto, string tipoPago, string canalPago, int? cuentaOrigenId, string referencia, string usuario)
        {
            try
            {
                if (opId <= 0)
                    return RespuestaOperacion<long>.CrearError("ID de operación inválido.");

                var validacion = ValidadorMonto.ValidarMontoPositivo(monto);
                if (!validacion.EsValido)
                    return RespuestaOperacion<long>.CrearError(string.Join("; ", validacion.Errores));

                long pagoId = _pagosDAS.RegistrarPago(opId, monto, tipoPago, canalPago, cuentaOrigenId, referencia, usuario);
                return RespuestaOperacion<long>.CrearExitosa(pagoId);
            }
            catch (Exception ex)
            {
                LogHelper.Error("RegistrarPago BLL", ex);
                return RespuestaOperacion<long>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<List<PagoDTO>> ConsultarPagosOperacion(int opId, DateTime? desde, DateTime? hasta)
        {
            try
            {
                if (opId <= 0)
                    return RespuestaOperacion<List<PagoDTO>>.CrearError("ID de operación inválido.");

                var pagos = _pagosDAS.ConsultarPagosOperacion(opId, desde, hasta);
                return RespuestaOperacion<List<PagoDTO>>.CrearExitosa(pagos);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarPagosOperacion BLL", ex);
                return RespuestaOperacion<List<PagoDTO>>.CrearError("Error al consultar pagos.");
            }
        }
    }
}
