using System;
using System.ServiceModel;
using Ejemplo.WCF.Contratos;
using Ejemplo.Comun.DTOs.Cartera;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Componentes.BLL;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.WCF.Servicios
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CarteraServicio : ICarteraServicio
    {
        public RespuestaOperacion<ReporteCarteraDTO> ObtenerReporteCartera(int sucursalId)
        {
            try
            {
                LogUtil.Info($"ObtenerReporteCartera: sucursal {sucursalId}");
                var bll = new CarteraBLL();
                var result = bll.ObtenerReporteCartera(sucursalId);
                return RespuestaOperacion<ReporteCarteraDTO>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<ReporteCarteraDTO>.CrearError(ex.Message);
            }
        }
    }
}
