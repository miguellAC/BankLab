using System;
using System.Collections.Generic;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Comun.DTOs.Creditos;
using Ejemplo.WCF.Contratos;

namespace Ejemplo.WCF.Agentes
{
    public class PagosAgente : AgenteBase<IPagosServicio>, IPagosServicio
    {
        private const string EndpointDefault = "net.tcp://localhost:8808/BancoEjemplo/Pagos";
        private const string ConfigKey = "WCF_Endpoint_Pagos";

        public PagosAgente() 
            : base(ObtenerEndpoint(ConfigKey, EndpointDefault))
        {
        }

        public PagosAgente(string endpointAddress) 
            : base(endpointAddress)
        {
        }

        public RespuestaOperacion<long> RegistrarPago(
            int operacionId, decimal montoPago, string tipoPago, string canalPago,
            int? cuentaOrigenId, string referencia, string usuario)
        {
            return EjecutarOperacion(c => c.RegistrarPago(operacionId, montoPago, tipoPago, canalPago, cuentaOrigenId, referencia, usuario), nameof(RegistrarPago));
        }

        public RespuestaOperacion<List<PagoDTO>> ConsultarPagosOperacion(int operacionId, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            return EjecutarOperacion(c => c.ConsultarPagosOperacion(operacionId, fechaDesde, fechaHasta), nameof(ConsultarPagosOperacion));
        }
    }
}
