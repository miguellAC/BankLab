using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ejemplo.Comun.Validaciones;

namespace Ejemplo.Tests.Validaciones
{
    [TestClass]
    public class ValidadorMontoTests
    {
        [TestMethod]
        public void ValidarMontoPositivo_NumeroMayorCero_RetornaExito()
        {
            var res = ValidadorMonto.ValidarMontoPositivo(50000m);
            Assert.IsTrue(res.EsValido);
        }

        [TestMethod]
        public void ValidarMontoPositivo_Cero_RetornaError()
        {
            var res = ValidadorMonto.ValidarMontoPositivo(0m);
            Assert.IsFalse(res.EsValido);
        }

        [TestMethod]
        public void ValidarMontoEnRango_ValorEnRango_RetornaExito()
        {
            var res = ValidadorMonto.ValidarMontoEnRango(100m, 10m, 200m);
            Assert.IsTrue(res.EsValido);
        }

        [TestMethod]
        public void ValidarMontoEnRango_ValorFueraDeRango_RetornaError()
        {
            var res = ValidadorMonto.ValidarMontoEnRango(500m, 10m, 200m);
            Assert.IsFalse(res.EsValido);
        }
    }
}
