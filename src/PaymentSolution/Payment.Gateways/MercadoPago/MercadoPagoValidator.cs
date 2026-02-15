using Payment.Core.Interfaces;

namespace Payment.Gateways.MercadoPago;

public class MercadoPagoValidator : ICardValidator
{
    public bool Validate(string cardNumber)
    {
        Console.WriteLine("MercadoPago: Validando cartão...");

        if (string.IsNullOrWhiteSpace(cardNumber))
            return false;

        // MercadoPago aceita cartões com 15 ou 16 dígitos
        return cardNumber.Length == 15 || cardNumber.Length == 16;
    }
}

