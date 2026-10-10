using System;
using System.Runtime.Serialization;

namespace Ejemplo.Comun.DTOs.Integracion
{
    [Serializable]
    [DataContract]
    public class LoteImportacionDTO
    {
        [DataMember]
        public int LoteId { get; set; }
        [DataMember]
        public string NombreArchivo { get; set; }
        [DataMember]
        public string TipoImportacion { get; set; }
        [DataMember]
        public int TotalRegistros { get; set; }
        [DataMember]
        public int RegistrosProcesados { get; set; }
        [DataMember]
        public int RegistrosExitosos { get; set; }
        [DataMember]
        public int RegistrosConError { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public DateTime FechaRecepcion { get; set; }
        [DataMember]
        public DateTime? FechaProcesamiento { get; set; }
        [DataMember]
        public string Observaciones { get; set; }
    }
}
