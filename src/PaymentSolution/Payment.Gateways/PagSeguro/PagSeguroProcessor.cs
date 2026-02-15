using Payment.Core.Interfaces;
using Payment.Core.Models;

namespace Payment.Gateways.PagSeguro;

public class PagSeguroProcessor : IPaymentProcessor
{
    public string Process(decimal amount, string cardNumber)
    {
        Console.WriteLine("PagSeguro: Processando pagamento...");

        if (amount <= 0)
            return "Valor inválido";

        if (string.IsNullOrWhiteSpace(cardNumber))
            return "Número do cartão inválido";

        // Simula processamento
        return $"Pagamento de {amount:C} processado com sucesso via PagSeguro!";
    }
}