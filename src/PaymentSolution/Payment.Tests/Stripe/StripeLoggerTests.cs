using Payment.Gateways.Stripe;

namespace Payment.Tests.Stripe;

public class StripeLoggerTests
{
    private readonly StripeLogger _logger;

    public StripeLoggerTests()
    {
        _logger = new StripeLogger();
    }

    [Fact]
    public void Log_WithValidMessage_DoesNotThrowException()
    {
        // Arrange
        var message = "Test log message";

        // Act & Assert
        var exception = Record.Exception(() => _logger.Log(message));
        Assert.Null(exception);
    }

    [Fact]
    public void Log_WithEmptyMessage_DoesNotThrowException()
    {
        // Arrange
        var message = string.Empty;

        // Act & Assert
        var exception = Record.Exception(() => _logger.Log(message));
        Assert.Null(exception);
    }

    [Fact]
    public void Log_WithNullMessage_DoesNotThrowException()
    {
        // Arrange
        string? message = null;

        // Act & Assert
        var exception = Record.Exception(() => _logger.Log(message!));
        Assert.Null(exception);
    }

    [Fact]
    public void Log_WithLongMessage_DoesNotThrowException()
    {
        // Arrange
        var message = new string('a', 1000);

        // Act & Assert
        var exception = Record.Exception(() => _logger.Log(message));
        Assert.Null(exception);
    }

    [Theory]
    [InlineData("Simple message")]
    [InlineData("Message with numbers 123")]
    [InlineData("Message with special chars !@#$%")]
    [InlineData("Payment processed successfully")]
    [InlineData("Transaction ID: 12345")]
    public void Log_WithVariousMessages_DoesNotThrowException(string message)
    {
        // Act & Assert
        var exception = Record.Exception(() => _logger.Log(message));
        Assert.Null(exception);
    }

    [Fact]
    public void Log_WritesOutputToConsole_CapturesOutput()
    {
        // Arrange
        var message = "Test message";
        var originalOutput = Console.Out;
        using var stringWriter = new StringWriter();

        try
        {
            Console.SetOut(stringWriter);

            // Act
            _logger.Log(message);

            // Assert
            var output = stringWriter.ToString();
            Assert.Contains("[Stripe]", output);
            Assert.Contains(message, output);
        }
        finally
        {
            Console.SetOut(originalOutput);
        }
    }

    [Fact]
    public void Log_IncludesTimestamp_CapturesOutput()
    {
        // Arrange
        var message = "Timestamp test";
        var originalOutput = Console.Out;
        using var stringWriter = new StringWriter();

        try
        {
            Console.SetOut(stringWriter);

            // Act
            _logger.Log(message);

            // Assert
            var output = stringWriter.ToString();
            // Verifica que contém o prefixo [Stripe] e a mensagem
            Assert.Contains("[Stripe]", output);
            Assert.Contains(message, output);
        }
        finally
        {
            Console.SetOut(originalOutput);
        }
    }

    [Fact]
    public void Log_WithMultipleMessages_LogsAll()
    {
        // Arrange
        var messages = new[] { "Message 1", "Message 2", "Message 3" };

        // Act & Assert
        foreach (var message in messages)
        {
            var exception = Record.Exception(() => _logger.Log(message));
            Assert.Null(exception);
        }
    }

    [Fact]
    public void Log_WithUnicodeCharacters_DoesNotThrowException()
    {
        // Arrange
        var message = "💳 Payment via Stripe! ✅ $100.00";

        // Act & Assert
        var exception = Record.Exception(() => _logger.Log(message));
        Assert.Null(exception);
    }

    [Fact]
    public void Log_WithPaymentInformation_LogsCorrectly()
    {
        // Arrange
        var message = "Payment of $150.00 processed successfully";
        var originalOutput = Console.Out;
        using var stringWriter = new StringWriter();

        try
        {
            Console.SetOut(stringWriter);

            // Act
            _logger.Log(message);

            // Assert
            var output = stringWriter.ToString();
            Assert.Contains("[Stripe]", output);
            Assert.Contains("Payment of $150.00 processed successfully", output);
        }
        finally
        {
            Console.SetOut(originalOutput);
        }
    }

    [Fact]
    public void Log_ConsecutiveCalls_WorksCorrectly()
    {
        // Act & Assert
        var exception1 = Record.Exception(() => _logger.Log("First message"));
        var exception2 = Record.Exception(() => _logger.Log("Second message"));
        var exception3 = Record.Exception(() => _logger.Log("Third message"));

        Assert.Null(exception1);
        Assert.Null(exception2);
        Assert.Null(exception3);
    }
}

