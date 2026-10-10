using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ejemplo.Comun.DTOs.Creditos
{
    [Serializable]
    [DataContract]
    public class OperacionCreditoDTO
    {
        [DataMember]
        public int OperacionId { get; set; }
        [DataMember]
        public string NumeroOperacion { get; set; }
        [DataMember]
        public int? SolicitudId { get; set; }
        [DataMember]
        public int ClienteId { get; set; }
        [DataMember]
        public string NombreCliente { get; set; }
        [DataMember]
        public int ProductoCreditoId { get; set; }
        [DataMember]
        public string NombreProductoCredito { get; set; }
        [DataMember]
        public decimal MontoDesembolsado { get; set; }
        [DataMember]
        public decimal TasaAnual { get; set; }
        [DataMember]
        public int PlazoMeses { get; set; }
        [DataMember]
        public int NumeroCuotas { get; set; }
        [DataMember]
        public DateTime FechaDesembolso { get; set; }
        [DataMember]
        public DateTime FechaVencimiento { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public int SucursalId { get; set; }
        [DataMember]
        public string NombreSucursal { get; set; }
        [DataMember]
        public SaldoOperacionDTO SaldoOperacion { get; set; }
        [DataMember]
        public List<CuotaDTO> Cuotas { get; set; }
    }
}
