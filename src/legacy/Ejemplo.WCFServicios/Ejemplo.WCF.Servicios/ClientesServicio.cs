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
                LogUtil.Info($"ConsultarClientePorIdentificacion: tipo {tipoIdentificacionId}, num {numeroIdentificacion}");
                var bll = new ClientesBLL();
                var result = bll.ConsultarClientePorIdentificacion(tipoIdentificacionId, numeroIdentificacion);
                return RespuestaOperacion<ClienteDTO>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<ClienteDTO>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<ClienteDTO> ConsultarClientePorId(int clienteId)
        {
            try
            {
                LogUtil.Info($"ConsultarClientePorId: {clienteId}");
                var bll = new ClientesBLL();
                var result = bll.ConsultarClientePorId(clienteId);
                return RespuestaOperacion<ClienteDTO>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<ClienteDTO>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<PaginacionDTO<ClienteDTO>> BuscarClientes(string nombres, string apellidos, string numeroIdentificacion, string estado, int? sucursalId, int pageNumber, int pageSize)
        {
            try
            {
                LogUtil.Info($"BuscarClientes: page {pageNumber}");
                var bll = new ClientesBLL();
                var result = bll.BuscarClientes(nombres, apellidos, numeroIdentificacion, estado, sucursalId, pageNumber, pageSize);
                return RespuestaOperacion<PaginacionDTO<ClienteDTO>>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<PaginacionDTO<ClienteDTO>>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<int> RegistrarCliente(ClienteDTO cliente, DireccionDTO direccion, ContactoDTO contacto, string usuario)
        {
            try
            {
                LogUtil.Info($"RegistrarCliente: por {usuario}");
                var bll = new ClientesBLL();
                var result = bll.RegistrarCliente(cliente, direccion, contacto, usuario);
                return RespuestaOperacion<int>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<int>.CrearError(ex.Message);
            }
        }
    }
}
