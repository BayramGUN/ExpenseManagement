# Entity Model

> [Back to README.md](../../README.md)

- [Payment Entity Model](#payment-entity)

## Payment Entity

```csharp
using Azure.Core;
using ExpenseManagement.Base.Entity;

namespace ExpenseManagement.Data.Entities;

public class Payment : BaseEntityWithId
{
    public int ExpenseId { get; set; }
    public DateTime PaymentDate { get; set; }
    public required string PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public virtual Expense? Expense { get; set; }
}
```
