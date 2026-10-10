using System;
using System.IO;

namespace Ejemplo.Servicios.ProcesadorPagos
{
    public class ProcesadorEngine
    {
        public void ScanDirectory()
        {
            string dirEntrada = System.Configuration.ConfigurationManager.AppSettings["DirectorioEntrada"];
            string dirProcesados = System.Configuration.ConfigurationManager.AppSettings["DirectorioProcesados"];

            if (!Directory.Exists(dirEntrada)) return;
            if (!Directory.Exists(dirProcesados)) Directory.CreateDirectory(dirProcesados);

            string[] files = Directory.GetFiles(dirEntrada, "*.csv");
            foreach (var file in files)
            {
                ProcessFile(file, dirProcesados);
            }
        }

        public void ProcessFile(string filePath, string dirProcesados)
        {
            Console.WriteLine($"Procesando archivo: {filePath}");
            // Lógica de lectura de CSV, validación y batch
            // Mover a procesados
            string fileName = Path.GetFileName(filePath);
            string dest = Path.Combine(dirProcesados, fileName);
            if (!File.Exists(dest))
            {
                File.Move(filePath, dest);
                Console.WriteLine($"Archivo procesado y movido a: {dest}");
            }
        }
    }
}
