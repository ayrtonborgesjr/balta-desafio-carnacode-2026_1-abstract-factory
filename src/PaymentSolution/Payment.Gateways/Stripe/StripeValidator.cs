using Payment.Core.Interfaces;

namespace Payment.Gateways.Stripe;

public class StripeValidator : ICardValidator
{
    public bool Validate(string cardNumber)
    {
        Console.WriteLine("Stripe: Validando cartão...");

        if (string.IsNullOrWhiteSpace(cardNumber))
            return false;

        // Stripe aceita cartões com 13 a 19 dígitos
        var length = cardNumber.Length;
        return length >= 13 && length <= 19;
    }
}

