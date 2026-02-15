using Payment.Core.Interfaces;
using Payment.Core.Models;

namespace Payment.Gateways.MercadoPago;

public class MercadoPagoProcessor : IPaymentProcessor
{
    public string Process(decimal amount, string cardNumber)
    {
        Console.WriteLine("MercadoPago: Processando pagamento...");

        if (amount <= 0)
            return "Valor inválido";

        if (string.IsNullOrWhiteSpace(cardNumber))
            return "Número do cartão inválido";

        // Simula taxa de processamento do MercadoPago
        var fee = amount * 0.0399m; // 3.99%
        var total = amount + fee;

        // Simula processamento
        return $"Pagamento de {amount:C} processado com sucesso via MercadoPago! Taxa: {fee:C} | Total: {total:C}";
    }
}

