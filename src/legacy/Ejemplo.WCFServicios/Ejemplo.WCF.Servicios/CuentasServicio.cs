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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CuentasServicio : ICuentasServicio
    {
        public RespuestaOperacion<List<CuentaDTO>> ConsultarCuentasCliente(int clienteId)
        {
            try
            {
                LogUtil.Info($"ConsultarCuentasCliente: {clienteId}");
                var bll = new CuentasBLL();
                var result = bll.ConsultarCuentasCliente(clienteId);
                return RespuestaOperacion<List<CuentaDTO>>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<List<CuentaDTO>>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<List<MovimientoCuentaDTO>> ConsultarMovimientosCuenta(int cuentaId, int pageNumber, int pageSize)
        {
            try
            {
                LogUtil.Info($"ConsultarMovimientosCuenta: {cuentaId}");
                var bll = new CuentasBLL();
                var result = bll.ConsultarMovimientosCuenta(cuentaId, pageNumber, pageSize);
                return RespuestaOperacion<List<MovimientoCuentaDTO>>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<List<MovimientoCuentaDTO>>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<decimal> ConsultarSaldoCuenta(int cuentaId)
        {
            try
            {
                LogUtil.Info($"ConsultarSaldoCuenta: {cuentaId}");
                var bll = new CuentasBLL();
                var result = bll.ConsultarSaldoCuenta(cuentaId);
                return RespuestaOperacion<decimal>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<decimal>.CrearError(ex.Message);
            }
        }
    }
}
