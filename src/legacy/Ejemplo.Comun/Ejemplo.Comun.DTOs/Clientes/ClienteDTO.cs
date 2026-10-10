using System;
using System.Runtime.Serialization;
using Ejemplo.Comun.Enumeraciones;

namespace Ejemplo.Comun.DTOs.Clientes
{
    [Serializable]
    [DataContract]
    public class ClienteDTO
    {
        [DataMember]
        public int ClienteId { get; set; }
        [DataMember]
        public int TipoIdentificacionId { get; set; }
        [DataMember]
        public string CodigoTipoIdentificacion { get; set; }
        [DataMember]
        public string NombreTipoIdentificacion { get; set; }
        [DataMember]
        public string NumeroIdentificacion { get; set; }
        [DataMember]
        public string PrimerNombre { get; set; }
        [DataMember]
        public string SegundoNombre { get; set; }
        [DataMember]
        public string PrimerApellido { get; set; }
        [DataMember]
        public string SegundoApellido { get; set; }
        [DataMember]
        public string NombreCompleto => $"{PrimerNombre} {SegundoNombre} {PrimerApellido} {SegundoApellido}".Replace("  ", " ").Trim();
        [DataMember]
        public DateTime? FechaNacimiento { get; set; }
        [DataMember]
        public string Genero { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public int SucursalId { get; set; }
        [DataMember]
        public string NombreSucursal { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public DateTime FechaRegistro { get; set; }
    }
}
