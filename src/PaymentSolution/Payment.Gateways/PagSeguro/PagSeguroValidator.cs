using Payment.Core.Interfaces;

namespace Payment.Gateways.PagSeguro;

public class PagSeguroValidator : ICardValidator
{
    public bool Validate(string cardNumber)
    {
        Console.WriteLine("PagSeguro: Validando cartão...");

        if (string.IsNullOrWhiteSpace(cardNumber))
            return false;

        return cardNumber.Length == 16;
    }
}