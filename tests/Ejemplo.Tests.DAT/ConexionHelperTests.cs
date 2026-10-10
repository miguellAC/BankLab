using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ejemplo.Componentes.DAT;

namespace Ejemplo.Tests.DAT
{
    [TestClass]
    public class ConexionHelperTests
    {
        [TestMethod]
        public void CrearComandoSP_RetornaComandoConfigurado()
        {
            using (var conn = new System.Data.SqlClient.SqlConnection("Server=localhost;Database=BankLabDB;Integrated Security=True;"))
            {
                var cmd = ConexionHelper.CrearComandoSP(conn, "sp_ConsultarClientePorId");
                Assert.AreEqual(System.Data.CommandType.StoredProcedure, cmd.CommandType);
                Assert.AreEqual("sp_ConsultarClientePorId", cmd.CommandText);
                Assert.IsTrue(cmd.CommandTimeout > 0);
            }
        }
    }
}
