# Entity Model

> [Back to README.md](../../README.md)

- [ExpenseApproval Entity Model](#expenseapproval-entity)

## ExpenseApproval Entity

```csharp
using ExpenseManagement.Base.Entity;
using ExpenseManagement.Base.Enums;

namespace ExpenseManagement.Data.Entities;


public class ExpenseApproval : BaseEntityWithId
{
    public virtual Expense? Expense { get; set; }
    public virtual int ExpenseId { get; set; }
    public virtual AppUser? Approver { get; set; }
    public virtual int ApproverId { get; set; }
    public string? Description { get; set; }

    public Status ApprovalStatus { get; set; }
}
```
