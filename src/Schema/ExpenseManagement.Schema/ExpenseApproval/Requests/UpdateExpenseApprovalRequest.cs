using ExpenseManagement.Base.Enums;
using ExpenseManagement.Base.Schema;

namespace ExpenseManagement.Schema.ExpenseApproval.Requests;

public class UpdateExpenseApprovalRequest : BaseRequest
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public Status? ApprovalStatus { get; set; }
}