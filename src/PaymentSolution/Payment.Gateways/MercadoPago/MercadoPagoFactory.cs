using Payment.Core.Interfaces;

namespace Payment.Gateways.MercadoPago;

public class MercadoPagoFactory : IPaymentGatewayFactory
{
    public ICardValidator CreateValidator() => new MercadoPagoValidator();
    public IPaymentProcessor CreateProcessor() => new MercadoPagoProcessor();
    public ILogger CreateLogger() => new MercadoPagoLogger();
}

