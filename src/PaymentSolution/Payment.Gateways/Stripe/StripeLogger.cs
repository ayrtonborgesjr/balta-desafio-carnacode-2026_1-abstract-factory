using Payment.Core.Interfaces;

namespace Payment.Gateways.Stripe;

public class StripeLogger : ILogger
{
    public void Log(string message)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"[Stripe] {DateTime.Now:HH:mm:ss} - {message}");
        Console.ResetColor();
    }
}

