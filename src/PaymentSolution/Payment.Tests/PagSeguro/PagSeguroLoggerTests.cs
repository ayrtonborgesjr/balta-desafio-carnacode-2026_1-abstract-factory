using Payment.Gateways.PagSeguro;

namespace Payment.Tests.PagSeguro;

public class PagSeguroLoggerTests
{
    private readonly PagSeguroLogger _logger;

    public PagSeguroLoggerTests()
    {
        _logger = new PagSeguroLogger();
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
            Assert.Contains("[PagSeguro]", output);
            Assert.Contains(message, output);
        }
        finally
        {
            Console.SetOut(originalOutput);
        }
    }
}

