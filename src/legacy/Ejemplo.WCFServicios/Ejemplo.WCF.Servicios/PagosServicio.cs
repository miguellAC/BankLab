using System;
using System.Collections.Generic;
using System.ServiceModel;
using Ejemplo.WCF.Contratos;
using Ejemplo.Comun.DTOs.Pagos;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Componentes.BLL;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.WCF.Servicios
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class PagosServicio : IPagosServicio
    {
        public RespuestaOperacion<PagoRealizadoDTO> RegistrarPago(PagoDTO pago, string usuario)
        {
            try
            {
                LogUtil.Info($"RegistrarPago: operacion {pago?.OperacionId} por {usuario}");
                var bll = new PagosBLL();
                var result = bll.RegistrarPago(pago, usuario);
                return RespuestaOperacion<PagoRealizadoDTO>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<PagoRealizadoDTO>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<List<PagoRealizadoDTO>> ConsultarPagosOperacion(int operacionId)
        {
            try
            {
                LogUtil.Info($"ConsultarPagosOperacion: {operacionId}");
                var bll = new PagosBLL();
                var result = bll.ConsultarPagosOperacion(operacionId);
                return RespuestaOperacion<List<PagoRealizadoDTO>>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<List<PagoRealizadoDTO>>.CrearError(ex.Message);
            }
        }
    }
}
