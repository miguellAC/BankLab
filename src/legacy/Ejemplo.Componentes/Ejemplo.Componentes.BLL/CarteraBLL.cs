using System;
using System.Collections.Generic;
using Ejemplo.Componentes.DAS;
using Ejemplo.Comun.DTOs.Cartera;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.Componentes.BLL
{
    public class CarteraBLL
    {
        private readonly CarteraDAS _carteraDAS = new CarteraDAS();

        public RespuestaOperacion<List<ReporteCarteraDTO>> ObtenerReporteCartera(DateTime? fechaCorte, int? sucursalId, string estado, string catRiesgo)
        {
            try
            {
                var reporte = _carteraDAS.ObtenerReporteCartera(fechaCorte, sucursalId, estado, catRiesgo);
                return RespuestaOperacion<List<ReporteCarteraDTO>>.Exito(reporte);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ObtenerReporteCartera BLL", ex);
                return RespuestaOperacion<List<ReporteCarteraDTO>>.Error("Error al obtener reporte de cartera.");
            }
        }

        public RespuestaOperacion<List<ReporteCarteraDTO>> ObtenerReporteCarteraVencida(int diasMinimo)
        {
            try
            {
                if (diasMinimo < 0) diasMinimo = 0;
                
                var reporte = _carteraDAS.ObtenerReporteCarteraVencida(diasMinimo);
                return RespuestaOperacion<List<ReporteCarteraDTO>>.Exito(reporte);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ObtenerReporteCarteraVencida BLL", ex);
                return RespuestaOperacion<List<ReporteCarteraDTO>>.Error("Error al obtener reporte de cartera vencida.");
            }
        }
    }
}
