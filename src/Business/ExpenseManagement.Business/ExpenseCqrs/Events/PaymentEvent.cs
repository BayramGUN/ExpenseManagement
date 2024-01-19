namespace ExpenseManagement.Business.ExpenseCqrs.Events;

public record PaymentEvent
{
    public string FromAccountNumber { get; init; } = null!;
    public string ToAccountNumber { get; init; } = null!;
    public decimal Amount { get; init; }
    public string Description { get; init; } = null!;
}