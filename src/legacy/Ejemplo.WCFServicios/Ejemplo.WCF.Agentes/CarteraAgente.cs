using System;
using System.Collections.Generic;
using Ejemplo.Comun.DTOs.Cartera;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.WCF.Contratos;

namespace Ejemplo.WCF.Agentes
{
    public class CarteraAgente : AgenteBase<ICarteraServicio>, ICarteraServicio
    {
        private const string EndpointDefault = "net.tcp://localhost:8808/BancoEjemplo/Cartera";
        private const string ConfigKey = "WCF_Endpoint_Cartera";

        public CarteraAgente() 
            : base(ObtenerEndpoint(ConfigKey, EndpointDefault))
        {
        }

        public CarteraAgente(string endpointAddress) 
            : base(endpointAddress)
        {
        }

        public RespuestaOperacion<List<ReporteCarteraDTO>> ObtenerReporteCartera(
            DateTime? fechaCorte, int? sucursalId, string estado, string categoriaRiesgo)
        {
            return EjecutarOperacion(c => c.ObtenerReporteCartera(fechaCorte, sucursalId, estado, categoriaRiesgo), nameof(ObtenerReporteCartera));
        }

        public RespuestaOperacion<List<ReporteCarteraDTO>> ObtenerReporteCarteraVencida(int diasMinimo)
        {
            return EjecutarOperacion(c => c.ObtenerReporteCarteraVencida(diasMinimo), nameof(ObtenerReporteCarteraVencida));
        }
    }
}
