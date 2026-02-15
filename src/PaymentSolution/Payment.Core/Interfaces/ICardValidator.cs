namespace Payment.Core.Interfaces;

public interface ICardValidator
{
    bool Validate(string cardNumber);
}