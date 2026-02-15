namespace Payment.Core.Interfaces;

public interface IPaymentGatewayFactory
{
    ICardValidator CreateValidator();
    IPaymentProcessor CreateProcessor();
    ILogger CreateLogger();
}