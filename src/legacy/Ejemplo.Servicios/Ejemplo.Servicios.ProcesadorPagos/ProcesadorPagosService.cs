using System.ServiceProcess;
using System.Timers;

namespace Ejemplo.Servicios.ProcesadorPagos
{
    public partial class ProcesadorPagosService : ServiceBase
    {
        private Timer _timer;
        private ProcesadorEngine _engine;

        public ProcesadorPagosService()
        {
            ServiceName = "Ejemplo.Servicios.ProcesadorPagos";
        }

        protected override void OnStart(string[] args)
        {
            _engine = new ProcesadorEngine();
            _timer = new Timer(30000); // 30 segundos
            _timer.Elapsed += (sender, e) => _engine.ScanDirectory();
            _timer.Start();
        }

        protected override void OnStop()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }
        }
    }
}
