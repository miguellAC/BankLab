using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Ejemplo.Servicios.ProcesadorPagos
{
    [RunInstaller(true)]
    public class ProcesadorPagosInstaller : Installer
    {
        public ProcesadorPagosInstaller()
        {
            var processInstaller = new ServiceProcessInstaller();
            var serviceInstaller = new ServiceInstaller();

            processInstaller.Account = ServiceAccount.LocalService;

            serviceInstaller.ServiceName = "ProcesadorPagosBancoEjemplo";
            serviceInstaller.DisplayName = "Procesador de Pagos Banco Ejemplo";
            serviceInstaller.Description = "Servicio de procesamiento de archivos de pagos CSV";
            serviceInstaller.StartType = ServiceStartMode.Manual;

            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
