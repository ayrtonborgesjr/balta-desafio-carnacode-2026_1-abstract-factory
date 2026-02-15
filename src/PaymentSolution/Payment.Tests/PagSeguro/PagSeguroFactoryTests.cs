using Payment.Core.Interfaces;
using Payment.Gateways.PagSeguro;

namespace Payment.Tests.PagSeguro;

public class PagSeguroFactoryTests
{
    private readonly PagSeguroFactory _factory;

    public PagSeguroFactoryTests()
    {
        _factory = new PagSeguroFactory();
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
    public void CreateValidator_ReturnsPagSeguroValidator()
    {
        // Act
        var validator = _factory.CreateValidator();

        // Assert
        Assert.IsType<PagSeguroValidator>(validator);
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
    public void CreateProcessor_ReturnsPagSeguroProcessor()
    {
        // Act
        var processor = _factory.CreateProcessor();

        // Assert
        Assert.IsType<PagSeguroProcessor>(processor);
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
    public void CreateLogger_ReturnsPagSeguroLogger()
    {
        // Act
        var logger = _factory.CreateLogger();

        // Assert
        Assert.IsType<PagSeguroLogger>(logger);
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
}

