using System;
using System.Data;
using System.Data.SqlClient;
using Ejemplo.Comun.DTOs.Clientes;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.Componentes.DAT
{
    public class ClientesDAT
    {
        public ClienteDTO ConsultarClientePorIdentificacion(int tipoId, string numero)
        {
            SqlConnection conn = null;
            try
            {
                conn = ConexionHelper.ObtenerConexion();
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, "sp_ConsultarClientePorIdentificacion"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@TipoIdentificacionId", tipoId);
                    ConexionHelper.AgregarParametro(cmd, "@NumeroIdentificacion", numero);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapearCliente(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en ConsultarClientePorIdentificacion", ex);
                throw;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            return null;
        }

        public ClienteDTO ConsultarClientePorId(int clienteId)
        {
            SqlConnection conn = null;
            try
            {
                conn = ConexionHelper.ObtenerConexion();
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, "sp_ConsultarClientePorId"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@ClienteId", clienteId);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapearCliente(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en ConsultarClientePorId", ex);
                throw;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            return null;
        }

        public PaginacionDTO<ClienteDTO> BuscarClientes(string nombres, string apellidos, string numId, string estado, int? sucursalId, int pageNumber, int pageSize)
        {
            var result = new PaginacionDTO<ClienteDTO>();
            SqlConnection conn = null;
            try
            {
                conn = ConexionHelper.ObtenerConexion();
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, "sp_BuscarClientes"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@Nombres", nombres);
                    ConexionHelper.AgregarParametro(cmd, "@Apellidos", apellidos);
                    ConexionHelper.AgregarParametro(cmd, "@NumeroIdentificacion", numId);
                    ConexionHelper.AgregarParametro(cmd, "@Estado", estado);
                    ConexionHelper.AgregarParametro(cmd, "@SucursalId", sucursalId);
                    ConexionHelper.AgregarParametro(cmd, "@PageNumber", pageNumber);
                    ConexionHelper.AgregarParametro(cmd, "@PageSize", pageSize);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result.TotalRecords = ConexionHelper.LeerValor<int>(reader, "TotalRegistros");
                        }

                        if (reader.NextResult())
                        {
                            result.Items = new System.Collections.Generic.List<ClienteDTO>();
                            while (reader.Read())
                            {
                                result.Items.Add(MapearCliente(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en BuscarClientes", ex);
                throw;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            return result;
        }

        public int RegistrarCliente(SqlConnection conn, SqlTransaction tx, ClienteDTO cliente, DireccionDTO dir, ContactoDTO contacto, string usuario)
        {
            try
            {
                using (SqlCommand cmd = ConexionHelper.CrearComandoSP(conn, tx, "sp_RegistrarCliente"))
                {
                    ConexionHelper.AgregarParametro(cmd, "@PrimerNombre", cliente.PrimerNombre);
                    ConexionHelper.AgregarParametro(cmd, "@PrimerApellido", cliente.PrimerApellido);
                    ConexionHelper.AgregarParametro(cmd, "@TipoIdentificacionId", cliente.TipoIdentificacionId);
                    ConexionHelper.AgregarParametro(cmd, "@NumeroIdentificacion", cliente.NumeroIdentificacion);
                    ConexionHelper.AgregarParametro(cmd, "@FechaNacimiento", cliente.FechaNacimiento);
                    ConexionHelper.AgregarParametro(cmd, "@Usuario", usuario);

                    SqlParameter outputId = new SqlParameter("@ClienteId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(outputId);

                    cmd.ExecuteNonQuery();

                    return (int)outputId.Value;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error en RegistrarCliente", ex);
                throw;
            }
        }

        private ClienteDTO MapearCliente(SqlDataReader reader)
        {
            return new ClienteDTO
            {
                ClienteId = ConexionHelper.LeerValor<int>(reader, "ClienteId"),
                TipoIdentificacionId = ConexionHelper.LeerValor<int>(reader, "TipoIdentificacionId"),
                NumeroIdentificacion = ConexionHelper.LeerValor<string>(reader, "NumeroIdentificacion"),
                PrimerNombre = ConexionHelper.LeerValor<string>(reader, "PrimerNombre"),
                SegundoNombre = ConexionHelper.LeerValor<string>(reader, "SegundoNombre"),
                PrimerApellido = ConexionHelper.LeerValor<string>(reader, "PrimerApellido"),
                SegundoApellido = ConexionHelper.LeerValor<string>(reader, "SegundoApellido"),
                FechaNacimiento = ConexionHelper.LeerFechaNullable(reader, "FechaNacimiento"),
                Estado = ConexionHelper.LeerValor<string>(reader, "Estado")
            };
        }
    }
}
