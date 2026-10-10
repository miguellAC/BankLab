using System;
using Ejemplo.Componentes.DAS;
using Ejemplo.Comun.DTOs.Clientes;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Comun.Validaciones;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.Componentes.BLL
{
    public class ClientesBLL
    {
        private readonly ClientesDAS _clientesDAS = new ClientesDAS();

        public RespuestaOperacion<ClienteDTO> ConsultarCliente(int tipoId, string numero)
        {
            try
            {
                var validador = new ValidadorIdentificacion();
                if (!validador.ValidarFormato(tipoId, numero))
                {
                    return RespuestaOperacion<ClienteDTO>.Error("Identificación inválida.");
                }

                var cliente = _clientesDAS.ConsultarCliente(tipoId, numero);
                if (cliente == null)
                    return RespuestaOperacion<ClienteDTO>.Error("Cliente no encontrado.");

                return RespuestaOperacion<ClienteDTO>.Exito(cliente);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarCliente BLL", ex);
                return RespuestaOperacion<ClienteDTO>.Error("Error interno al consultar el cliente.");
            }
        }

        public RespuestaOperacion<ClienteDTO> ConsultarClientePorId(int clienteId)
        {
            try
            {
                if (clienteId <= 0)
                    return RespuestaOperacion<ClienteDTO>.Error("ID de cliente debe ser mayor a 0.");

                var cliente = _clientesDAS.ConsultarClientePorId(clienteId);
                return cliente != null 
                    ? RespuestaOperacion<ClienteDTO>.Exito(cliente) 
                    : RespuestaOperacion<ClienteDTO>.Error("Cliente no encontrado.");
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarClientePorId BLL", ex);
                return RespuestaOperacion<ClienteDTO>.Error("Error interno.");
            }
        }

        public RespuestaOperacion<PaginacionDTO<ClienteDTO>> BuscarClientes(string nombres, string apellidos, string numId, string estado, int? sucursalId, int pageNumber, int pageSize)
        {
            try
            {
                if (pageNumber <= 0) pageNumber = 1;
                if (pageSize <= 0) pageSize = 10;

                var result = _clientesDAS.BuscarClientes(nombres, apellidos, numId, estado, sucursalId, pageNumber, pageSize);
                return RespuestaOperacion<PaginacionDTO<ClienteDTO>>.Exito(result);
            }
            catch (Exception ex)
            {
                LogHelper.Error("BuscarClientes BLL", ex);
                return RespuestaOperacion<PaginacionDTO<ClienteDTO>>.Error("Error al buscar clientes.");
            }
        }

        public RespuestaOperacion<int> RegistrarCliente(ClienteDTO cliente, DireccionDTO dir, ContactoDTO contacto, string usuario)
        {
            try
            {
                if (cliente == null)
                    return RespuestaOperacion<int>.Error("Datos del cliente requeridos.");

                var validador = new ValidadorIdentificacion();
                if (!validador.ValidarFormato(cliente.TipoIdentificacion, cliente.NumeroIdentificacion))
                {
                    return RespuestaOperacion<int>.Error("Identificación inválida.");
                }

                int id = _clientesDAS.RegistrarCliente(cliente, dir, contacto, usuario);
                return RespuestaOperacion<int>.Exito(id);
            }
            catch (Exception ex)
            {
                LogHelper.Error("RegistrarCliente BLL", ex);
                return RespuestaOperacion<int>.Error("Error al registrar cliente.");
            }
        }
    }
}
