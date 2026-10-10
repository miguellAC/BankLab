using System;
using System.IO;
using System.Text;
using Ejemplo.Comun.Validaciones;

namespace Prometeus.Utilities.ValidadorDatos
{
    public class ValidadorEntrada
    {
        public static ResultadoValidacion ValidarArchivoCSV(string rutaArchivo, string[] columnasEsperadas)
        {
            var resultado = new ResultadoValidacion();

            if (string.IsNullOrWhiteSpace(rutaArchivo) || !File.Exists(rutaArchivo))
            {
                resultado.AgregarError($"El archivo no existe o la ruta es inválida: '{rutaArchivo}'");
                return resultado;
            }

            var extension = Path.GetExtension(rutaArchivo);
            if (!string.Equals(extension, ".csv", StringComparison.OrdinalIgnoreCase))
            {
                resultado.AgregarError("El archivo no posee extensión .csv válida.");
                return resultado;
            }

            try
            {
                using (var reader = new StreamReader(rutaArchivo, Encoding.UTF8))
                {
                    string encabezado = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(encabezado))
                    {
                        resultado.AgregarError("El archivo CSV está vacío o sin encabezados.");
                        return resultado;
                    }

                    if (columnasEsperadas != null && columnasEsperadas.Length > 0)
                    {
                        var colsArchivo = encabezado.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                        if (colsArchivo.Length < columnasEsperadas.Length)
                        {
                            resultado.AgregarError($"Número de columnas insuficiente. Esperadas: {columnasEsperadas.Length}, Encontradas: {colsArchivo.Length}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.AgregarError($"Error de lectura en archivo CSV: {ex.Message}");
            }

            return resultado;
        }

        public static bool ValidarFormatoLinea(string linea, int cantidadColumnasMinima, char delimitador = ',')
        {
            if (string.IsNullOrWhiteSpace(linea)) return false;
            var partes = linea.Split(delimitador);
            return partes.Length >= cantidadColumnasMinima;
        }

        public static bool ValidarEncodingUTF8(string rutaArchivo)
        {
            try
            {
                using (var fs = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read))
                {
                    byte[] bom = new byte[3];
                    fs.Read(bom, 0, 3);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
