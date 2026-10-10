using System;
using System.Runtime.Serialization;

namespace Ejemplo.Comun.DTOs.Creditos
{
    [Serializable]
    [DataContract]
    public class CuotaDTO
    {
        [DataMember]
        public long CuotaId { get; set; }
        [DataMember]
        public int OperacionId { get; set; }
        [DataMember]
        public int NumeroCuota { get; set; }
        [DataMember]
        public DateTime FechaVencimiento { get; set; }
        [DataMember]
        public decimal CapitalCuota { get; set; }
        [DataMember]
        public decimal InteresCuota { get; set; }
        [DataMember]
        public decimal SeguroCuota { get; set; }
        [DataMember]
        public decimal ValorCuota { get; set; }
        [DataMember]
        public decimal SaldoCapital { get; set; }
        [DataMember]
        public decimal CapitalPagado { get; set; }
        [DataMember]
        public decimal InteresPagado { get; set; }
        [DataMember]
        public decimal SeguroPagado { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public DateTime? FechaPago { get; set; }
    }
}
