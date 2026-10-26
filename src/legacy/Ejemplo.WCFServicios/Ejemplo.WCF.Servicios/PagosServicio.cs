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
    public class PagosServicio : IPagosServicio
    {
        public RespuestaOperacion<long> RegistrarPago(int operacionId, decimal montoPago, string tipoPago, string canalPago, int? cuentaOrigenId, string referencia, string usuario)
        {
            try
            {
                LogHelper.Informacion($"RegistrarPago: operacion {operacionId} por {usuario}");
                var bll = new PagosBLL();
                return bll.RegistrarPago(operacionId, montoPago, tipoPago, canalPago, cuentaOrigenId, referencia, usuario);
            }
            catch (Exception ex)
            {
                LogHelper.Error("RegistrarPago WCF", ex);
                return RespuestaOperacion<long>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<List<PagoDTO>> ConsultarPagosOperacion(int operacionId, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            try
            {
                LogHelper.Informacion($"ConsultarPagosOperacion: {operacionId}");
                var bll = new PagosBLL();
                return bll.ConsultarPagosOperacion(operacionId, fechaDesde, fechaHasta);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarPagosOperacion WCF", ex);
                return RespuestaOperacion<List<PagoDTO>>.CrearError(ex.Message);
            }
        }
    }
}
