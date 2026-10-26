using System;
using System.Collections.Generic;
using System.ServiceModel;
using Ejemplo.WCF.Contratos;
using Ejemplo.Comun.DTOs.Creditos;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Componentes.BLL;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.WCF.Servicios
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CreditosServicio : ICreditosServicio
    {
        public RespuestaOperacion<OperacionCreditoDTO> ConsultarOperacionCredito(int? operacionId, string numeroOperacion)
        {
            try
            {
                LogHelper.Informacion($"ConsultarOperacionCredito: {operacionId} / {numeroOperacion}");
                var bll = new CreditosBLL();
                return bll.ConsultarOperacionCredito(operacionId, numeroOperacion);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarOperacionCredito WCF", ex);
                return RespuestaOperacion<OperacionCreditoDTO>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<SaldoOperacionDTO> ConsultarSaldosVigenteVencido(int operacionId)
        {
            try
            {
                LogHelper.Informacion($"ConsultarSaldosVigenteVencido: {operacionId}");
                var bll = new CreditosBLL();
                return bll.ConsultarSaldosVigenteVencido(operacionId);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarSaldosVigenteVencido WCF", ex);
                return RespuestaOperacion<SaldoOperacionDTO>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<List<CuotaDTO>> ConsultarCuotasOperacion(int operacionId, bool soloVencidas, bool soloPendientes)
        {
            try
            {
                LogHelper.Informacion($"ConsultarCuotasOperacion: {operacionId}");
                var bll = new CreditosBLL();
                return bll.ConsultarCuotasOperacion(operacionId, soloVencidas, soloPendientes);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarCuotasOperacion WCF", ex);
                return RespuestaOperacion<List<CuotaDTO>>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<List<CuotaDTO>> ConsultarCuotasPendientesVencidas(int? clienteId)
        {
            try
            {
                LogHelper.Informacion($"ConsultarCuotasPendientesVencidas: clienteId={clienteId}");
                var bll = new CreditosBLL();
                return bll.ConsultarCuotasPendientesVencidas(clienteId);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarCuotasPendientesVencidas WCF", ex);
                return RespuestaOperacion<List<CuotaDTO>>.CrearError(ex.Message);
            }
        }
    }
}
