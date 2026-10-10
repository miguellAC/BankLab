using System;
using System.Collections.Generic;
using Ejemplo.Componentes.DAT;
using Ejemplo.Comun.DTOs.Creditos;

namespace Ejemplo.Componentes.DAL
{
    public class CreditosDAL
    {
        private readonly CreditosDAT _creditosDAT = new CreditosDAT();

        public OperacionCreditoDTO ConsultarOperacionCredito(int? opId, string numOp)
        {
            var operacion = _creditosDAT.ConsultarOperacionCredito(opId, numOp);
            // Business rule: Validates operation exists
            if (operacion == null)
            {
                throw new Exception("La operación de crédito no existe o no fue encontrada.");
            }
            return operacion;
        }

        public SaldoOperacionDTO ConsultarSaldosVigenteVencido(int opId)
        {
            return _creditosDAT.ConsultarSaldosVigenteVencido(opId);
        }

        public List<CuotaDTO> ConsultarCuotasOperacion(int opId, bool soloVencidas, bool soloPendientes)
        {
            return _creditosDAT.ConsultarCuotasOperacion(opId, soloVencidas, soloPendientes);
        }

        public List<CuotaDTO> ConsultarCuotasPendientesVencidas(int? clienteId)
        {
            return _creditosDAT.ConsultarCuotasPendientesVencidas(clienteId);
        }
    }
}
