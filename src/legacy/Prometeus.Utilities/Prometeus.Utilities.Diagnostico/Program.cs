using System;

namespace Prometeus.Utilities.Diagnostico
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Prometeus - Diagnóstico del Entorno";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========================================");
            Console.WriteLine("  Prometeus - Diagnóstico del Sistema");
            Console.WriteLine("  Banco Ejemplo - BankLab");
            Console.WriteLine("========================================");
            Console.ResetColor();
            Console.WriteLine();

            // 1. Diagnóstico SQL Server
            Console.Write("Verificando Base de Datos SQL Server... ");
            if (DiagnosticoConfig.ProbarConexionBD(out string msgBD))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[OK]");
                Console.ResetColor();
                Console.WriteLine($"   {msgBD}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[FALLO]");
                Console.ResetColor();
                Console.WriteLine($"   {msgBD}");
            }
            Console.WriteLine();

            // 2. Diagnóstico WCF NetTcp
            Console.Write("Verificando Endpoint WCF NetTcp (8808)... ");
            if (DiagnosticoConfig.ProbarPuertoWCF("localhost", 8808, out string msgWCF))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[OK]");
                Console.ResetColor();
                Console.WriteLine($"   {msgWCF}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[ADVERTENCIA]");
                Console.ResetColor();
                Console.WriteLine($"   {msgWCF}");
            }
            Console.WriteLine();

            Console.WriteLine("Diagnóstico finalizado. Presione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
