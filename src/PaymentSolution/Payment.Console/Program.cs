using Payment.Core.Services;
using Payment.Gateways.PagSeguro;

Console.WriteLine("=== Teste PagSeguro ===\n");

// Cria a factory do gateway
var pagSeguroFactory = new PagSeguroFactory();

// Injeta no serviço
var paymentService = new PaymentService(pagSeguroFactory);

// Executa pagamento
paymentService.ProcessPayment(
    150.00m,
    "1234567890123456"
);

Console.ReadLine();