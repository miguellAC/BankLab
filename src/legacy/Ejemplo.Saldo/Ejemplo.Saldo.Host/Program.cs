using System;
using System.ServiceModel;
using Ejemplo.WCF.Servicios;

namespace Ejemplo.Saldo.Host
{
    /// <summary>
    /// Host de consola minimo del laboratorio "Saldo": aloja unicamente CuentasServicio
    /// (la configuracion del servicio, el binding y el endpoint estan en App.config).
    /// No contiene logica de negocio: reutiliza Ejemplo.WCF.Servicios y las capas BLL/DAS/DAL/DAT existentes.
    /// </summary>
    internal static class Program
    {
        private static int Main()
        {
            Console.Title = "BankLab - Host WCF (rebanada Saldo)";
            Console.WriteLine("BankLab - Host WCF (rebanada Saldo) - solo CuentasServicio");

            ServiceHost host = null;
            try
            {
                host = new ServiceHost(typeof(CuentasServicio));
                host.Open();

                foreach (var endpoint in host.Description.Endpoints)
                    Console.WriteLine("  [OK] Escuchando en: " + endpoint.Address.Uri);

                Console.WriteLine("Presione ENTER para detener el servicio.");
                Console.ReadLine();
                return 0;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No se pudo iniciar el servicio: " + ex.Message);
                Console.ResetColor();
                return 1;
            }
            finally
            {
                if (host != null)
                {
                    try { if (host.State != CommunicationState.Faulted) host.Close(); else host.Abort(); }
                    catch { host.Abort(); }
                }
            }
        }
    }
}
