using BankLab.Modern.API.Validators;
using FluentAssertions;
using Xunit;

namespace BankLab.Modern.Tests.Validators;

public class PagoValidatorTests
{
    private readonly PagoValidator _validator;

    public PagoValidatorTests()
    {
        _validator = new PagoValidator();
    }

    [Fact]
    public void Validate_NegativeAmount_ReturnsError()
    {
        // Act
        var result = _validator.Validate(-50, out var error);

        // Assert
        result.Should().BeFalse();
        error.Should().Be("El monto no puede ser negativo.");
    }

    [Fact]
    public void Validate_ZeroAmount_ReturnsError()
    {
        // Act
        var result = _validator.Validate(0, out var error);

        // Assert
        result.Should().BeFalse();
        error.Should().Be("El monto no puede ser cero.");
    }

    [Fact]
    public void Validate_ValidAmount_ReturnsSuccess()
    {
        // Act
        var result = _validator.Validate(100, out var error);

        // Assert
        result.Should().BeTrue();
        error.Should().BeEmpty();
    }
}
