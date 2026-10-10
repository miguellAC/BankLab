using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ejemplo.Comun.DTOs.Comunes
{
    [Serializable]
    [DataContract]
    public class PaginacionDTO<T>
    {
        [DataMember]
        public int PageNumber { get; set; }
        [DataMember]
        public int PageSize { get; set; }
        [DataMember]
        public int TotalRecords { get; set; }
        [DataMember]
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / (PageSize > 0 ? PageSize : 1));
        [DataMember]
        public List<T> Items { get; set; }
    }
}
