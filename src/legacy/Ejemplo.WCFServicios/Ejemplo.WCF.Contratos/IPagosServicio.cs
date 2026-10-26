using System;
using System.Collections.Generic;
using System.ServiceModel;
using Ejemplo.Comun.DTOs.Creditos;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.WCF.Contratos
{
    [ServiceContract(Namespace = "http://bancoejemplo.com/servicios/pagos")]
    public interface IPagosServicio
    {
        [OperationContract]
        RespuestaOperacion<long> RegistrarPago(int operacionId, decimal montoPago, string tipoPago, string canalPago, int? cuentaOrigenId, string referencia, string usuario);

        [OperationContract]
        RespuestaOperacion<List<PagoDTO>> ConsultarPagosOperacion(int operacionId, DateTime? fechaDesde, DateTime? fechaHasta);
    }
}
