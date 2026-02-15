using Payment.Gateways.MercadoPago;

namespace Payment.Tests.MercadoPago;

public class MercadoPagoLoggerTests
{
    private readonly MercadoPagoLogger _logger;

    public MercadoPagoLoggerTests()
    {
        _logger = new MercadoPagoLogger();
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
    [InlineData("Mensagem em português")]
    [InlineData("Payment processed successfully")]
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
            Assert.Contains("[MercadoPago]", output);
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
            // Verifica que contém um padrão de hora HH:mm:ss
            Assert.Matches(@"\d{2}:\d{2}:\d{2}", output);
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
        var message = "🎉 Pagamento realizado! 💰 R$ 100,00";

        // Act & Assert
        var exception = Record.Exception(() => _logger.Log(message));
        Assert.Null(exception);
    }
}

