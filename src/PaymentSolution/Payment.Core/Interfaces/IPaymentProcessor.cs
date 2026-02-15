namespace Payment.Core.Interfaces;

public interface IPaymentProcessor
{
    string Process(decimal amount, string cardNumber);
}