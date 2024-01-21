namespace ExpenseManagement.Business.PaymentCqrs.Events;

public record PaymentEvent
{
    public int FromAccountNumber { get; init; }
    public int ToAccountNumber { get; init; }
    public decimal Amount { get; init; }
    public string Description { get; init; } = null!;
}