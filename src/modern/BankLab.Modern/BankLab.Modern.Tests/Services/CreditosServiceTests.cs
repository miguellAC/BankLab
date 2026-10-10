using BankLab.Modern.API.Data.Repositories;
using BankLab.Modern.API.DTOs;
using BankLab.Modern.API.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace BankLab.Modern.Tests.Services;

public class CreditosServiceTests
{
    private readonly Mock<ICreditoRepository> _repositoryMock;
    private readonly CreditosService _service;

    public CreditosServiceTests()
    {
        _repositoryMock = new Mock<ICreditoRepository>();
        _service = new CreditosService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetOperacionAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetOperacionAsync(It.IsAny<int>(), null)).ReturnsAsync((CreditoResponse?)null);

        // Act
        var result = await _service.GetOperacionAsync(999, null);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetSaldosAsync_ReturnsCorrectData()
    {
        // Arrange
        var expectedData = new { OperacionId = 1, TotalPagar = 1050m };
        _repositoryMock.Setup(r => r.GetSaldosAsync(1)).ReturnsAsync(expectedData);

        // Act
        var result = await _service.GetSaldosAsync(1);

        // Assert
        result.Should().BeEquivalentTo(expectedData);
    }
}
