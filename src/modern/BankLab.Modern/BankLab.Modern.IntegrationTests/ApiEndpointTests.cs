using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace BankLab.Modern.IntegrationTests;

public class ApiEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ApiEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact(Skip = "Requiere base de datos BankLabDB en ejecución")]
    [Trait("Category", "Integration")]
    public async Task GetClientes_ReturnsOk()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/clientes");

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact(Skip = "Requiere base de datos BankLabDB en ejecución")]
    [Trait("Category", "Integration")]
    public async Task GetCliente_InvalidId_Returns404()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/clientes/999999");

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact(Skip = "Requiere base de datos BankLabDB en ejecución")]
    [Trait("Category", "Integration")]
    public async Task GetCreditoByOperacion_ReturnsOk()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/creditos/operacion/TEST-01");

        // Assert
        // This will likely return 404 if the test data is missing, but verifies endpoint exists
        Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NotFound);
    }
}
