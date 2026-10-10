using System;
using System.Runtime.Serialization;

namespace Ejemplo.Comun.DTOs.Cartera
{
    [Serializable]
    [DataContract]
    public class ClasificacionRiesgoDTO
    {
        [DataMember]
        public int ClasificacionId { get; set; }
        [DataMember]
        public int ClienteId { get; set; }
        [DataMember]
        public string NombreCliente { get; set; }
        [DataMember]
        public string Categoria { get; set; }
        [DataMember]
        public int Puntaje { get; set; }
        [DataMember]
        public DateTime FechaClasificacion { get; set; }
        [DataMember]
        public bool Vigente { get; set; }
        [DataMember]
        public string Observaciones { get; set; }
    }
}
