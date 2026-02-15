using Payment.Gateways.Stripe;

namespace Payment.Tests.Stripe;

public class StripeProcessorTests
{
    private readonly StripeProcessor _processor;

    public StripeProcessorTests()
    {
        _processor = new StripeProcessor();
    }

    [Fact]
    public void Process_WithValidAmountAndCardNumber_ReturnsSuccessMessage()
    {
        // Arrange
        decimal amount = 100.00m;
        string cardNumber = "4242424242424242";

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains("processado via Stripe", result);
        Assert.Contains(amount.ToString("C"), result);
    }

    [Fact]
    public void Process_WithValidAmount_IncludesPercentageFeeInResult()
    {
        // Arrange
        decimal amount = 100.00m;
        string cardNumber = "4242424242424242";
        decimal expectedPercentageFee = amount * 0.029m; // 2.9%

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains(expectedPercentageFee.ToString("C"), result);
    }

    [Fact]
    public void Process_WithValidAmount_IncludesFixedFeeInResult()
    {
        // Arrange
        decimal amount = 100.00m;
        string cardNumber = "4242424242424242";
        decimal fixedFee = 0.30m;

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains(fixedFee.ToString("C"), result);
    }

    [Fact]
    public void Process_WithValidAmount_IncludesTotalFeeInResult()
    {
        // Arrange
        decimal amount = 100.00m;
        string cardNumber = "4242424242424242";
        decimal expectedTotalFee = (amount * 0.029m) + 0.30m;

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains("Taxa:", result);
        Assert.Contains(expectedTotalFee.ToString("C"), result);
    }

    [Fact]
    public void Process_WithValidAmount_IncludesTotalInResult()
    {
        // Arrange
        decimal amount = 100.00m;
        string cardNumber = "4242424242424242";
        decimal expectedTotal = amount + (amount * 0.029m) + 0.30m;

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
        string cardNumber = "4242424242424242";

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
        string cardNumber = "4242424242424242";

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
        string cardNumber = "4242424242424242";

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains("processado via Stripe", result);
        Assert.Contains(amount.ToString("C"), result);
    }

    [Fact]
    public void Process_WithLargeAmount_ReturnsSuccessMessage()
    {
        // Arrange
        decimal amount = 999999.99m;
        string cardNumber = "4242424242424242";

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains("processado via Stripe", result);
    }

    [Fact]
    public void Process_CalculatesTotalFeeCorrectly()
    {
        // Arrange
        decimal amount = 250.00m;
        string cardNumber = "4242424242424242";
        decimal expectedTotalFee = (250.00m * 0.029m) + 0.30m; // 7.25 + 0.30 = 7.55

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains(expectedTotalFee.ToString("C"), result);
    }

    [Fact]
    public void Process_CalculatesTotalCorrectly()
    {
        // Arrange
        decimal amount = 250.00m;
        string cardNumber = "4242424242424242";
        decimal expectedTotal = 250.00m + (250.00m * 0.029m) + 0.30m; // 257.55

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains(expectedTotal.ToString("C"), result);
    }

    [Fact]
    public void Process_WithSmallAmount_MinimumFeeApplies()
    {
        // Arrange
        decimal amount = 1.00m;
        string cardNumber = "4242424242424242";
        decimal expectedPercentageFee = 0.029m; // 1 * 2.9%
        decimal fixedFee = 0.30m;
        decimal expectedTotal = amount + expectedPercentageFee + fixedFee;

        // Act
        var result = _processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains(expectedTotal.ToString("C"), result);
    }

    [Fact]
    public void Process_WithAmexCard_ProcessesSuccessfully()
    {
        // Arrange
        decimal amount = 100m;
        string amexCard = "378282246310005"; // 15 digits

        // Act
        var result = _processor.Process(amount, amexCard);

        // Assert
        Assert.Contains("processado via Stripe", result);
    }

    [Fact]
    public void Process_WithVisaCard_ProcessesSuccessfully()
    {
        // Arrange
        decimal amount = 100m;
        string visaCard = "4242424242424242";

        // Act
        var result = _processor.Process(amount, visaCard);

        // Assert
        Assert.Contains("processado via Stripe", result);
    }

    [Fact]
    public void Process_WithMastercardCard_ProcessesSuccessfully()
    {
        // Arrange
        decimal amount = 100m;
        string mastercardCard = "5555555555554444";

        // Act
        var result = _processor.Process(amount, mastercardCard);

        // Assert
        Assert.Contains("processado via Stripe", result);
    }

    [Fact]
    public void Process_FeeCalculation_IsConsistent()
    {
        // Arrange
        decimal amount = 100.00m;
        string cardNumber = "4242424242424242";

        // Act
        var result1 = _processor.Process(amount, cardNumber);
        var result2 = _processor.Process(amount, cardNumber);

        // Assert - Results should be identical
        Assert.Equal(result1, result2);
    }
}

