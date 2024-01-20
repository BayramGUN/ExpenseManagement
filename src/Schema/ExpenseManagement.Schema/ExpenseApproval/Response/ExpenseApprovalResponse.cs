namespace ExpenseManagement.Schema.ExpenseApproval.Responses;

public class ExpenseApprovalResponse
{
    public int Id { get; set; }
    public string? ExpenseTitle {get; set; }
    public string? ApproverName {get; set; }
    public string? Description { get; set; }
    public string? ApprovalStatus { get; set; }
}