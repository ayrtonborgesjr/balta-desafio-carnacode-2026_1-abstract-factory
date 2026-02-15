namespace Payment.Core.Models;

public class PaymentResult
{
    public bool Success { get; }
    public string TransactionId { get; }
    public string Message { get; }
    public decimal Amount { get; }

    private PaymentResult(
        bool success,
        string transactionId,
        string message,
        decimal amount)
    {
        Success = success;
        TransactionId = transactionId;
        Message = message;
        Amount = amount;
    }

    public static PaymentResult Approved(
        string transactionId,
        decimal amount,
        string message = "Pagamento aprovado")
    {
        return new PaymentResult(
            true,
            transactionId,
            message,
            amount);
    }

    public static PaymentResult Rejected(
        decimal amount,
        string message = "Pagamento recusado")
    {
        return new PaymentResult(
            false,
            string.Empty,
            message,
            amount);
    }
}