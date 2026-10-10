using System;
using System.Collections.Generic;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Comun.DTOs.Cuentas;
using Ejemplo.WCF.Contratos;

namespace Ejemplo.WCF.Agentes
{
    public class CuentasAgente : AgenteBase<ICuentasServicio>, ICuentasServicio
    {
        private const string EndpointDefault = "net.tcp://localhost:8808/BancoEjemplo/Cuentas";
        private const string ConfigKey = "WCF_Endpoint_Cuentas";

        public CuentasAgente() 
            : base(ObtenerEndpoint(ConfigKey, EndpointDefault))
        {
        }

        public CuentasAgente(string endpointAddress) 
            : base(endpointAddress)
        {
        }

        public RespuestaOperacion<List<CuentaDTO>> ConsultarCuentasCliente(int clienteId)
        {
            return EjecutarOperacion(c => c.ConsultarCuentasCliente(clienteId), nameof(ConsultarCuentasCliente));
        }

        public RespuestaOperacion<PaginacionDTO<MovimientoDTO>> ConsultarMovimientosCuenta(
            int cuentaId, DateTime? fechaDesde, DateTime? fechaHasta, int pageNumber, int pageSize)
        {
            return EjecutarOperacion(c => c.ConsultarMovimientosCuenta(cuentaId, fechaDesde, fechaHasta, pageNumber, pageSize), nameof(ConsultarMovimientosCuenta));
        }

        public RespuestaOperacion<SaldoDTO> ConsultarSaldoCuenta(int cuentaId)
        {
            return EjecutarOperacion(c => c.ConsultarSaldoCuenta(cuentaId), nameof(ConsultarSaldoCuenta));
        }
    }
}
