using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ejemplo.Comun.DTOs.Creditos
{
    [Serializable]
    [DataContract]
    public class PagoDTO
    {
        [DataMember]
        public long PagoId { get; set; }
        [DataMember]
        public int OperacionId { get; set; }
        [DataMember]
        public int? CuentaOrigenId { get; set; }
        [DataMember]
        public decimal MontoPago { get; set; }
        [DataMember]
        public DateTime FechaPago { get; set; }
        [DataMember]
        public DateTime? FechaAplicacion { get; set; }
        [DataMember]
        public string TipoPago { get; set; }
        [DataMember]
        public string CanalPago { get; set; }
        [DataMember]
        public string Referencia { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public List<AplicacionPagoDTO> Detalle { get; set; }
    }
}
