using System;
using System.Runtime.Serialization;

namespace Ejemplo.Comun.DTOs.Cuentas
{
    [Serializable]
    [DataContract]
    public class SaldoDTO
    {
        [DataMember]
        public int CuentaId { get; set; }
        [DataMember]
        public string NumeroCuenta { get; set; }
        [DataMember]
        public decimal Saldo { get; set; }
        [DataMember]
        public decimal SaldoDisponible { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public DateTime? FechaUltimoMovimiento { get; set; }
    }
}
