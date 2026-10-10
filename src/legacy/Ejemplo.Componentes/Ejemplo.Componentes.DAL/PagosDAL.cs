using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Ejemplo.Componentes.DAT;
using Ejemplo.Comun.DTOs.Creditos;

namespace Ejemplo.Componentes.DAL
{
    public class PagosDAL
    {
        private readonly PagosDAT _pagosDAT = new PagosDAT();
        private readonly CreditosDAT _creditosDAT = new CreditosDAT();

        public long RegistrarPago(SqlConnection conn, SqlTransaction tx, int opId, decimal monto, string tipoPago, string canalPago, int? cuentaOrigenId, string referencia, string usuario)
        {
            // Business rule: Validates operation state before payment
            var operacion = _creditosDAT.ConsultarOperacionCredito(opId, null);
            if (operacion == null)
            {
                throw new Exception("Operación no encontrada para realizar el pago.");
            }
            if (operacion.Estado != "ACT" && operacion.Estado != "VEN")
            {
                throw new Exception($"No se puede registrar pagos a una operación en estado {operacion.Estado}.");
            }

            return _pagosDAT.RegistrarPago(conn, tx, opId, monto, tipoPago, canalPago, cuentaOrigenId, referencia, usuario);
        }

        public List<PagoDTO> ConsultarPagosOperacion(int opId, DateTime? desde, DateTime? hasta)
        {
            return _pagosDAT.ConsultarPagosOperacion(opId, desde, hasta);
        }
    }
}
