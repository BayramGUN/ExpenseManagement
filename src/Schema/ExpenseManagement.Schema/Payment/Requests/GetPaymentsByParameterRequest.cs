using ExpenseManagement.Base.Enums;

namespace ExpenseManagement.Schema.Payment.Requests;

public class GetPaymentsByParameterRequest
{
    public DateTime? PaymentDate { get; set; } = null;
    public decimal? Amount { get; set; }
    public string? PaymentMethod { get; set; }
    public int? AppUserId { get; set; }

}