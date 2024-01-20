# Entity Model

- [Expense Entity Model](#expense-entity)

## Expense Entity

```csharp
using ExpenseManagement.Base.Entity;
using ExpenseManagement.Base.Enums;

namespace ExpenseManagement.Data.Entities;

public class Expense : BaseEntityWithId
{
    public int AppUserId { get; set; }
    public virtual AppUser AppUser { get; set; } = null!;

    public decimal Amount { get; set; }
    public string Title { get; set; } = null!;
    public DateTime ExpensedDate { get; set; }
    public string? Description { get; set; }
    public Status Status { get ; set; }
    public string ReceiptPhotoUrl { get; set; } = null!;

    public virtual List<ExpenseApproval>? ExpenseApprovals { get; set; }
    public virtual Payment? Payment { get; set; }
}
```

> [Back to README.md](../../README.md#entities)
