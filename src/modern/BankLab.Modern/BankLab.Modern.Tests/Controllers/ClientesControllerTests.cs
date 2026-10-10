using BankLab.Modern.API.Controllers;
using BankLab.Modern.API.DTOs;
using BankLab.Modern.API.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BankLab.Modern.Tests.Controllers;

public class ClientesControllerTests
{
    private readonly Mock<IClientesService> _serviceMock;
    private readonly ClientesController _controller;

    public ClientesControllerTests()
    {
        _serviceMock = new Mock<IClientesService>();
        _controller = new ClientesController(_serviceMock.Object);
    }

    [Fact]
    public async Task GetById_WhenNotFound_Returns404()
    {
        // Arrange
        _serviceMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((ClienteResponse?)null);

        // Act
        var result = await _controller.GetById(999);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetById_WhenFound_Returns200()
    {
        // Arrange
        var cliente = new ClienteResponse { ClienteId = 1 };
        _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(cliente);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(cliente);
    }

    [Fact]
    public async Task Search_ReturnsOkWithPaginatedResult()
    {
        // Arrange
        var paginatedResult = new PaginatedResponse<ClienteResponse>();
        _serviceMock.Setup(s => s.SearchAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>(), 1, 20))
            .ReturnsAsync(paginatedResult);

        // Act
        var result = await _controller.Search(null, null, null, null, null, 1, 20);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(paginatedResult);
    }
}
