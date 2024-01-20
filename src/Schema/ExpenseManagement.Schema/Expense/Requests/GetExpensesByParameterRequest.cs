using ExpenseManagement.Base.Enums;

namespace ExpenseManagement.Schema.Expense.Requests;

public class GetExpensesByParameterRequest
{
    public Status? Status { get; set; }
    public DateTime? ExpensedDate { get; set; } = null;
    public decimal? Amount { get; set; }
}