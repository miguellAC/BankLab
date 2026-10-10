using System.ServiceModel;
using Ejemplo.Comun.DTOs.Clientes;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.WCF.Contratos
{
    /// <summary>
    /// Contrato de servicio para operaciones de clientes.
    /// </summary>
    [ServiceContract(Namespace = "http://bancoejemplo.com/servicios/clientes")]
    public interface IClientesServicio
    {
        [OperationContract]
        RespuestaOperacion<ClienteDTO> ConsultarClientePorIdentificacion(int tipoIdentificacionId, string numeroIdentificacion);

        [OperationContract]
        RespuestaOperacion<ClienteDTO> ConsultarClientePorId(int clienteId);

        [OperationContract]
        RespuestaOperacion<PaginacionDTO<ClienteDTO>> BuscarClientes(string nombres, string apellidos, string numeroIdentificacion, string estado, int? sucursalId, int pageNumber, int pageSize);

        [OperationContract]
        RespuestaOperacion<int> RegistrarCliente(ClienteDTO cliente, DireccionDTO direccion, ContactoDTO contacto, string usuario);
    }
}
