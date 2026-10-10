using System;
using System.Data.SqlClient;
using Ejemplo.Componentes.DAT;
using Ejemplo.Comun.DTOs.Integracion;

namespace Ejemplo.Componentes.DAL
{
    public class ImportacionDAL
    {
        private readonly ImportacionDAT _importacionDAT = new ImportacionDAT();

        public int CrearLoteImportacion(SqlConnection conn, SqlTransaction tx, string archivo, string tipo, int total, string usuario)
        {
            return _importacionDAT.CrearLoteImportacion(conn, tx, archivo, tipo, total, usuario);
        }

        public void InsertarDetalleImportacion(SqlConnection conn, SqlTransaction tx, int loteId, int numLinea, string contenido)
        {
            _importacionDAT.InsertarDetalleImportacion(conn, tx, loteId, numLinea, contenido);
        }

        public void ProcesarLoteImportacion(SqlConnection conn, SqlTransaction tx, int loteId, string usuario)
        {
            // Business rule: Validates batch state before processing
            var estado = ConsultarEstadoLote(loteId);
            if (estado == null)
            {
                throw new Exception("Lote no encontrado.");
            }
            if (estado.Estado != "PEN")
            {
                throw new Exception($"El lote ya ha sido procesado o se encuentra en un estado inválido ({estado.Estado}).");
            }

            _importacionDAT.ProcesarLoteImportacion(conn, tx, loteId, usuario);
        }

        public LoteImportacionDTO ConsultarEstadoLote(int loteId)
        {
            return _importacionDAT.ConsultarEstadoLote(loteId);
        }
    }
}
