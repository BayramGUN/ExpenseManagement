using ExpenseManagement.Base.Schema;

namespace ExpenseManagement.Schema.Expense.Requests;

public class CreateExpenseRequest : BaseRequest
{
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string? Title { get; set; }
    public string ReceiptPhotoUrl { get; set; } = null!;
}