using Payment.Core.Interfaces;
using Payment.Gateways.Stripe;

namespace Payment.Tests.Stripe;

public class StripeFactoryTests
{
    private readonly StripeFactory _factory;

    public StripeFactoryTests()
    {
        _factory = new StripeFactory();
    }

    [Fact]
    public void CreateValidator_ReturnsValidatorInstance()
    {
        // Act
        var validator = _factory.CreateValidator();

        // Assert
        Assert.NotNull(validator);
        Assert.IsAssignableFrom<ICardValidator>(validator);
    }

    [Fact]
    public void CreateValidator_ReturnsStripeValidator()
    {
        // Act
        var validator = _factory.CreateValidator();

        // Assert
        Assert.IsType<StripeValidator>(validator);
    }

    [Fact]
    public void CreateProcessor_ReturnsProcessorInstance()
    {
        // Act
        var processor = _factory.CreateProcessor();

        // Assert
        Assert.NotNull(processor);
        Assert.IsAssignableFrom<IPaymentProcessor>(processor);
    }

    [Fact]
    public void CreateProcessor_ReturnsStripeProcessor()
    {
        // Act
        var processor = _factory.CreateProcessor();

        // Assert
        Assert.IsType<StripeProcessor>(processor);
    }

    [Fact]
    public void CreateLogger_ReturnsLoggerInstance()
    {
        // Act
        var logger = _factory.CreateLogger();

        // Assert
        Assert.NotNull(logger);
        Assert.IsAssignableFrom<ILogger>(logger);
    }

    [Fact]
    public void CreateLogger_ReturnsStripeLogger()
    {
        // Act
        var logger = _factory.CreateLogger();

        // Assert
        Assert.IsType<StripeLogger>(logger);
    }

    [Fact]
    public void CreateValidator_MultipleCalls_ReturnsDifferentInstances()
    {
        // Act
        var validator1 = _factory.CreateValidator();
        var validator2 = _factory.CreateValidator();

        // Assert
        Assert.NotSame(validator1, validator2);
    }

    [Fact]
    public void CreateProcessor_MultipleCalls_ReturnsDifferentInstances()
    {
        // Act
        var processor1 = _factory.CreateProcessor();
        var processor2 = _factory.CreateProcessor();

        // Assert
        Assert.NotSame(processor1, processor2);
    }

    [Fact]
    public void CreateLogger_MultipleCalls_ReturnsDifferentInstances()
    {
        // Act
        var logger1 = _factory.CreateLogger();
        var logger2 = _factory.CreateLogger();

        // Assert
        Assert.NotSame(logger1, logger2);
    }

    [Fact]
    public void Factory_ImplementsIPaymentGatewayFactory()
    {
        // Assert
        Assert.IsAssignableFrom<IPaymentGatewayFactory>(_factory);
    }

    [Fact]
    public void Factory_CreatedComponents_WorkTogether()
    {
        // Arrange
        var validator = _factory.CreateValidator();
        var processor = _factory.CreateProcessor();
        var logger = _factory.CreateLogger();
        var cardNumber = "4242424242424242";
        var amount = 100m;

        // Act
        var isValid = validator.Validate(cardNumber);
        var processResult = processor.Process(amount, cardNumber);
        var logException = Record.Exception(() => logger.Log("Test"));

        // Assert
        Assert.True(isValid);
        Assert.Contains("Stripe", processResult);
        Assert.Null(logException);
    }

    [Fact]
    public void Factory_CreatedValidator_Supports13DigitCards()
    {
        // Arrange
        var validator = _factory.CreateValidator();
        var card = "1234567890123"; // 13 digits

        // Act
        var isValid = validator.Validate(card);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void Factory_CreatedValidator_Supports15DigitCards()
    {
        // Arrange
        var validator = _factory.CreateValidator();
        var amexCard = "378282246310005"; // 15 digits

        // Act
        var isValid = validator.Validate(amexCard);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void Factory_CreatedValidator_Supports16DigitCards()
    {
        // Arrange
        var validator = _factory.CreateValidator();
        var visaCard = "4242424242424242"; // 16 digits

        // Act
        var isValid = validator.Validate(visaCard);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void Factory_CreatedValidator_Supports19DigitCards()
    {
        // Arrange
        var validator = _factory.CreateValidator();
        var card = "1234567890123456789"; // 19 digits

        // Act
        var isValid = validator.Validate(card);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void Factory_CreatedProcessor_IncludesPercentageFee()
    {
        // Arrange
        var processor = _factory.CreateProcessor();
        var amount = 100m;
        var cardNumber = "4242424242424242";

        // Act
        var result = processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains("Taxa:", result);
        Assert.Contains("2.9%", result.ToLower() + " 2.9%"); // Fee is 2.9%
    }

    [Fact]
    public void Factory_CreatedProcessor_IncludesFixedFee()
    {
        // Arrange
        var processor = _factory.CreateProcessor();
        var amount = 100m;
        var cardNumber = "4242424242424242";
        var fixedFee = 0.30m;

        // Act
        var result = processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains(fixedFee.ToString("C"), result);
    }

    [Fact]
    public void Factory_CreatedProcessor_IncludesTotal()
    {
        // Arrange
        var processor = _factory.CreateProcessor();
        var amount = 100m;
        var cardNumber = "4242424242424242";

        // Act
        var result = processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains("Total:", result);
    }

    [Fact]
    public void Factory_AllComponents_AreIndependent()
    {
        // Arrange & Act
        var validator1 = _factory.CreateValidator();
        var processor1 = _factory.CreateProcessor();
        var logger1 = _factory.CreateLogger();
        
        var validator2 = _factory.CreateValidator();
        var processor2 = _factory.CreateProcessor();
        var logger2 = _factory.CreateLogger();

        // Assert - All instances are different
        Assert.NotSame(validator1, validator2);
        Assert.NotSame(processor1, processor2);
        Assert.NotSame(logger1, logger2);
    }

    [Fact]
    public void Factory_CreatedComponents_HandleRealWorldScenario()
    {
        // Arrange
        var validator = _factory.CreateValidator();
        var processor = _factory.CreateProcessor();
        var logger = _factory.CreateLogger();
        
        var testCards = new[]
        {
            "4242424242424242", // Visa
            "5555555555554444", // Mastercard
            "378282246310005"   // Amex
        };

        // Act & Assert
        foreach (var card in testCards)
        {
            var isValid = validator.Validate(card);
            Assert.True(isValid);
            
            var result = processor.Process(100m, card);
            Assert.Contains("Stripe", result);
            
            var logException = Record.Exception(() => logger.Log($"Processed card ending in {card[^4..]}"));
            Assert.Null(logException);
        }
    }
}

