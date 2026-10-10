using System;
using System.Runtime.Serialization;

namespace Ejemplo.Comun.DTOs.Integracion
{
    [Serializable]
    [DataContract]
    public class MensajeIntegracionDTO
    {
        [DataMember]
        public long MensajeId { get; set; }
        [DataMember]
        public string CodigoMensaje { get; set; }
        [DataMember]
        public string SistemaOrigen { get; set; }
        [DataMember]
        public string SistemaDestino { get; set; }
        [DataMember]
        public string TipoMensaje { get; set; }
        [DataMember]
        public string Contenido { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public int Prioridad { get; set; }
        [DataMember]
        public int IntentosProc { get; set; }
        [DataMember]
        public int MaxIntentos { get; set; }
        [DataMember]
        public DateTime FechaRecepcion { get; set; }
        [DataMember]
        public DateTime? FechaProcesamiento { get; set; }
        [DataMember]
        public string MensajeError { get; set; }
    }
}
