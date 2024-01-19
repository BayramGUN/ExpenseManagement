using ExpenseManagement.Base.Enums;
using ExpenseManagement.Base.Schema;

namespace ExpenseManagement.Schema.Expense.Requests;

public class GetExpensesByParameterRequest
{
    public Status? Status { get; set; }
    public DateTime? ExpensedDate { get; set; } = null;
    public decimal? Amount { get; set; }
}