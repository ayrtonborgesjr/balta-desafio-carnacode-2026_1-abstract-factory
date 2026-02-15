using Payment.Core.Interfaces;

namespace Payment.Gateways.PagSeguro;

public class PagSeguroFactory : IPaymentGatewayFactory
{
    public ICardValidator CreateValidator() => new PagSeguroValidator();
    public IPaymentProcessor CreateProcessor() => new PagSeguroProcessor();
    public ILogger CreateLogger() => new PagSeguroLogger();
}