using System;
using System.Configuration;
using System.ServiceModel;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.WCF.Agentes
{
    /// <summary>
    /// Clase base para todos los agentes WCF del lado del cliente.
    /// Gestiona la creación, uso y cierre del canal de comunicación NetTcp.
    /// 
    /// IMPORTANTE: Un agente NO es un servicio. Es un componente del lado del cliente
    /// que encapsula la comunicación con el servicio WCF remoto. La GUI y los shells
    /// usan agentes para consumir los servicios sin conocer los detalles de WCF.
    /// </summary>
    /// <typeparam name="TContrato">Interfaz del contrato de servicio WCF</typeparam>
    public abstract class AgenteBase<TContrato> : IDisposable where TContrato : class
    {
        private ChannelFactory<TContrato> _factory;
        private TContrato _canal;
        private readonly string _endpointAddress;
        private readonly object _lock = new object();

        /// <summary>
        /// Inicializa el agente con la dirección del endpoint WCF.
        /// </summary>
        /// <param name="endpointAddress">Dirección net.tcp del servicio</param>
        protected AgenteBase(string endpointAddress)
        {
            _endpointAddress = endpointAddress ?? throw new ArgumentNullException(nameof(endpointAddress));
        }

        /// <summary>
        /// Obtiene el canal de comunicación WCF. Crea uno nuevo si no existe o está en estado Faulted.
        /// </summary>
        protected TContrato Canal
        {
            get
            {
                lock (_lock)
                {
                    if (_canal == null || ((ICommunicationObject)_canal).State == CommunicationState.Faulted)
                    {
                        CerrarCanal();
                        CrearCanal();
                    }
                    return _canal;
                }
            }
        }

        private void CrearCanal()
        {
            var binding = new NetTcpBinding
            {
                MaxReceivedMessageSize = int.MaxValue,
                MaxBufferSize = int.MaxValue,
                OpenTimeout = TimeSpan.FromMinutes(1),
                CloseTimeout = TimeSpan.FromMinutes(1),
                SendTimeout = TimeSpan.FromMinutes(5),
                ReceiveTimeout = TimeSpan.FromMinutes(10),
                Security = { Mode = SecurityMode.None }
            };
            binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;

            var endpoint = new EndpointAddress(_endpointAddress);
            _factory = new ChannelFactory<TContrato>(binding, endpoint);
            _canal = _factory.CreateChannel();
            ((ICommunicationObject)_canal).Open();
            LogHelper.Informacion($"Canal WCF abierto hacia {_endpointAddress}");
        }

        private void CerrarCanal()
        {
            try
            {
                if (_canal != null)
                {
                    var commObj = (ICommunicationObject)_canal;
                    if (commObj.State == CommunicationState.Faulted)
                        commObj.Abort();
                    else if (commObj.State != CommunicationState.Closed)
                        commObj.Close();
                }
            }
            catch { ((ICommunicationObject)_canal)?.Abort(); }
            finally { _canal = null; }

            try { _factory?.Close(); } catch { _factory?.Abort(); } finally { _factory = null; }
        }

        /// <summary>
        /// Ejecuta una operación en el servicio WCF con manejo automático de errores de comunicación.
        /// Si el canal falla, intenta reconectar una vez antes de propagar el error.
        /// </summary>
        protected TResult EjecutarOperacion<TResult>(Func<TContrato, TResult> operacion, string nombreOperacion)
        {
            try
            {
                LogHelper.Debug($"Agente: Ejecutando {nombreOperacion}");
                return operacion(Canal);
            }
            catch (CommunicationException ex)
            {
                LogHelper.Advertencia($"Error de comunicación en {nombreOperacion}. Reintentando...");
                CerrarCanal();
                try
                {
                    return operacion(Canal);
                }
                catch (Exception retryEx)
                {
                    LogHelper.Error($"Error en reintento de {nombreOperacion}", retryEx);
                    throw new InvalidOperationException(
                        $"No se pudo comunicar con el servicio WCF ({nombreOperacion}). " +
                        "Verifique que el host WCF esté activo en net.tcp://localhost:8808.", retryEx);
                }
            }
            catch (TimeoutException ex)
            {
                LogHelper.Error($"Timeout en {nombreOperacion}", ex);
                CerrarCanal();
                throw new InvalidOperationException(
                    $"El servicio WCF no respondió a tiempo ({nombreOperacion}).", ex);
            }
        }

        /// <summary>
        /// Lee la dirección del endpoint desde la configuración de la aplicación.
        /// Si no existe, devuelve el valor predeterminado.
        /// </summary>
        protected static string ObtenerEndpoint(string claveConfig, string valorPredeterminado)
        {
            string valor = ConfigurationManager.AppSettings[claveConfig];
            return string.IsNullOrEmpty(valor) ? valorPredeterminado : valor;
        }

        public void Dispose()
        {
            CerrarCanal();
        }
    }
}
