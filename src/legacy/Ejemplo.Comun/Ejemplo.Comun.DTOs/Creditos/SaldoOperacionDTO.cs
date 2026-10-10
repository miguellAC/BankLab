using System;
using System.Runtime.Serialization;

namespace Ejemplo.Comun.DTOs.Creditos
{
    [Serializable]
    [DataContract]
    public class SaldoOperacionDTO
    {
        [DataMember]
        public int SaldoOperacionId { get; set; }
        [DataMember]
        public int OperacionId { get; set; }
        [DataMember]
        public decimal SaldoCapitalVigente { get; set; }
        [DataMember]
        public decimal SaldoCapitalVencido { get; set; }
        [DataMember]
        public decimal SaldoInteresVigente { get; set; }
        [DataMember]
        public decimal SaldoInteresVencido { get; set; }
        [DataMember]
        public decimal SaldoSeguro { get; set; }
        [DataMember]
        public decimal SaldoTotal { get; set; }
        [DataMember]
        public int DiasEnMora { get; set; }
        [DataMember]
        public DateTime? FechaUltimoPago { get; set; }
        [DataMember]
        public DateTime FechaActualizacion { get; set; }
    }
}
