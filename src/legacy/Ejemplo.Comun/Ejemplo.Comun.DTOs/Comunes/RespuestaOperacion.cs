using System;
using System.Runtime.Serialization;

namespace Ejemplo.Comun.DTOs.Comunes
{
    [Serializable]
    [DataContract]
    public class RespuestaOperacion<T>
    {
        [DataMember]
        public bool Exitosa { get; set; }
        [DataMember]
        public string Mensaje { get; set; }
        [DataMember]
        public string CodigoError { get; set; }
        [DataMember]
        public T Datos { get; set; }
        [DataMember]
        public DateTime FechaRespuesta { get; set; } = DateTime.Now;

        public static RespuestaOperacion<T> CrearExitosa(T datos, string mensaje = "Operación exitosa")
        {
            return new RespuestaOperacion<T> { Exitosa = true, Mensaje = mensaje, Datos = datos };
        }

        public static RespuestaOperacion<T> CrearError(string mensaje, string codigoError = null)
        {
            return new RespuestaOperacion<T> { Exitosa = false, Mensaje = mensaje, CodigoError = codigoError };
        }
    }
}
