using Payment.Gateways.MercadoPago;

namespace Payment.Tests.MercadoPago;

public class MercadoPagoProcessorTests
{
    private readonly MercadoPagoProcessor _processor;

    public MercadoPagoProcessorTests()
    {
        _processor = new MercadoPagoProcessor();
    }

    [Fact]
    public void Process_WithValidAmountAndCardNumber_ReturnsSuccessMessage()
    {
        // Arrange
        decimal amount = 100.00m;
        string cardNumber = "1234567890123456";

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains("processado com sucesso", result);
        Assert.Contains("MercadoPago", result);
        Assert.Contains(amount.ToString("C"), result);
    }

    [Fact]
    public void Process_WithValidAmount_IncludesFeeInResult()
    {
        // Arrange
        decimal amount = 100.00m;
        string cardNumber = "1234567890123456";
        decimal expectedFee = amount * 0.0399m; // 3.99%

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains("Taxa:", result);
        Assert.Contains(expectedFee.ToString("C"), result);
    }

    [Fact]
    public void Process_WithValidAmount_IncludesTotalInResult()
    {
        // Arrange
        decimal amount = 100.00m;
        string cardNumber = "1234567890123456";
        decimal expectedTotal = amount + (amount * 0.0399m);

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains("Total:", result);
        Assert.Contains(expectedTotal.ToString("C"), result);
    }

    [Fact]
    public void Process_WithZeroAmount_ReturnsInvalidValueMessage()
    {
        // Arrange
        decimal amount = 0m;
        string cardNumber = "1234567890123456";

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Equal("Valor inválido", result);
    }

    [Fact]
    public void Process_WithNegativeAmount_ReturnsInvalidValueMessage()
    {
        // Arrange
        decimal amount = -50m;
        string cardNumber = "1234567890123456";

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Equal("Valor inválido", result);
    }

    [Fact]
    public void Process_WithNullCardNumber_ReturnsInvalidCardMessage()
    {
        // Arrange
        decimal amount = 100m;
        string? cardNumber = null;

        // Act
        var result = _processor.Process(amount, cardNumber!);

        // Assert
        Assert.Equal("Número do cartão inválido", result);
    }

    [Fact]
    public void Process_WithEmptyCardNumber_ReturnsInvalidCardMessage()
    {
        // Arrange
        decimal amount = 100m;
        string cardNumber = string.Empty;

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Equal("Número do cartão inválido", result);
    }

    [Fact]
    public void Process_WithWhitespaceCardNumber_ReturnsInvalidCardMessage()
    {
        // Arrange
        decimal amount = 100m;
        string cardNumber = "   ";

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Equal("Número do cartão inválido", result);
    }

    [Theory]
    [InlineData(0.01)]
    [InlineData(1.00)]
    [InlineData(50.75)]
    [InlineData(1000.00)]
    [InlineData(9999.99)]
    public void Process_WithVariousValidAmounts_ReturnsSuccessMessage(decimal amount)
    {
        // Arrange
        string cardNumber = "1234567890123456";

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains("processado com sucesso", result);
        Assert.Contains(amount.ToString("C"), result);
    }

    [Fact]
    public void Process_WithLargeAmount_ReturnsSuccessMessage()
    {
        // Arrange
        decimal amount = 999999.99m;
        string cardNumber = "1234567890123456";

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains("processado com sucesso", result);
        Assert.Contains("MercadoPago", result);
    }

    [Fact]
    public void Process_CalculatesFeeCorrectly()
    {
        // Arrange
        decimal amount = 250.00m;
        string cardNumber = "1234567890123456";
        decimal expectedFee = 9.975m; // 250 * 0.0399

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains(expectedFee.ToString("C"), result);
    }

    [Fact]
    public void Process_CalculatesTotalCorrectly()
    {
        // Arrange
        decimal amount = 250.00m;
        string cardNumber = "1234567890123456";
        decimal expectedTotal = 259.975m; // 250 + (250 * 0.0399)

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains(expectedTotal.ToString("C"), result);
    }

    [Fact]
    public void Process_WithAmexCard_ReturnsSuccess()
    {
        // Arrange
        decimal amount = 100m;
        string amexCard = "378282246310005"; // 15 digits

        // Act
        var result = _processor.Process(amount, amexCard);

        // Assert
        Assert.Contains("processado com sucesso", result);
    }
}

