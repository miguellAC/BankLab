using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ejemplo.Componentes.BLL;
using Ejemplo.Comun.DTOs.Clientes;

namespace Ejemplo.Tests.BLL
{
    [TestClass]
    public class ClientesBLLTests
    {
        private ClientesBLL _bll;

        [TestInitialize]
        public void Setup()
        {
            _bll = new ClientesBLL();
        }

        [TestMethod]
        public void ConsultarCliente_ConIdentificacionInvalida_RetornaError()
        {
            var resultado = _bll.ConsultarCliente(1, "");
            Assert.IsFalse(resultado.Exitosa);
            Assert.IsTrue(resultado.Mensaje.Contains("obligatorio") || resultado.Mensaje.Contains("inválid"));
        }

        [TestMethod]
        public void ConsultarCliente_ConIdNegativo_RetornaError()
        {
            var resultado = _bll.ConsultarClientePorId(-10);
            Assert.IsFalse(resultado.Exitosa);
        }

        [TestMethod]
        public void RegistrarCliente_SinNombre_RetornaError()
        {
            var cli = new ClienteDTO
            {
                TipoIdentificacionId = 1,
                NumeroIdentificacion = "1020304050",
                PrimerNombre = "", // Vacío
                PrimerApellido = "Pérez"
            };

            var resultado = _bll.RegistrarCliente(cli, null, null, "TEST_USER");
            Assert.IsFalse(resultado.Exitosa);
        }
    }
}
