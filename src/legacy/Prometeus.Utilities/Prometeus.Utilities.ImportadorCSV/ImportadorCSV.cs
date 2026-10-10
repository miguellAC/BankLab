using System;
using System.IO;
using System.Text;
using Ejemplo.Componentes.BLL;
using Ejemplo.Comun.DTOs.Integracion;

namespace Prometeus.Utilities.ImportadorCSV
{
    public class ImportadorCSV
    {
        private readonly ImportacionBLL _importacionBLL = new ImportacionBLL();

        public void ProcesarArchivoPagos(string rutaArchivo, string usuario = "SYSTEM_BATCH")
        {
            if (!File.Exists(rutaArchivo))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"El archivo {rutaArchivo} no fue encontrado.");
                Console.ResetColor();
                return;
            }

            string nombreArchivo = Path.GetFileName(rutaArchivo);
            var lineas = File.ReadAllLines(rutaArchivo, Encoding.UTF8);

            Console.WriteLine($"Iniciando importación del archivo {nombreArchivo} con {lineas.Length} líneas...");

            // 1. Crear lote
            var resLote = _importacionBLL.CrearLoteImportacion(nombreArchivo, "PAGOS", lineas.Length, usuario);
            if (!resLote.Exitosa)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error al crear lote: {resLote.Mensaje}");
                Console.ResetColor();
                return;
            }

            int loteId = resLote.Datos;
            Console.WriteLine($"Lote #{loteId} creado exitosamente.");

            // 2. Insertar detalles
            for (int i = 0; i < lineas.Length; i++)
            {
                string contenido = lineas[i];
                if (string.IsNullOrWhiteSpace(contenido)) continue;

                var resDetalle = _importacionBLL.InsertarDetalle(loteId, i + 1, contenido);
                if (!resDetalle.Exitosa)
                {
                    Console.WriteLine($"Aviso en línea {i + 1}: {resDetalle.Mensaje}");
                }
            }

            // 3. Procesar lote
            Console.WriteLine($"Procesando transacciones del lote #{loteId}...");
            var resProc = _importacionBLL.ProcesarLote(loteId, usuario);
            if (resProc.Exitosa)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Lote #{loteId} procesado satisfactoriamente.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Lote procesado con observaciones: {resProc.Mensaje}");
                Console.ResetColor();
            }

            // 4. Resumen
            var resEstado = _importacionBLL.ConsultarEstadoLote(loteId);
            if (resEstado.Exitosa && resEstado.Datos != null)
            {
                var l = resEstado.Datos;
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"Total Registros:     {l.TotalRegistros}");
                Console.WriteLine($"Exitosos:            {l.RegistrosExitosos}");
                Console.WriteLine($"Con Error:           {l.RegistrosConError}");
                Console.WriteLine($"Estado Final:        {l.Estado}");
                Console.WriteLine("----------------------------------------");
            }
        }
    }
}
