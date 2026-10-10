using System;
using System.Collections.Generic;
using Ejemplo.Componentes.DAL;
using Ejemplo.Comun.DTOs.Cartera;

namespace Ejemplo.Componentes.DAS
{
    public class CarteraDAS
    {
        private readonly CarteraDAL _carteraDAL = new CarteraDAL();

        public List<ReporteCarteraDTO> ObtenerReporteCartera(DateTime? fechaCorte, int? sucursalId, string estado, string catRiesgo)
        {
            return _carteraDAL.ObtenerReporteCartera(fechaCorte, sucursalId, estado, catRiesgo);
        }

        public List<ReporteCarteraDTO> ObtenerReporteCarteraVencida(int diasMinimo)
        {
            return _carteraDAL.ObtenerReporteCarteraVencida(diasMinimo);
        }
    }
}
