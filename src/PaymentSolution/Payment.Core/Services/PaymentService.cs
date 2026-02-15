using Payment.Core.Interfaces;

namespace Payment.Core.Services;

public class PaymentService
{
    private readonly ICardValidator _validator;
    private readonly IPaymentProcessor _processor;
    private readonly ILogger _logger;

    public PaymentService(IPaymentGatewayFactory factory)
    {
        _validator = factory.CreateValidator();
        _processor = factory.CreateProcessor();
        _logger = factory.CreateLogger();
    }

    public void ProcessPayment(decimal amount, string cardNumber)
    {
        if (!_validator.Validate(cardNumber))
        {
            Console.WriteLine("Cartão inválido");
            return;
        }

        var result = _processor.Process(amount, cardNumber);
        _logger.Log($"Transação processada: {result}");
    }
}
