using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ejemplo.Componentes.BLL;

namespace Ejemplo.Tests.BLL
{
    [TestClass]
    public class CreditosBLLTests
    {
        private CreditosBLL _bll;

        [TestInitialize]
        public void Setup()
        {
            _bll = new CreditosBLL();
        }

        [TestMethod]
        public void ConsultarOperacion_SinParametros_RetornaError()
        {
            var resultado = _bll.ConsultarOperacion(null, "");
            Assert.IsFalse(resultado.Exitosa);
        }

        [TestMethod]
        public void ConsultarCuotas_OperacionIdInvalido_RetornaError()
        {
            var resultado = _bll.ConsultarCuotas(0, false, false);
            Assert.IsFalse(resultado.Exitosa);
        }
    }
}
