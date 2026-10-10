using System;
using System.ServiceProcess;
using System.Linq;

namespace Ejemplo.Servicios.ProcesadorPagos
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args.Contains("--console"))
            {
                Console.WriteLine("Iniciando en modo consola...");
                var engine = new ProcesadorEngine();
                engine.ScanDirectory();
                Console.WriteLine("Procesamiento completado. Presione cualquier tecla para salir.");
                Console.ReadKey();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new ProcesadorPagosService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
