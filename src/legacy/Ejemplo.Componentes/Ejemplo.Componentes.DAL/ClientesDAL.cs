using System;
using System.Data.SqlClient;
using Ejemplo.Componentes.DAT;
using Ejemplo.Comun.DTOs.Clientes;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.Componentes.DAL
{
    public class ClientesDAL
    {
        private readonly ClientesDAT _clientesDAT = new ClientesDAT();

        public ClienteDTO ConsultarClientePorIdentificacion(int tipoId, string numero)
        {
            if (string.IsNullOrWhiteSpace(numero)) return null;
            return _clientesDAT.ConsultarClientePorIdentificacion(tipoId, numero.Trim());
        }

        public ClienteDTO ConsultarClientePorId(int clienteId)
        {
            return _clientesDAT.ConsultarClientePorId(clienteId);
        }

        public PaginacionDTO<ClienteDTO> BuscarClientes(string nombres, string apellidos, string numId, string estado, int? sucursalId, int pageNumber, int pageSize)
        {
            // Business rule: Normalize names to uppercase
            string n = string.IsNullOrWhiteSpace(nombres) ? null : nombres.Trim().ToUpper();
            string a = string.IsNullOrWhiteSpace(apellidos) ? null : apellidos.Trim().ToUpper();

            return _clientesDAT.BuscarClientes(n, a, numId, estado, sucursalId, pageNumber, pageSize);
        }

        public int RegistrarCliente(SqlConnection conn, SqlTransaction tx, ClienteDTO cliente, DireccionDTO dir, ContactoDTO contacto, string usuario)
        {
            // Business rule: Normalize inputs
            if (cliente.PrimerNombre != null) cliente.PrimerNombre = cliente.PrimerNombre.Trim().ToUpper();
            if (cliente.PrimerApellido != null) cliente.PrimerApellido = cliente.PrimerApellido.Trim().ToUpper();
            return _clientesDAT.RegistrarCliente(conn, tx, cliente, dir, contacto, usuario);
        }
    }
}
