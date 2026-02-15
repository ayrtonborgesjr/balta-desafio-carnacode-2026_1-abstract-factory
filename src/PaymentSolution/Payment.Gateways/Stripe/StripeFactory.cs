using Payment.Core.Interfaces;

namespace Payment.Gateways.Stripe;

public class StripeFactory : IPaymentGatewayFactory
{
    public ICardValidator CreateValidator() => new StripeValidator();
    public IPaymentProcessor CreateProcessor() => new StripeProcessor();
    public ILogger CreateLogger() => new StripeLogger();
}

