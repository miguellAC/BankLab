using System.Collections.Generic;
using System.ServiceModel;
using Ejemplo.Comun.DTOs.Pagos;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.WCF.Contratos
{
    [ServiceContract(Namespace = "http://bancoejemplo.com/servicios/pagos")]
    public interface IPagosServicio
    {
        [OperationContract]
        RespuestaOperacion<PagoRealizadoDTO> RegistrarPago(PagoDTO pago, string usuario);

        [OperationContract]
        RespuestaOperacion<List<PagoRealizadoDTO>> ConsultarPagosOperacion(int operacionId);
    }
}
