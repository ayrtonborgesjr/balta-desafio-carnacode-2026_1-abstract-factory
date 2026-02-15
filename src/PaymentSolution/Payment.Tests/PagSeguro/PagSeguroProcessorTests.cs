using Payment.Gateways.PagSeguro;

namespace Payment.Tests.PagSeguro;

public class PagSeguroProcessorTests
{
    private readonly PagSeguroProcessor _processor;

    public PagSeguroProcessorTests()
    {
        _processor = new PagSeguroProcessor();
    }

    [Fact]
    public void Process_WithValidAmountAndCardNumber_ReturnsSuccessMessage()
    {
        // Arrange
        decimal amount = 100.50m;
        string cardNumber = "1234567890123456";

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains("processado com sucesso", result);
        Assert.Contains("PagSeguro", result);
        Assert.Contains(amount.ToString("C"), result);
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
        Assert.Contains("PagSeguro", result);
    }
}

