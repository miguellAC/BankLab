using System;
using System.Collections.Generic;
using Ejemplo.Componentes.DAL;
using Ejemplo.Comun.DTOs.Cuentas;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.Componentes.DAS
{
    public class CuentasDAS
    {
        private readonly CuentasDAL _cuentasDAL = new CuentasDAL();

        public List<CuentaDTO> ConsultarCuentasCliente(int clienteId)
        {
            return _cuentasDAL.ConsultarCuentasCliente(clienteId);
        }

        public PaginacionDTO<MovimientoDTO> ConsultarMovimientosCuenta(int cuentaId, DateTime? desde, DateTime? hasta, int pageNum, int pageSize)
        {
            return _cuentasDAL.ConsultarMovimientosCuenta(cuentaId, desde, hasta, pageNum, pageSize);
        }

        public SaldoDTO ConsultarSaldoCuenta(int cuentaId)
        {
            return _cuentasDAL.ConsultarSaldoCuenta(cuentaId);
        }
    }
}
