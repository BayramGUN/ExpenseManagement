using ExpenseManagement.Base.Enums;

namespace ExpenseManagement.Schema.Expense.Responses;

public class ExpenseResponse
{
    public int Id { get; set; }
    public string? EmployeeName { get ; set;}
    public decimal Amount { get; set; }
    public DateTime ExpensedDate { get; set; }
    public string? Description { get; set; }
    public string? Status { get ; set; }
    public string? ReceiptPhotoUrl { get; set; }
}