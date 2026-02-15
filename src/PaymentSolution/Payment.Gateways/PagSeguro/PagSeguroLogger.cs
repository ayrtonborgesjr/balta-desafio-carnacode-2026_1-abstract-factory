using Payment.Core.Interfaces;

namespace Payment.Gateways.PagSeguro;

public class PagSeguroLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[PagSeguro] {DateTime.Now:HH:mm:ss} - {message}");
    }
}