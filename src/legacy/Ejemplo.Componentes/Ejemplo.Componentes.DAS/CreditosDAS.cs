using System;
using System.Collections.Generic;
using Ejemplo.Componentes.DAL;
using Ejemplo.Comun.DTOs.Creditos;

namespace Ejemplo.Componentes.DAS
{
    public class CreditosDAS
    {
        private readonly CreditosDAL _creditosDAL = new CreditosDAL();

        public OperacionCreditoDTO ConsultarOperacionCredito(int? opId, string numOp)
        {
            return _creditosDAL.ConsultarOperacionCredito(opId, numOp);
        }

        public SaldoOperacionDTO ConsultarSaldosVigenteVencido(int opId)
        {
            return _creditosDAL.ConsultarSaldosVigenteVencido(opId);
        }

        public List<CuotaDTO> ConsultarCuotasOperacion(int opId, bool soloVencidas, bool soloPendientes)
        {
            return _creditosDAL.ConsultarCuotasOperacion(opId, soloVencidas, soloPendientes);
        }

        public List<CuotaDTO> ConsultarCuotasPendientesVencidas(int? clienteId)
        {
            return _creditosDAL.ConsultarCuotasPendientesVencidas(clienteId);
        }
    }
}
