using System;
using System.ServiceModel;
using Ejemplo.WCF.Servicios;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.WCF.Host
{
    /// <summary>
    /// Host de consola auto-alojado para los servicios WCF de Banco Ejemplo.
    /// Utiliza transporte NetTcp en el puerto 8808.
    /// Este es el modo principal de ejecución para desarrollo y depuración.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Banco Ejemplo - WCF Host NetTcp";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=======================================");
            Console.WriteLine("  Banco Ejemplo - Servicios WCF");
            Console.WriteLine("  Transporte: NetTcp");
            Console.WriteLine("  Puerto: 8808");
            Console.WriteLine("=======================================");
            Console.ResetColor();
            Console.WriteLine();

            ServiceHost hostClientes = null;
            ServiceHost hostCuentas = null;
            ServiceHost hostCreditos = null;
            ServiceHost hostPagos = null;
            ServiceHost hostCartera = null;

            try
            {
                hostClientes = new ServiceHost(typeof(ClientesServicio));
                hostCuentas = new ServiceHost(typeof(CuentasServicio));
                hostCreditos = new ServiceHost(typeof(CreditosServicio));
                hostPagos = new ServiceHost(typeof(PagosServicio));
                hostCartera = new ServiceHost(typeof(CarteraServicio));

                hostClientes.Open();
                ImprimirEstado("Clientes", hostClientes);

                hostCuentas.Open();
                ImprimirEstado("Cuentas", hostCuentas);

                hostCreditos.Open();
                ImprimirEstado("Créditos", hostCreditos);

                hostPagos.Open();
                ImprimirEstado("Pagos", hostPagos);

                hostCartera.Open();
                ImprimirEstado("Cartera", hostCartera);

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Todos los servicios están activos.");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("Presione ENTER para detener los servicios...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error al iniciar servicios: {ex.Message}");
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
                LogHelper.Error("Error al iniciar host WCF", ex);
                Console.WriteLine("Presione ENTER para salir...");
                Console.ReadLine();
            }
            finally
            {
                CerrarHost(hostClientes);
                CerrarHost(hostCuentas);
                CerrarHost(hostCreditos);
                CerrarHost(hostPagos);
                CerrarHost(hostCartera);
                Console.WriteLine("Servicios detenidos.");
            }
        }

        static void ImprimirEstado(string nombre, ServiceHost host)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"  [OK] ");
            Console.ResetColor();
            Console.Write($"Servicio de {nombre} iniciado en: ");
            foreach (var endpoint in host.Description.Endpoints)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(endpoint.Address.Uri);
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        static void CerrarHost(ServiceHost host)
        {
            try
            {
                if (host != null && host.State != CommunicationState.Closed)
                    host.Close();
            }
            catch
            {
                host?.Abort();
            }
        }
    }
}
