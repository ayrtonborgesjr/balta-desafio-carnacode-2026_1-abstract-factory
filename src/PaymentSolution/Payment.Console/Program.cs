using Payment.Core.Services;
using Payment.Gateways.PagSeguro;
using Payment.Gateways.MercadoPago;
using Payment.Gateways.Stripe;

Console.WriteLine("=== Sistema de Pagamentos - Abstract Factory Pattern ===\n");

// ========================================
// Teste com PagSeguro
// ========================================
Console.WriteLine("=== Teste PagSeguro ===\n");

var pagSeguroFactory = new PagSeguroFactory();
var pagSeguroService = new PaymentService(pagSeguroFactory);

pagSeguroService.ProcessPayment(
    150.00m,
    "1234567890123456"
);

Console.WriteLine("\n" + new string('-', 60) + "\n");

// ========================================
// Teste com MercadoPago
// ========================================
Console.WriteLine("=== Teste MercadoPago ===\n");

var mercadoPagoFactory = new MercadoPagoFactory();
var mercadoPagoService = new PaymentService(mercadoPagoFactory);

mercadoPagoService.ProcessPayment(
    150.00m,
    "1234567890123456"
);

Console.WriteLine("\n" + new string('-', 60) + "\n");

// ========================================
// Teste com MercadoPago - Cartão American Express (15 dígitos)
// ========================================
Console.WriteLine("=== Teste MercadoPago - American Express ===\n");

mercadoPagoService.ProcessPayment(
    200.00m,
    "378282246310005"
);

Console.WriteLine("\n" + new string('-', 60) + "\n");

// ========================================
// Teste com Stripe
// ========================================
Console.WriteLine("=== Teste Stripe ===\n");

var stripeFactory = new StripeFactory();
var stripeService = new PaymentService(stripeFactory);

stripeService.ProcessPayment(
    150.00m,
    "4242424242424242" // Visa test card
);

Console.WriteLine("\n" + new string('-', 60) + "\n");

// ========================================
// Teste com Stripe - Mastercard
// ========================================
Console.WriteLine("=== Teste Stripe - Mastercard ===\n");

stripeService.ProcessPayment(
    250.00m,
    "5555555555554444" // Mastercard test card
);

Console.WriteLine("\n" + new string('-', 60) + "\n");

// ========================================
// Teste com Stripe - Cartão de 13 dígitos
// ========================================
Console.WriteLine("=== Teste Stripe - Cartão 13 dígitos ===\n");

stripeService.ProcessPayment(
    100.00m,
    "1234567890123" // 13 digit card
);

Console.WriteLine("\n" + new string('-', 60) + "\n");

// ========================================
// Teste com cartão inválido
// ========================================
Console.WriteLine("=== Teste com Cartão Inválido ===\n");

pagSeguroService.ProcessPayment(
    100.00m,
    "123" // Cartão inválido
);

Console.WriteLine("\n" + new string('-', 60) + "\n");

// ========================================
// Comparação de taxas entre gateways
// ========================================
Console.WriteLine("=== Comparação de Taxas - R$ 1000,00 ===\n");

Console.WriteLine("PagSeguro:");
pagSeguroService.ProcessPayment(1000.00m, "1234567890123456");

Console.WriteLine("\nMercadoPago:");
mercadoPagoService.ProcessPayment(1000.00m, "1234567890123456");

Console.WriteLine("\nStripe:");
stripeService.ProcessPayment(1000.00m, "4242424242424242");

Console.WriteLine("\nPressione ENTER para sair...");
Console.ReadLine();