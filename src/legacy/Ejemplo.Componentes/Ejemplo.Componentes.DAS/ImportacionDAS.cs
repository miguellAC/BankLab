using System;
using Ejemplo.Componentes.DAL;
using Ejemplo.Comun.DTOs.Integracion;

namespace Ejemplo.Componentes.DAS
{
    public class ImportacionDAS
    {
        private readonly ImportacionDAL _importacionDAL = new ImportacionDAL();

        public void ProcesarLoteCompleto(int loteId, string usuario)
        {
            TransaccionHelper.EjecutarEnTransaccion((conn, tx) =>
            {
                _importacionDAL.ProcesarLoteImportacion(conn, tx, loteId, usuario);
            });
        }

        public LoteImportacionDTO ConsultarEstadoLote(int loteId)
        {
            return _importacionDAL.ConsultarEstadoLote(loteId);
        }
    }
}
