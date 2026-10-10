using System;
using System.Collections.Generic;
using Ejemplo.Componentes.DAT;
using Ejemplo.Comun.DTOs.Cuentas;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.Componentes.DAL
{
    public class CuentasDAL
    {
        private readonly CuentasDAT _cuentasDAT = new CuentasDAT();

        public List<CuentaDTO> ConsultarCuentasCliente(int clienteId)
        {
            return _cuentasDAT.ConsultarCuentasCliente(clienteId);
        }

        public PaginacionDTO<MovimientoDTO> ConsultarMovimientosCuenta(int cuentaId, DateTime? desde, DateTime? hasta, int pageNum, int pageSize)
        {
            // Business rule: Validate page size limits
            if (pageSize > 100)
            {
                pageSize = 100; // Cap to maximum 100 records per page
            }
            if (pageSize <= 0)
            {
                pageSize = 10; // Default
            }
            
            return _cuentasDAT.ConsultarMovimientosCuenta(cuentaId, desde, hasta, pageNum, pageSize);
        }

        public SaldoDTO ConsultarSaldoCuenta(int cuentaId)
        {
            return _cuentasDAT.ConsultarSaldoCuenta(cuentaId);
        }
    }
}
