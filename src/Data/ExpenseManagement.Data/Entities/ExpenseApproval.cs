using ExpenseManagement.Base.Entity;
using ExpenseManagement.Base.Enums;

namespace ExpenseManagement.Data.Entities;


public class ExpenseApproval : BaseEntityWithId
{
    public virtual Expense? Expense { get; set; }
    public virtual int ExpenseId { get; set; }
    public virtual AppUser? Approver { get; set; }
    public virtual int ApproverId { get; set; }

    public Status ApprovalStatus { get; set; } // Approved, Rejected
}