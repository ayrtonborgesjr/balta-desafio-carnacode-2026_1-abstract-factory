using Payment.Core.Interfaces;

namespace Payment.Gateways.MercadoPago;

public class MercadoPagoLogger : ILogger
{
    public void Log(string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[MercadoPago] {DateTime.Now:HH:mm:ss} - {message}");
        Console.ResetColor();
    }
}

