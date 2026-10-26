using System;
using System.Collections.Generic;
using System.ServiceModel;
using Ejemplo.Comun.DTOs.Creditos;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.WCF.Contratos
{
    [ServiceContract(Namespace = "http://bancoejemplo.com/servicios/creditos")]
    public interface ICreditosServicio
    {
        [OperationContract]
        RespuestaOperacion<OperacionCreditoDTO> ConsultarOperacionCredito(int? operacionId, string numeroOperacion);

        [OperationContract]
        RespuestaOperacion<SaldoOperacionDTO> ConsultarSaldosVigenteVencido(int operacionId);

        [OperationContract]
        RespuestaOperacion<List<CuotaDTO>> ConsultarCuotasOperacion(int operacionId, bool soloVencidas, bool soloPendientes);

        [OperationContract]
        RespuestaOperacion<List<CuotaDTO>> ConsultarCuotasPendientesVencidas(int? clienteId);
    }
}
