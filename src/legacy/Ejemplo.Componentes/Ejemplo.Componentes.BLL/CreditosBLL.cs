using System;
using System.Collections.Generic;
using Ejemplo.Componentes.DAS;
using Ejemplo.Comun.DTOs.Creditos;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.Componentes.BLL
{
    public class CreditosBLL
    {
        private readonly CreditosDAS _creditosDAS = new CreditosDAS();

        public RespuestaOperacion<OperacionCreditoDTO> ConsultarOperacionCredito(int? opId, string numOp)
        {
            try
            {
                if (!opId.HasValue && string.IsNullOrWhiteSpace(numOp))
                    return RespuestaOperacion<OperacionCreditoDTO>.CrearError("Debe especificar ID u número de operación.");

                var operacion = _creditosDAS.ConsultarOperacionCredito(opId, numOp);
                return RespuestaOperacion<OperacionCreditoDTO>.CrearExitosa(operacion);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarOperacionCredito BLL", ex);
                return RespuestaOperacion<OperacionCreditoDTO>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<SaldoOperacionDTO> ConsultarSaldosVigenteVencido(int opId)
        {
            try
            {
                if (opId <= 0) return RespuestaOperacion<SaldoOperacionDTO>.CrearError("ID inválido.");
                
                var saldo = _creditosDAS.ConsultarSaldosVigenteVencido(opId);
                return saldo != null 
                    ? RespuestaOperacion<SaldoOperacionDTO>.CrearExitosa(saldo)
                    : RespuestaOperacion<SaldoOperacionDTO>.CrearError("No encontrado.");
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarSaldosVigenteVencido BLL", ex);
                return RespuestaOperacion<SaldoOperacionDTO>.CrearError("Error interno.");
            }
        }

        public RespuestaOperacion<List<CuotaDTO>> ConsultarCuotasOperacion(int opId, bool soloVencidas, bool soloPendientes)
        {
            try
            {
                if (opId <= 0) return RespuestaOperacion<List<CuotaDTO>>.CrearError("ID de operación inválido.");

                var cuotas = _creditosDAS.ConsultarCuotasOperacion(opId, soloVencidas, soloPendientes);
                return RespuestaOperacion<List<CuotaDTO>>.CrearExitosa(cuotas);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarCuotasOperacion BLL", ex);
                return RespuestaOperacion<List<CuotaDTO>>.CrearError("Error interno.");
            }
        }

        public RespuestaOperacion<List<CuotaDTO>> ConsultarCuotasPendientesVencidas(int? clienteId)
        {
            try
            {
                var cuotas = _creditosDAS.ConsultarCuotasPendientesVencidas(clienteId);
                return RespuestaOperacion<List<CuotaDTO>>.CrearExitosa(cuotas);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarCuotasPendientesVencidas BLL", ex);
                return RespuestaOperacion<List<CuotaDTO>>.CrearError("Error interno.");
            }
        }
    }
}
