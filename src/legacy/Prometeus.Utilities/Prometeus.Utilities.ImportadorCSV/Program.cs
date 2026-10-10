using System;

namespace Prometeus.Utilities.ImportadorCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Prometeus - Importador CSV";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========================================");
            Console.WriteLine("  Prometeus Utilities - Importador CSV");
            Console.WriteLine("========================================");
            Console.ResetColor();

            if (args.Length == 0)
            {
                Console.WriteLine("Uso: ImportadorCSV.exe <ruta_archivo_csv>");
                Console.WriteLine("Ejemplo: ImportadorCSV.exe ..\\..\\..\\..\\samples\\importacion\\pagos_ejemplo.csv");
                return;
            }

            string ruta = args[0];
            var importador = new ImportadorCSV();
            importador.ProcesarArchivoPagos(ruta);

            Console.WriteLine();
            Console.WriteLine("Fin del proceso.");
        }
    }
}
