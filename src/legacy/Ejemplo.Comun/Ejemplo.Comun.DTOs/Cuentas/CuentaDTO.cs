using System;
using System.Runtime.Serialization;

namespace Ejemplo.Comun.DTOs.Cuentas
{
    [Serializable]
    [DataContract]
    public class CuentaDTO
    {
        [DataMember]
        public int CuentaId { get; set; }
        [DataMember]
        public string NumeroCuenta { get; set; }
        [DataMember]
        public int ClienteId { get; set; }
        [DataMember]
        public string NombreCliente { get; set; }
        [DataMember]
        public int ProductoId { get; set; }
        [DataMember]
        public string NombreProducto { get; set; }
        [DataMember]
        public int TipoCuentaId { get; set; }
        [DataMember]
        public string NombreTipoCuenta { get; set; }
        [DataMember]
        public int SucursalId { get; set; }
        [DataMember]
        public string NombreSucursal { get; set; }
        [DataMember]
        public decimal Saldo { get; set; }
        [DataMember]
        public decimal SaldoDisponible { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public DateTime FechaApertura { get; set; }
        [DataMember]
        public string Moneda { get; set; }
    }
}
