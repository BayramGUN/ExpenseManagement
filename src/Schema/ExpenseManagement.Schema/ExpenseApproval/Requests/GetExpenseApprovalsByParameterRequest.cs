using ExpenseManagement.Base.Enums;

namespace ExpenseManagement.Schema.ExpenseApproval.Requests;

public class GetExpenseApprovalsByParameterRequest
{
    public Status? Status { get; set; }
    public int? ApproverId { get; set; }
}