using System;
using System.Collections.Generic;
using Ejemplo.Componentes.DAS;
using Ejemplo.Comun.DTOs.Cuentas;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.Componentes.BLL
{
    public class CuentasBLL
    {
        private readonly CuentasDAS _cuentasDAS = new CuentasDAS();

        public RespuestaOperacion<List<CuentaDTO>> ConsultarCuentasCliente(int clienteId)
        {
            try
            {
                if (clienteId <= 0)
                    return RespuestaOperacion<List<CuentaDTO>>.CrearError("ID de cliente inválido.");

                var cuentas = _cuentasDAS.ConsultarCuentasCliente(clienteId);
                return RespuestaOperacion<List<CuentaDTO>>.CrearExitosa(cuentas);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarCuentasCliente BLL", ex);
                return RespuestaOperacion<List<CuentaDTO>>.CrearError("Error al consultar cuentas.");
            }
        }

        public RespuestaOperacion<PaginacionDTO<MovimientoDTO>> ConsultarMovimientosCuenta(int cuentaId, DateTime? desde, DateTime? hasta, int pageNum, int pageSize)
        {
            try
            {
                if (cuentaId <= 0)
                    return RespuestaOperacion<PaginacionDTO<MovimientoDTO>>.CrearError("ID de cuenta inválido.");

                var movimientos = _cuentasDAS.ConsultarMovimientosCuenta(cuentaId, desde, hasta, pageNum, pageSize);
                return RespuestaOperacion<PaginacionDTO<MovimientoDTO>>.CrearExitosa(movimientos);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarMovimientosCuenta BLL", ex);
                return RespuestaOperacion<PaginacionDTO<MovimientoDTO>>.CrearError("Error al consultar movimientos.");
            }
        }

        public RespuestaOperacion<SaldoDTO> ConsultarSaldoCuenta(int cuentaId)
        {
            try
            {
                if (cuentaId <= 0)
                    return RespuestaOperacion<SaldoDTO>.CrearError("ID de cuenta inválido.");

                var saldo = _cuentasDAS.ConsultarSaldoCuenta(cuentaId);
                return saldo != null 
                    ? RespuestaOperacion<SaldoDTO>.CrearExitosa(saldo)
                    : RespuestaOperacion<SaldoDTO>.CrearError("Cuenta no encontrada.");
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarSaldoCuenta BLL", ex);
                return RespuestaOperacion<SaldoDTO>.CrearError("Error al consultar saldo.");
            }
        }
    }
}
