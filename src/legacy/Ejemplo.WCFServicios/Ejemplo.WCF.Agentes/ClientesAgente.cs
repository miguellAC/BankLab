using System;
using Ejemplo.Comun.DTOs.Clientes;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.WCF.Contratos;

namespace Ejemplo.WCF.Agentes
{
    public class ClientesAgente : AgenteBase<IClientesServicio>, IClientesServicio
    {
        private const string EndpointDefault = "net.tcp://localhost:8808/BancoEjemplo/Clientes";
        private const string ConfigKey = "WCF_Endpoint_Clientes";

        public ClientesAgente() 
            : base(ObtenerEndpoint(ConfigKey, EndpointDefault))
        {
        }

        public ClientesAgente(string endpointAddress) 
            : base(endpointAddress)
        {
        }

        public RespuestaOperacion<ClienteDTO> ConsultarClientePorIdentificacion(int tipoIdentificacionId, string numeroIdentificacion)
        {
            return EjecutarOperacion(c => c.ConsultarClientePorIdentificacion(tipoIdentificacionId, numeroIdentificacion), nameof(ConsultarClientePorIdentificacion));
        }

        public RespuestaOperacion<ClienteDTO> ConsultarClientePorId(int clienteId)
        {
            return EjecutarOperacion(c => c.ConsultarClientePorId(clienteId), nameof(ConsultarClientePorId));
        }

        public RespuestaOperacion<PaginacionDTO<ClienteDTO>> BuscarClientes(
            string nombres, string apellidos, string numeroIdentificacion,
            string estado, int? sucursalId, int pageNumber, int pageSize)
        {
            return EjecutarOperacion(c => c.BuscarClientes(nombres, apellidos, numeroIdentificacion, estado, sucursalId, pageNumber, pageSize), nameof(BuscarClientes));
        }

        public RespuestaOperacion<int> RegistrarCliente(
            ClienteDTO cliente, DireccionDTO direccion, ContactoDTO contacto, string usuario)
        {
            return EjecutarOperacion(c => c.RegistrarCliente(cliente, direccion, contacto, usuario), nameof(RegistrarCliente));
        }
    }
}
