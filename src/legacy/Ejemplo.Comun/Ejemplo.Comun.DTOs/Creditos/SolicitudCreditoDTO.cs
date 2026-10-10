using System;
using System.Runtime.Serialization;

namespace Ejemplo.Comun.DTOs.Creditos
{
    [Serializable]
    [DataContract]
    public class SolicitudCreditoDTO
    {
        [DataMember]
        public int SolicitudId { get; set; }
        [DataMember]
        public int ClienteId { get; set; }
        [DataMember]
        public string NombreCliente { get; set; }
        [DataMember]
        public int ProductoCreditoId { get; set; }
        [DataMember]
        public string NombreProductoCredito { get; set; }
        [DataMember]
        public int EmpleadoId { get; set; }
        [DataMember]
        public string NombreAsesor { get; set; }
        [DataMember]
        public decimal MontoSolicitado { get; set; }
        [DataMember]
        public int PlazoMeses { get; set; }
        [DataMember]
        public decimal IngresosMensuales { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public DateTime FechaRadicacion { get; set; }
        [DataMember]
        public DateTime FechaResolucion { get; set; }
        [DataMember]
        public string ObservacionesResolucion { get; set; }
    }
}
