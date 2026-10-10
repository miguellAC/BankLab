using System;
using Ejemplo.Componentes.DAL;
using Ejemplo.Comun.DTOs.Clientes;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.Componentes.DAS
{
    public class ClientesDAS
    {
        private readonly ClientesDAL _clientesDAL = new ClientesDAL();
        private readonly AuditoriaDAL _auditoriaDAL = new AuditoriaDAL();

        public ClienteDTO ConsultarCliente(int tipoId, string numero)
        {
            return _clientesDAL.ConsultarClientePorIdentificacion(tipoId, numero);
        }

        public ClienteDTO ConsultarClientePorId(int clienteId)
        {
            return _clientesDAL.ConsultarClientePorId(clienteId);
        }

        public PaginacionDTO<ClienteDTO> BuscarClientes(string nombres, string apellidos, string numId, string estado, int? sucursalId, int pageNumber, int pageSize)
        {
            return _clientesDAL.BuscarClientes(nombres, apellidos, numId, estado, sucursalId, pageNumber, pageSize);
        }

        public int RegistrarCliente(ClienteDTO cliente, DireccionDTO dir, ContactoDTO contacto, string usuario)
        {
            return TransaccionHelper.EjecutarEnTransaccion((conn, tx) =>
            {
                int clienteId = _clientesDAL.RegistrarCliente(conn, tx, cliente, dir, contacto, usuario);
                
                _auditoriaDAL.RegistrarAuditoria(conn, tx, "CLIENTE", clienteId, "CREAR", "", cliente.NumeroIdentificacion, "127.0.0.1", "APP", usuario);
                
                return clienteId;
            });
        }
    }
}
