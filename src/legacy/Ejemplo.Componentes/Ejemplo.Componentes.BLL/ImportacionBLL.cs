using System;
using Ejemplo.Componentes.DAS;
using Ejemplo.Comun.DTOs.Integracion;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.Componentes.BLL
{
    public class ImportacionBLL
    {
        private readonly ImportacionDAS _importacionDAS = new ImportacionDAS();

        public RespuestaOperacion<bool> ProcesarLoteCompleto(int loteId, string usuario)
        {
            try
            {
                if (loteId <= 0)
                    return RespuestaOperacion<bool>.Error("ID de lote inválido.");

                _importacionDAS.ProcesarLoteCompleto(loteId, usuario);
                return RespuestaOperacion<bool>.Exito(true);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ProcesarLoteCompleto BLL", ex);
                return RespuestaOperacion<bool>.Error(ex.Message);
            }
        }

        public RespuestaOperacion<LoteImportacionDTO> ConsultarEstadoLote(int loteId)
        {
            try
            {
                var estado = _importacionDAS.ConsultarEstadoLote(loteId);
                return estado != null 
                    ? RespuestaOperacion<LoteImportacionDTO>.Exito(estado)
                    : RespuestaOperacion<LoteImportacionDTO>.Error("Lote no encontrado.");
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarEstadoLote BLL", ex);
                return RespuestaOperacion<LoteImportacionDTO>.Error("Error interno.");
            }
        }
    }
}
