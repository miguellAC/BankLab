using System;
using System.ServiceModel;
using Ejemplo.WCF.Contratos;
using Ejemplo.Comun.DTOs.Clientes;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Componentes.BLL;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.WCF.Servicios
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ClientesServicio : IClientesServicio
    {
        public RespuestaOperacion<ClienteDTO> ConsultarClientePorIdentificacion(int tipoIdentificacionId, string numeroIdentificacion)
        {
            try
            {
                LogHelper.Informacion($"ConsultarClientePorIdentificacion: tipo {tipoIdentificacionId}, num {numeroIdentificacion}");
                var bll = new ClientesBLL();
                return bll.ConsultarCliente(tipoIdentificacionId, numeroIdentificacion);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarClientePorIdentificacion WCF", ex);
                return RespuestaOperacion<ClienteDTO>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<ClienteDTO> ConsultarClientePorId(int clienteId)
        {
            try
            {
                LogHelper.Informacion($"ConsultarClientePorId: {clienteId}");
                var bll = new ClientesBLL();
                return bll.ConsultarClientePorId(clienteId);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarClientePorId WCF", ex);
                return RespuestaOperacion<ClienteDTO>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<PaginacionDTO<ClienteDTO>> BuscarClientes(string nombres, string apellidos, string numeroIdentificacion, string estado, int? sucursalId, int pageNumber, int pageSize)
        {
            try
            {
                LogHelper.Informacion($"BuscarClientes: page {pageNumber}");
                var bll = new ClientesBLL();
                return bll.BuscarClientes(nombres, apellidos, numeroIdentificacion, estado, sucursalId, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                LogHelper.Error("BuscarClientes WCF", ex);
                return RespuestaOperacion<PaginacionDTO<ClienteDTO>>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<int> RegistrarCliente(ClienteDTO cliente, DireccionDTO direccion, ContactoDTO contacto, string usuario)
        {
            try
            {
                LogHelper.Informacion($"RegistrarCliente: por {usuario}");
                var bll = new ClientesBLL();
                return bll.RegistrarCliente(cliente, direccion, contacto, usuario);
            }
            catch (Exception ex)
            {
                LogHelper.Error("RegistrarCliente WCF", ex);
                return RespuestaOperacion<int>.CrearError(ex.Message);
            }
        }
    }
}
