using Payment.Core.Interfaces;
using Payment.Gateways.MercadoPago;

namespace Payment.Tests.MercadoPago;

public class MercadoPagoFactoryTests
{
    private readonly MercadoPagoFactory _factory;

    public MercadoPagoFactoryTests()
    {
        _factory = new MercadoPagoFactory();
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
    public void CreateValidator_ReturnsMercadoPagoValidator()
    {
        // Act
        var validator = _factory.CreateValidator();

        // Assert
        Assert.IsType<MercadoPagoValidator>(validator);
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
    public void CreateProcessor_ReturnsMercadoPagoProcessor()
    {
        // Act
        var processor = _factory.CreateProcessor();

        // Assert
        Assert.IsType<MercadoPagoProcessor>(processor);
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
    public void CreateLogger_ReturnsMercadoPagoLogger()
    {
        // Act
        var logger = _factory.CreateLogger();

        // Assert
        Assert.IsType<MercadoPagoLogger>(logger);
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
        var cardNumber = "1234567890123456";
        var amount = 100m;

        // Act
        var isValid = validator.Validate(cardNumber);
        var processResult = processor.Process(amount, cardNumber);
        var logException = Record.Exception(() => logger.Log("Test"));

        // Assert
        Assert.True(isValid);
        Assert.Contains("sucesso", processResult);
        Assert.Null(logException);
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
        var visaCard = "4532015112830366"; // 16 digits

        // Act
        var isValid = validator.Validate(visaCard);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void Factory_CreatedProcessor_IncludesFees()
    {
        // Arrange
        var processor = _factory.CreateProcessor();
        var amount = 100m;
        var cardNumber = "1234567890123456";

        // Act
        var result = processor.Process(amount, cardNumber);

        // Assert
        Assert.Contains("Taxa:", result);
        Assert.Contains("Total:", result);
    }
}

