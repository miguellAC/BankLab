using System;
using System.Runtime.Serialization;

namespace Ejemplo.Comun.DTOs.Cuentas
{
    [Serializable]
    [DataContract]
    public class MovimientoDTO
    {
        [DataMember]
        public long MovimientoId { get; set; }
        [DataMember]
        public int CuentaId { get; set; }
        [DataMember]
        public int TipoTransaccionId { get; set; }
        [DataMember]
        public string NombreTipoTransaccion { get; set; }
        [DataMember]
        public char Naturaleza { get; set; }
        [DataMember]
        public decimal Monto { get; set; }
        [DataMember]
        public decimal SaldoAnterior { get; set; }
        [DataMember]
        public decimal SaldoPosterior { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Referencia { get; set; }
        [DataMember]
        public string CanalOrigen { get; set; }
        [DataMember]
        public DateTime FechaMovimiento { get; set; }
        [DataMember]
        public DateTime FechaValor { get; set; }
    }
}
