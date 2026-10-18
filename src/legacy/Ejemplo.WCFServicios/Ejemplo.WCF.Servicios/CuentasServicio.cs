using System;
using System.Collections.Generic;
using System.ServiceModel;
using Ejemplo.WCF.Contratos;
using Ejemplo.Comun.DTOs.Cuentas;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Componentes.BLL;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.WCF.Servicios
{
    /// <summary>
    /// Servicio WCF de cuentas. La BLL ya devuelve RespuestaOperacion&lt;T&gt;, por lo que el servicio
    /// la reenvia tal cual (sin envolverla por segunda vez) y solo captura excepciones inesperadas.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CuentasServicio : ICuentasServicio
    {
        private const string MensajeErrorInterno = "Error interno del servicio. Consulte el registro del servidor.";

        public RespuestaOperacion<List<CuentaDTO>> ConsultarCuentasCliente(int clienteId)
        {
            try
            {
                LogHelper.Informacion($"ConsultarCuentasCliente: {clienteId}");
                return new CuentasBLL().ConsultarCuentasCliente(clienteId);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarCuentasCliente (servicio)", ex);
                return RespuestaOperacion<List<CuentaDTO>>.CrearError(MensajeErrorInterno, "ERR_INTERNO");
            }
        }

        public RespuestaOperacion<PaginacionDTO<MovimientoDTO>> ConsultarMovimientosCuenta(
            int cuentaId, DateTime? fechaDesde, DateTime? fechaHasta, int pageNumber, int pageSize)
        {
            try
            {
                LogHelper.Informacion($"ConsultarMovimientosCuenta: {cuentaId}");
                return new CuentasBLL().ConsultarMovimientosCuenta(cuentaId, fechaDesde, fechaHasta, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarMovimientosCuenta (servicio)", ex);
                return RespuestaOperacion<PaginacionDTO<MovimientoDTO>>.CrearError(MensajeErrorInterno, "ERR_INTERNO");
            }
        }

        public RespuestaOperacion<SaldoDTO> ConsultarSaldoCuenta(int cuentaId)
        {
            try
            {
                LogHelper.Informacion($"ConsultarSaldoCuenta: {cuentaId}");
                return new CuentasBLL().ConsultarSaldoCuenta(cuentaId);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarSaldoCuenta (servicio)", ex);
                return RespuestaOperacion<SaldoDTO>.CrearError(MensajeErrorInterno, "ERR_INTERNO");
            }
        }
    }
}
