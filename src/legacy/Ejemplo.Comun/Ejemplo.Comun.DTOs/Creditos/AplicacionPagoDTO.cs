using System;
using System.Runtime.Serialization;

namespace Ejemplo.Comun.DTOs.Creditos
{
    [Serializable]
    [DataContract]
    public class AplicacionPagoDTO
    {
        [DataMember]
        public long AplicacionPagoId { get; set; }
        [DataMember]
        public long PagoId { get; set; }
        [DataMember]
        public long CuotaId { get; set; }
        [DataMember]
        public int NumeroCuota { get; set; }
        [DataMember]
        public decimal MontoCapital { get; set; }
        [DataMember]
        public decimal MontoInteres { get; set; }
        [DataMember]
        public decimal MontoSeguro { get; set; }
        [DataMember]
        public decimal MontoTotal { get; set; }
    }
}
