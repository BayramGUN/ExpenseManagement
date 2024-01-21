namespace ExpenseManagement.Data.Entities;

public class ApprovalStatusReport
{
    public int PendingExpenseCount { get; set; }
    public int ApprovedExpenseCount { get; set; }
    public int RejectedExpenseCount { get; set; }
}