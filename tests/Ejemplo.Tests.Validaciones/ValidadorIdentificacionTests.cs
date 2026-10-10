using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ejemplo.Comun.Validaciones;

namespace Ejemplo.Tests.Validaciones
{
    [TestClass]
    public class ValidadorIdentificacionTests
    {
        [TestMethod]
        public void ValidarCC_CedulaValida_RetornaExito()
        {
            var res = ValidadorIdentificacion.Validar("CC", "1020304050");
            Assert.IsTrue(res.EsValido);
        }

        [TestMethod]
        public void ValidarCC_Letras_RetornaError()
        {
            var res = ValidadorIdentificacion.Validar("CC", "10203ABC50");
            Assert.IsFalse(res.EsValido);
        }

        [TestMethod]
        public void ValidarNIT_SinDigitoVerificacion_RetornaError()
        {
            var res = ValidadorIdentificacion.Validar("NIT", "");
            Assert.IsFalse(res.EsValido);
        }
    }
}
