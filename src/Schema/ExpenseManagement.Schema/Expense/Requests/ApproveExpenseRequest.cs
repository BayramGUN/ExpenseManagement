using ExpenseManagement.Base.Enums;
using ExpenseManagement.Base.Schema;

namespace ExpenseManagement.Schema.Expense.Requests;

public class ApproveExpenseRequest : BaseRequest
{
    public Status Status { get; set; }
    public int ExpenseId { get; set; }
    public string? Description { get; set; }
}