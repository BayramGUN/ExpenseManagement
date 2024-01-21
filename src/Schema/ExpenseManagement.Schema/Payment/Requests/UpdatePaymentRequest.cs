using ExpenseManagement.Base.Schema;

namespace ExpenseManagement.Schema.Payment.Requests;

public class UpdatePaymentRequest : BaseRequest
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int ExpenseId { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? PaymentMethod { get; set; }
}