using System;
using System.Runtime.Serialization;

namespace Ejemplo.Comun.DTOs.Clientes
{
    [Serializable]
    [DataContract]
    public class ContactoDTO
    {
        [DataMember]
        public int ContactoId { get; set; }
        [DataMember]
        public int ClienteId { get; set; }
        [DataMember]
        public string TipoContacto { get; set; }
        [DataMember]
        public string Valor { get; set; }
        [DataMember]
        public bool EsPrincipal { get; set; }
        [DataMember]
        public bool Activo { get; set; }
    }
}
