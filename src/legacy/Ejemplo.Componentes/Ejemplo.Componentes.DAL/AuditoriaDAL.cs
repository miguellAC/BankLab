using System;
using System.Data;
using System.Data.SqlClient;
using Ejemplo.Componentes.DAT;

namespace Ejemplo.Componentes.DAL
{
    public class AuditoriaDAL
    {
        private readonly AuditoriaDAT _auditoriaDAT = new AuditoriaDAT();

        public void RegistrarAuditoria(SqlConnection conn, SqlTransaction tx, string tipoEntidad, int entidadId, string accion, string valorAnt, string valorNuevo, string ip, string equipo, string usuario)
        {
            // Pass-through with formatting (e.g. standardizing nulls)
            valorAnt = string.IsNullOrWhiteSpace(valorAnt) ? "N/A" : valorAnt;
            valorNuevo = string.IsNullOrWhiteSpace(valorNuevo) ? "N/A" : valorNuevo;
            ip = string.IsNullOrWhiteSpace(ip) ? "0.0.0.0" : ip;
            equipo = string.IsNullOrWhiteSpace(equipo) ? "DESCONOCIDO" : equipo;

            _auditoriaDAT.RegistrarAuditoria(conn, tx, tipoEntidad, entidadId, accion, valorAnt, valorNuevo, ip, equipo, usuario);
        }

        public void RegistrarAuditoria(string tipoEntidad, int entidadId, string accion, string valorAnt, string valorNuevo, string ip, string equipo, string usuario)
        {
            valorAnt = string.IsNullOrWhiteSpace(valorAnt) ? "N/A" : valorAnt;
            valorNuevo = string.IsNullOrWhiteSpace(valorNuevo) ? "N/A" : valorNuevo;
            ip = string.IsNullOrWhiteSpace(ip) ? "0.0.0.0" : ip;
            equipo = string.IsNullOrWhiteSpace(equipo) ? "DESCONOCIDO" : equipo;

            _auditoriaDAT.RegistrarAuditoria(tipoEntidad, entidadId, accion, valorAnt, valorNuevo, ip, equipo, usuario);
        }

        public DataTable ConsultarAuditoriaEntidad(string tipoEntidad, int entidadId, DateTime? desde, DateTime? hasta)
        {
            return _auditoriaDAT.ConsultarAuditoriaEntidad(tipoEntidad, entidadId, desde, hasta);
        }
    }
}
