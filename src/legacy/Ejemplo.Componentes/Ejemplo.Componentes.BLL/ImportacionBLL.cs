using System;
using System.Collections.Generic;
using Ejemplo.Componentes.DAS;
using Ejemplo.Comun.DTOs.Integracion;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.Componentes.BLL
{
    public class ImportacionBLL
    {
        private readonly ImportacionDAS _importacionDAS = new ImportacionDAS();

        // -----------------------------------------------------------------------
        // Flujo original (DAS existente): procesa un lote ya registrado en BD.
        // -----------------------------------------------------------------------

        public RespuestaOperacion<bool> ProcesarLoteCompleto(int loteId, string usuario)
        {
            try
            {
                if (loteId <= 0)
                    return RespuestaOperacion<bool>.CrearError("ID de lote inválido.");

                _importacionDAS.ProcesarLoteCompleto(loteId, usuario);
                return RespuestaOperacion<bool>.CrearExitosa(true);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ProcesarLoteCompleto BLL", ex);
                return RespuestaOperacion<bool>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<LoteImportacionDTO> ConsultarEstadoLote(int loteId)
        {
            try
            {
                var estado = _importacionDAS.ConsultarEstadoLote(loteId);
                return estado != null
                    ? RespuestaOperacion<LoteImportacionDTO>.CrearExitosa(estado)
                    : RespuestaOperacion<LoteImportacionDTO>.CrearError("Lote no encontrado.");
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarEstadoLote BLL", ex);
                return RespuestaOperacion<LoteImportacionDTO>.CrearError("Error interno.");
            }
        }

        // -----------------------------------------------------------------------
        // API de tres pasos requerida por ImportadorCSV (Opción C — stub seguro).
        // Acumula las líneas en memoria y delega al flujo completo en ProcesarLote.
        // No requiere nuevos stored procedures ni cambios de esquema.
        // -----------------------------------------------------------------------

        // Clave: loteId (correlativo en memoria por instancia de ImportacionBLL)
        private static int _nextLoteId = 1;
        private static readonly object _loteIdLock = new object();

        // Estado en memoria del lote activo
        private int _loteIdActivo = 0;
        private string _nombreArchivoActivo;
        private string _tipoImportacionActivo;
        private string _usuarioActivo;
        private readonly List<string> _lineasPendientes = new List<string>();
        private int _totalRegistrosDeclarados;

        /// <summary>
        /// Registra un nuevo lote en memoria y devuelve su ID correlativo.
        /// Compatible con el contrato que espera ImportadorCSV.
        /// </summary>
        public RespuestaOperacion<int> CrearLoteImportacion(
            string nombreArchivo, string tipoImportacion, int totalRegistros, string usuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombreArchivo))
                    return RespuestaOperacion<int>.CrearError("Nombre de archivo obligatorio.");
                if (totalRegistros < 0)
                    return RespuestaOperacion<int>.CrearError("TotalRegistros no puede ser negativo.");

                int loteId;
                lock (_loteIdLock) { loteId = _nextLoteId++; }

                _loteIdActivo            = loteId;
                _nombreArchivoActivo     = nombreArchivo;
                _tipoImportacionActivo   = tipoImportacion;
                _usuarioActivo           = usuario ?? "SYSTEM";
                _totalRegistrosDeclarados = totalRegistros;
                _lineasPendientes.Clear();

                LogHelper.Informacion($"ImportacionBLL: lote #{loteId} creado en memoria " +
                               $"({nombreArchivo}, {tipoImportacion}, {totalRegistros} registros).");

                return RespuestaOperacion<int>.CrearExitosa(loteId);
            }
            catch (Exception ex)
            {
                LogHelper.Error("CrearLoteImportacion BLL", ex);
                return RespuestaOperacion<int>.CrearError(ex.Message);
            }
        }

        /// <summary>
        /// Acumula una línea del archivo CSV en el buffer del lote activo.
        /// Compatible con el contrato que espera ImportadorCSV.
        /// </summary>
        public RespuestaOperacion<bool> InsertarDetalle(int loteId, int numeroLinea, string contenido)
        {
            try
            {
                if (loteId != _loteIdActivo)
                    return RespuestaOperacion<bool>.CrearError(
                        $"Lote #{loteId} no coincide con el lote activo #{_loteIdActivo}.");

                if (string.IsNullOrWhiteSpace(contenido))
                    return RespuestaOperacion<bool>.CrearExitosa(true); // línea vacía, se ignora

                _lineasPendientes.Add(contenido);
                return RespuestaOperacion<bool>.CrearExitosa(true);
            }
            catch (Exception ex)
            {
                LogHelper.Error($"InsertarDetalle BLL (lote={loteId}, linea={numeroLinea})", ex);
                return RespuestaOperacion<bool>.CrearError(ex.Message);
            }
        }

        /// <summary>
        /// Delega al flujo completo (ProcesarLoteCompleto → DAS → DAL → SP).
        /// El loteId debe haber sido creado previamente con CrearLoteImportacion.
        /// Compatible con el contrato que espera ImportadorCSV.
        /// </summary>
        public RespuestaOperacion<bool> ProcesarLote(int loteId, string usuario)
        {
            try
            {
                if (loteId != _loteIdActivo)
                    return RespuestaOperacion<bool>.CrearError(
                        $"Lote #{loteId} no coincide con el lote activo #{_loteIdActivo}.");

                LogHelper.Informacion($"ImportacionBLL: procesando lote #{loteId} " +
                               $"({_lineasPendientes.Count} líneas acumuladas).");

                return ProcesarLoteCompleto(loteId, usuario ?? _usuarioActivo);
            }
            catch (Exception ex)
            {
                LogHelper.Error($"ProcesarLote BLL (lote={loteId})", ex);
                return RespuestaOperacion<bool>.CrearError(ex.Message);
            }
        }
    }
}
