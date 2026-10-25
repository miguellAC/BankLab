using System;
using System.Data;
using Ejemplo.Componentes.DAS;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.Componentes.BLL
{
    public class AuditoriaBLL
    {
        private readonly AuditoriaDAS _auditoriaDAS = new AuditoriaDAS();

        public RespuestaOperacion<bool> RegistrarAuditoria(string tipoEntidad, int entidadId, string accion, string valorAnt, string valorNuevo, string ip, string equipo, string usuario)
        {
            try
            {
                _auditoriaDAS.RegistrarAuditoria(tipoEntidad, entidadId, accion, valorAnt, valorNuevo, ip, equipo, usuario);
                return RespuestaOperacion<bool>.CrearExitosa(true);
            }
            catch (Exception ex)
            {
                LogHelper.Error("RegistrarAuditoria BLL", ex);
                return RespuestaOperacion<bool>.CrearError("Error al registrar auditoria.");
            }
        }

        public RespuestaOperacion<DataTable> ConsultarAuditoriaEntidad(string tipoEntidad, int entidadId, DateTime? desde, DateTime? hasta)
        {
            try
            {
                var dt = _auditoriaDAS.ConsultarAuditoriaEntidad(tipoEntidad, entidadId, desde, hasta);
                return RespuestaOperacion<DataTable>.CrearExitosa(dt);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ConsultarAuditoriaEntidad BLL", ex);
                return RespuestaOperacion<DataTable>.CrearError("Error interno.");
            }
        }
    }
}
