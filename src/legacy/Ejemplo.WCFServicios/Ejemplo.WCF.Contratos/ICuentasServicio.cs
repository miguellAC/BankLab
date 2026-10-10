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
        RespuestaOperacion<List<MovimientoCuentaDTO>> ConsultarMovimientosCuenta(int cuentaId, int pageNumber, int pageSize);

        [OperationContract]
        RespuestaOperacion<decimal> ConsultarSaldoCuenta(int cuentaId);
    }
}
