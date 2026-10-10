using System;
using System.Collections.Generic;
using Ejemplo.Componentes.DAL;
using Ejemplo.Comun.DTOs.Creditos;

namespace Ejemplo.Componentes.DAS
{
    public class PagosDAS
    {
        private readonly PagosDAL _pagosDAL = new PagosDAL();
        private readonly AuditoriaDAL _auditoriaDAL = new AuditoriaDAL();

        public long RegistrarPago(int opId, decimal monto, string tipoPago, string canalPago, int? cuentaOrigenId, string referencia, string usuario)
        {
            return TransaccionHelper.EjecutarEnTransaccion((conn, tx) =>
            {
                long pagoId = _pagosDAL.RegistrarPago(conn, tx, opId, monto, tipoPago, canalPago, cuentaOrigenId, referencia, usuario);
                
                _auditoriaDAL.RegistrarAuditoria(conn, tx, "PAGO", (int)pagoId, "REGISTRAR", "", monto.ToString(), "127.0.0.1", "APP", usuario);
                
                return pagoId;
            });
        }

        public List<PagoDTO> ConsultarPagosOperacion(int opId, DateTime? desde, DateTime? hasta)
        {
            return _pagosDAL.ConsultarPagosOperacion(opId, desde, hasta);
        }
    }
}
