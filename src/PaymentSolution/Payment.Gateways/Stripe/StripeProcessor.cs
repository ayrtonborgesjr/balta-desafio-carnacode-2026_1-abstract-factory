using Payment.Core.Interfaces;

namespace Payment.Gateways.Stripe;

public class StripeProcessor : IPaymentProcessor
{
    public string Process(decimal amount, string cardNumber)
    {
        Console.WriteLine("Stripe: Processando pagamento...");

        if (amount <= 0)
            return "Valor inválido";

        if (string.IsNullOrWhiteSpace(cardNumber))
            return "Número do cartão inválido";

        // Simula taxa fixa de Stripe: 2.9% + R$ 0,30
        var percentageFee = amount * 0.029m; // 2.9%
        var fixedFee = 0.30m; // R$ 0,30
        var totalFee = percentageFee + fixedFee;
        var total = amount + totalFee;

        // Simula processamento
        return $"Pagamento de {amount:C} processado via Stripe! Taxa: {totalFee:C} ({percentageFee:C} + {fixedFee:C}) | Total: {total:C}";
    }
}

