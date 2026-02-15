using Payment.Gateways.Stripe;

namespace Payment.Tests.Stripe;

public class StripeValidatorTests
{
    private readonly StripeValidator _validator;

    public StripeValidatorTests()
    {
        _validator = new StripeValidator();
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
        var validCardNumber = "123456789012345"; // 15 digits (Amex)

        // Act
        var result = _validator.Validate(validCardNumber);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Validate_WithValid13DigitCardNumber_ReturnsTrue()
    {
        // Arrange
        var validCardNumber = "1234567890123"; // 13 digits (Visa)

        // Act
        var result = _validator.Validate(validCardNumber);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Validate_WithValid19DigitCardNumber_ReturnsTrue()
    {
        // Arrange
        var validCardNumber = "1234567890123456789"; // 19 digits

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
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("123")]
    [InlineData("123456789012")] // 12 digits - too short
    public void Validate_WithCardNumberLessThan13Digits_ReturnsFalse(string cardNumber)
    {
        // Act
        var result = _validator.Validate(cardNumber);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("12345678901234567890")] // 20 digits
    [InlineData("123456789012345678901")] // 21 digits
    [InlineData("1234567890123456789012345")] // 25 digits
    public void Validate_WithCardNumberMoreThan19Digits_ReturnsFalse(string cardNumber)
    {
        // Act
        var result = _validator.Validate(cardNumber);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("1234567890123")] // 13 digits
    [InlineData("12345678901234")] // 14 digits
    [InlineData("123456789012345")] // 15 digits
    [InlineData("1234567890123456")] // 16 digits
    [InlineData("12345678901234567")] // 17 digits
    [InlineData("123456789012345678")] // 18 digits
    [InlineData("1234567890123456789")] // 19 digits
    public void Validate_WithCardNumberBetween13And19Digits_ReturnsTrue(string cardNumber)
    {
        // Act
        var result = _validator.Validate(cardNumber);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Validate_WithVisaTestCard_ReturnsTrue()
    {
        // Arrange
        var visaCard = "4242424242424242"; // Stripe test card

        // Act
        var result = _validator.Validate(visaCard);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Validate_WithMastercardTestCard_ReturnsTrue()
    {
        // Arrange
        var mastercardCard = "5555555555554444"; // Stripe test card

        // Act
        var result = _validator.Validate(mastercardCard);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Validate_WithAmexTestCard_ReturnsTrue()
    {
        // Arrange
        var amexCard = "378282246310005"; // Stripe test card (15 digits)

        // Act
        var result = _validator.Validate(amexCard);

        // Assert
        Assert.True(result);
    }
}

