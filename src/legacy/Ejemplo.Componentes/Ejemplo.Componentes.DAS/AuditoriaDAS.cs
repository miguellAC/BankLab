using System;
using System.Data;
using Ejemplo.Componentes.DAL;

namespace Ejemplo.Componentes.DAS
{
    public class AuditoriaDAS
    {
        private readonly AuditoriaDAL _auditoriaDAL = new AuditoriaDAL();

        public void RegistrarAuditoria(string tipoEntidad, int entidadId, string accion, string valorAnt, string valorNuevo, string ip, string equipo, string usuario)
        {
            _auditoriaDAL.RegistrarAuditoria(tipoEntidad, entidadId, accion, valorAnt, valorNuevo, ip, equipo, usuario);
        }

        public DataTable ConsultarAuditoriaEntidad(string tipoEntidad, int entidadId, DateTime? desde, DateTime? hasta)
        {
            return _auditoriaDAL.ConsultarAuditoriaEntidad(tipoEntidad, entidadId, desde, hasta);
        }
    }
}
