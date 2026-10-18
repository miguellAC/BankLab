using System;
using System.Collections.Generic;
using System.ServiceModel;
using Ejemplo.Comun.DTOs.Cuentas;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.WCF.Contratos
{
    [ServiceContract(Namespace = "http://bancoejemplo.com/servicios/cuentas")]
    public interface ICuentasServicio
    {
        [OperationContract]
        RespuestaOperacion<List<CuentaDTO>> ConsultarCuentasCliente(int clienteId);

        [OperationContract]
        RespuestaOperacion<PaginacionDTO<MovimientoDTO>> ConsultarMovimientosCuenta(int cuentaId, DateTime? fechaDesde, DateTime? fechaHasta, int pageNumber, int pageSize);

        [OperationContract]
        RespuestaOperacion<SaldoDTO> ConsultarSaldoCuenta(int cuentaId);
    }
}
