using ExpenseManagement.Base.Schema;

namespace ExpenseManagement.Schema.ExpenseApproval.Requests;

public class CreateExpenseApprovalRequest : BaseRequest
{
    public int ExpenseId { get; set; }
    public int ApproverId { get; set; }
    public string? Description { get; set; }
    public string? ApprovalStatus { get; set; }
}