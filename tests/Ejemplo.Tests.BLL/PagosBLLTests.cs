using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ejemplo.Componentes.BLL;

namespace Ejemplo.Tests.BLL
{
    [TestClass]
    public class PagosBLLTests
    {
        private PagosBLL _bll;

        [TestInitialize]
        public void Setup()
        {
            _bll = new PagosBLL();
        }

        [TestMethod]
        public void RegistrarPago_MontoNegativo_RetornaError()
        {
            var resultado = _bll.RegistrarPago(1, -50000m, "ORDINARIO", "CAJA", null, "REF-123", "TEST");
            Assert.IsFalse(resultado.Exitosa);
        }

        [TestMethod]
        public void RegistrarPago_MontoZero_RetornaError()
        {
            var resultado = _bll.RegistrarPago(1, 0m, "ORDINARIO", "CAJA", null, "REF-123", "TEST");
            Assert.IsFalse(resultado.Exitosa);
        }

        [TestMethod]
        public void RegistrarPago_OperacionIdInvalido_RetornaError()
        {
            var resultado = _bll.RegistrarPago(0, 100000m, "ORDINARIO", "CAJA", null, "REF-123", "TEST");
            Assert.IsFalse(resultado.Exitosa);
        }
    }
}
