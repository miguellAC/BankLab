using System;
using System.Runtime.Serialization;

namespace Ejemplo.Comun.DTOs.Clientes
{
    [Serializable]
    [DataContract]
    public class DireccionDTO
    {
        [DataMember]
        public int DireccionId { get; set; }
        [DataMember]
        public int ClienteId { get; set; }
        [DataMember]
        public string TipoDireccion { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string Ciudad { get; set; }
        [DataMember]
        public string Departamento { get; set; }
        [DataMember]
        public string CodigoPostal { get; set; }
        [DataMember]
        public string Pais { get; set; }
        [DataMember]
        public bool EsPrincipal { get; set; }
        [DataMember]
        public bool Activa { get; set; }
    }
}
