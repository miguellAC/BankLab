using System.Collections.Generic;
using System.Linq;

namespace Ejemplo.Comun.Validaciones
{
    public class ResultadoValidacion
    {
        public bool EsValido => !Errores.Any();
        public List<string> Errores { get; } = new List<string>();

        public void AgregarError(string error)
        {
            if (!string.IsNullOrWhiteSpace(error))
            {
                Errores.Add(error);
            }
        }

        public static ResultadoValidacion Valido()
        {
            return new ResultadoValidacion();
        }

        public static ResultadoValidacion Invalido(string error)
        {
            var resultado = new ResultadoValidacion();
            resultado.AgregarError(error);
            return resultado;
        }
    }
}
