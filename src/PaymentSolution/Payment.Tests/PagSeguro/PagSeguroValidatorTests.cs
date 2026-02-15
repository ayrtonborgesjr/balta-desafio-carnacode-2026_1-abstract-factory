using Payment.Gateways.PagSeguro;

namespace Payment.Tests.PagSeguro;

public class PagSeguroValidatorTests
{
    private readonly PagSeguroValidator _validator;

    public PagSeguroValidatorTests()
    {
        _validator = new PagSeguroValidator();
    }

    [Fact]
    public void Validate_WithValidCardNumber_ReturnsTrue()
    {
        // Arrange
        var validCardNumber = "1234567890123456"; // 16 digits

        // Act
        var result = _validator.Validate(validCardNumber);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Validate_WithNullCardNumber_ReturnsFalse()
    {
        // Arrange
        string? nullCardNumber = null;

        // Act
        var result = _validator.Validate(nullCardNumber!);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Validate_WithEmptyCardNumber_ReturnsFalse()
    {
        // Arrange
        var emptyCardNumber = string.Empty;

        // Act
        var result = _validator.Validate(emptyCardNumber);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Validate_WithWhitespaceCardNumber_ReturnsFalse()
    {
        // Arrange
        var whitespaceCardNumber = "   ";

        // Act
        var result = _validator.Validate(whitespaceCardNumber);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("123")]
    [InlineData("12345678901234")]
    [InlineData("12345")]
    public void Validate_WithCardNumberLessThan16Digits_ReturnsFalse(string cardNumber)
    {
        // Act
        var result = _validator.Validate(cardNumber);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("12345678901234567")]
    [InlineData("123456789012345678901")]
    public void Validate_WithCardNumberMoreThan16Digits_ReturnsFalse(string cardNumber)
    {
        // Act
        var result = _validator.Validate(cardNumber);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Validate_WithExactly16Digits_ReturnsTrue()
    {
        // Arrange
        var cardNumber = "4532015112830366"; // Valid 16-digit card

        // Act
        var result = _validator.Validate(cardNumber);

        // Assert
        Assert.True(result);
    }
}

