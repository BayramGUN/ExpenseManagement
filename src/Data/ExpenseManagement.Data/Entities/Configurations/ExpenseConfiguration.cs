
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManagement.Data.Entities.Configurations;

/// <summary>
/// Configuration for the Expense entity, defining the database schema and constraints.
/// </summary>
public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    /// <summary>
    /// Configures the properties and relationships of the Expense entity.
    /// </summary>
    /// <param name="builder">The entity type builder for the Expense entity.</param>
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.InsertUserId).IsRequired(true);
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.UpdateUserId).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.Amount).IsRequired(true).HasPrecision(18,2);
        builder.Property(x => x.AppUserId).IsRequired(true);
        builder.Property(x => x.Status).IsRequired(true);
        builder.Property(x => x.IsActive).IsRequired(true);
        builder.Property(x => x.Description).IsRequired(false);
        builder.Property(x => x.ReceiptPhotoUrl).IsRequired(true);

        builder.HasIndex(x => x.AppUserId);

        builder.HasMany(x => x.ExpenseApprovals)
            .WithOne(x => x.Expense)
            .HasForeignKey(x => x.ExpenseId);
            
        builder.HasOne(e => e.Payment)
               .WithOne(p => p.Expense)
               .HasForeignKey<Payment>(p => p.ExpenseId)
               .IsRequired(false);
    }
}