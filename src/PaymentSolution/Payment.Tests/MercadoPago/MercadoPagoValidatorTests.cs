using Payment.Gateways.MercadoPago;

namespace Payment.Tests.MercadoPago;

public class MercadoPagoValidatorTests
{
    private readonly MercadoPagoValidator _validator;

    public MercadoPagoValidatorTests()
    {
        _validator = new MercadoPagoValidator();
    }

    [Fact]
    public void Validate_WithValid16DigitCardNumber_ReturnsTrue()
    {
        // Arrange
        var validCardNumber = "1234567890123456"; // 16 digits

        // Act
        var result = _validator.Validate(validCardNumber);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Validate_WithValid15DigitCardNumber_ReturnsTrue()
    {
        // Arrange
        var validCardNumber = "123456789012345"; // 15 digits (American Express)

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
    [InlineData("1234567890123")]
    [InlineData("12345678901234")]
    public void Validate_WithCardNumberLessThan15Digits_ReturnsFalse(string cardNumber)
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
    public void Validate_WithAmexCard15Digits_ReturnsTrue()
    {
        // Arrange
        var amexCard = "378282246310005"; // Valid 15-digit Amex test card

        // Act
        var result = _validator.Validate(amexCard);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Validate_WithVisaCard16Digits_ReturnsTrue()
    {
        // Arrange
        var visaCard = "4532015112830366"; // Valid 16-digit Visa test card

        // Act
        var result = _validator.Validate(visaCard);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("1234567890")]
    public void Validate_WithInvalidLength_ReturnsFalse(string cardNumber)
    {
        // Act
        var result = _validator.Validate(cardNumber);

        // Assert
        Assert.False(result);
    }
}

