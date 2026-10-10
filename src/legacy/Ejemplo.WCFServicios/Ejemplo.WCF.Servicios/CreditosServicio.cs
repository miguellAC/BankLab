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
        public RespuestaOperacion<OperacionCreditoDTO> ConsultarOperacionCredito(int operacionId)
        {
            try
            {
                LogUtil.Info($"ConsultarOperacionCredito: {operacionId}");
                var bll = new CreditosBLL();
                var result = bll.ConsultarOperacionCredito(operacionId);
                return RespuestaOperacion<OperacionCreditoDTO>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<OperacionCreditoDTO>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<SaldoOperacionDTO> ConsultarSaldosVigenteVencido(int operacionId)
        {
            try
            {
                LogUtil.Info($"ConsultarSaldosVigenteVencido: {operacionId}");
                var bll = new CreditosBLL();
                var result = bll.ConsultarSaldosVigenteVencido(operacionId);
                return RespuestaOperacion<SaldoOperacionDTO>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<SaldoOperacionDTO>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<List<CuotaCreditoDTO>> ConsultarCuotasOperacion(int operacionId)
        {
            try
            {
                LogUtil.Info($"ConsultarCuotasOperacion: {operacionId}");
                var bll = new CreditosBLL();
                var result = bll.ConsultarCuotasOperacion(operacionId);
                return RespuestaOperacion<List<CuotaCreditoDTO>>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<List<CuotaCreditoDTO>>.CrearError(ex.Message);
            }
        }
    }
}
