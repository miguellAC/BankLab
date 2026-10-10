using System;
using System.Runtime.Serialization;

namespace Ejemplo.Comun.DTOs.Cartera
{
    [Serializable]
    [DataContract]
    public class ReporteCarteraDTO
    {
        [DataMember]
        public int OperacionId { get; set; }
        [DataMember]
        public string NumeroOperacion { get; set; }
        [DataMember]
        public int ClienteId { get; set; }
        [DataMember]
        public string NombreCliente { get; set; }
        [DataMember]
        public string NumeroIdentificacion { get; set; }
        [DataMember]
        public string NombreProducto { get; set; }
        [DataMember]
        public string NombreSucursal { get; set; }
        [DataMember]
        public decimal MontoDesembolsado { get; set; }
        [DataMember]
        public decimal SaldoCapitalVigente { get; set; }
        [DataMember]
        public decimal SaldoCapitalVencido { get; set; }
        [DataMember]
        public decimal SaldoTotal { get; set; }
        [DataMember]
        public int DiasEnMora { get; set; }
        [DataMember]
        public string CategoriaRiesgo { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public DateTime FechaDesembolso { get; set; }
        [DataMember]
        public DateTime FechaVencimiento { get; set; }
    }
}
