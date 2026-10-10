using BankLab.Modern.API.Data.Repositories;
using BankLab.Modern.API.DTOs;
using BankLab.Modern.API.Services;
using BankLab.Modern.API.Configuration;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace BankLab.Modern.Tests.Services;

public class ClientesServiceTests
{
    private readonly Mock<IClienteRepository> _repositoryMock;
    private readonly Mock<IOptions<BankLabOptions>> _optionsMock;
    private readonly ClientesService _service;

    public ClientesServiceTests()
    {
        _repositoryMock = new Mock<IClienteRepository>();
        _optionsMock = new Mock<IOptions<BankLabOptions>>();
        _optionsMock.Setup(x => x.Value).Returns(new BankLabOptions { DefaultPageSize = 20, MaxPageSize = 100 });
        _service = new ClientesService(_repositoryMock.Object, _optionsMock.Object);
    }

    [Fact]
    public async Task SearchAsync_WithValidParams_ReturnsResults()
    {
        // Arrange
        var expectedResponse = new PaginatedResponse<ClienteResponse> { TotalRecords = 1, Items = new[] { new ClienteResponse() } };
        _repositoryMock.Setup(r => r.SearchAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>(), 1, 20))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.SearchAsync(null, null, null, null, null, 1, 20);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((ClienteResponse?)null);

        // Act
        var result = await _service.GetByIdAsync(999);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsCliente()
    {
        // Arrange
        var expectedCliente = new ClienteResponse { ClienteId = 1 };
        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(expectedCliente);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        result.Should().BeEquivalentTo(expectedCliente);
    }

    [Fact]
    public async Task SearchAsync_PageSizeExceedsMax_ClampedToMax()
    {
        // Arrange
        _repositoryMock.Setup(r => r.SearchAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>(), 1, 100))
            .ReturnsAsync(new PaginatedResponse<ClienteResponse> { PageSize = 100 });

        // Act
        var result = await _service.SearchAsync(null, null, null, null, null, 1, 500);

        // Assert
        _repositoryMock.Verify(r => r.SearchAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>(), 1, 100), Times.Once);
        result.PageSize.Should().Be(100);
    }
}
