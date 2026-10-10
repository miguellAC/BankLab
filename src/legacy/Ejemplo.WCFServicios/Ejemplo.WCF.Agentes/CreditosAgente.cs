using System;
using System.Collections.Generic;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Comun.DTOs.Creditos;
using Ejemplo.WCF.Contratos;

namespace Ejemplo.WCF.Agentes
{
    public class CreditosAgente : AgenteBase<ICreditosServicio>, ICreditosServicio
    {
        private const string EndpointDefault = "net.tcp://localhost:8808/BancoEjemplo/Creditos";
        private const string ConfigKey = "WCF_Endpoint_Creditos";

        public CreditosAgente() 
            : base(ObtenerEndpoint(ConfigKey, EndpointDefault))
        {
        }

        public CreditosAgente(string endpointAddress) 
            : base(endpointAddress)
        {
        }

        public RespuestaOperacion<OperacionCreditoDTO> ConsultarOperacionCredito(int? operacionId, string numeroOperacion)
        {
            return EjecutarOperacion(c => c.ConsultarOperacionCredito(operacionId, numeroOperacion), nameof(ConsultarOperacionCredito));
        }

        public RespuestaOperacion<SaldoOperacionDTO> ConsultarSaldosVigenteVencido(int operacionId)
        {
            return EjecutarOperacion(c => c.ConsultarSaldosVigenteVencido(operacionId), nameof(ConsultarSaldosVigenteVencido));
        }

        public RespuestaOperacion<List<CuotaDTO>> ConsultarCuotasOperacion(int operacionId, bool soloVencidas, bool soloPendientes)
        {
            return EjecutarOperacion(c => c.ConsultarCuotasOperacion(operacionId, soloVencidas, soloPendientes), nameof(ConsultarCuotasOperacion));
        }
    }
}
