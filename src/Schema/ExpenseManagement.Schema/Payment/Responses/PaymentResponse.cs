using ExpenseManagement.Base.Enums;

namespace ExpenseManagement.Schema.Payment.Responses;

public class PaymentResponse
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int ExpenseId { get; set; }
    public int AppUserId { get; set; }
    public string? AppUserName { get; set; }
}