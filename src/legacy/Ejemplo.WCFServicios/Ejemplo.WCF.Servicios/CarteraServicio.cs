using System;
using System.Collections.Generic;
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
        public RespuestaOperacion<List<ReporteCarteraDTO>> ObtenerReporteCartera(
            DateTime? fechaCorte, int? sucursalId, string estado, string categoriaRiesgo)
        {
            try
            {
                LogHelper.Informacion($"ObtenerReporteCartera: sucursal {sucursalId}, estado {estado}");
                var bll = new CarteraBLL();
                return bll.ObtenerReporteCartera(fechaCorte, sucursalId, estado, categoriaRiesgo);
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en CarteraServicio.ObtenerReporteCartera", ex);
                return RespuestaOperacion<List<ReporteCarteraDTO>>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<List<ReporteCarteraDTO>> ObtenerReporteCarteraVencida(int diasMinimo)
        {
            try
            {
                LogHelper.Informacion($"ObtenerReporteCarteraVencida: diasMinimo {diasMinimo}");
                var bll = new CarteraBLL();
                return bll.ObtenerReporteCarteraVencida(diasMinimo);
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en CarteraServicio.ObtenerReporteCarteraVencida", ex);
                return RespuestaOperacion<List<ReporteCarteraDTO>>.CrearError(ex.Message);
            }
        }
    }
}
