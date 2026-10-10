using System;
using System.Collections.Generic;
using Ejemplo.Componentes.DAT;
using Ejemplo.Comun.DTOs.Cartera;

namespace Ejemplo.Componentes.DAL
{
    public class CarteraDAL
    {
        private readonly CarteraDAT _carteraDAT = new CarteraDAT();

        public List<ReporteCarteraDTO> ObtenerReporteCartera(DateTime? fechaCorte, int? sucursalId, string estado, string catRiesgo)
        {
            // Business rule: Applies default date if null
            if (!fechaCorte.HasValue)
            {
                fechaCorte = DateTime.Today;
            }
            return _carteraDAT.ObtenerReporteCartera(fechaCorte, sucursalId, estado, catRiesgo);
        }

        public List<ReporteCarteraDTO> ObtenerReporteCarteraVencida(int diasMinimo)
        {
            return _carteraDAT.ObtenerReporteCarteraVencida(diasMinimo);
        }
    }
}
