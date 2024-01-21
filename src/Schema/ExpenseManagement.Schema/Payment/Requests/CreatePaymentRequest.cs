using ExpenseManagement.Base.Schema;

namespace ExpenseManagement.Schema.Payment.Requests;

public class CreatePaymentRequest : BaseRequest
{
    public decimal Amount { get; set; }
    public int ExpenseId { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? PaymentMethod { get; set; }
}