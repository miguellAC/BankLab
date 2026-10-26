using System;
using System.Collections.Generic;
using System.ServiceModel;
using Ejemplo.Comun.DTOs.Cartera;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.WCF.Contratos
{
    [ServiceContract(Namespace = "http://bancoejemplo.com/servicios/cartera")]
    public interface ICarteraServicio
    {
        [OperationContract]
        RespuestaOperacion<List<ReporteCarteraDTO>> ObtenerReporteCartera(
            DateTime? fechaCorte, int? sucursalId, string estado, string categoriaRiesgo);

        [OperationContract]
        RespuestaOperacion<List<ReporteCarteraDTO>> ObtenerReporteCarteraVencida(int diasMinimo);
    }
}
