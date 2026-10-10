using System.ServiceModel;
using Ejemplo.Comun.DTOs.Cartera;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.WCF.Contratos
{
    [ServiceContract(Namespace = "http://bancoejemplo.com/servicios/cartera")]
    public interface ICarteraServicio
    {
        [OperationContract]
        RespuestaOperacion<ReporteCarteraDTO> ObtenerReporteCartera(int sucursalId);
    }
}
