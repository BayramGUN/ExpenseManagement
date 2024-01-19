
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManagement.Data.Entities.Configurations;

/// <summary>
/// Configuration for the ExpenseApproval entity, defining the database schema and constraints.
/// </summary>
public class ExpenseApprovalConfiguration : IEntityTypeConfiguration<ExpenseApproval>
{
    /// <summary>
    /// Configures the properties and relationships of the ExpenseApproval entity.
    /// </summary>
    /// <param name="builder">The entity type builder for the ExpenseApproval entity.</param>
    public void Configure(EntityTypeBuilder<ExpenseApproval> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.InsertUserId).IsRequired(true);
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.UpdateUserId).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.Description).IsRequired(false);
        builder.Property(x => x.ApprovalStatus).IsRequired(true);
        builder.Property(x => x.IsActive).IsRequired(true);

        builder.HasIndex(x => x.ApproverId);
        builder.HasIndex(x => x.ExpenseId);

        builder.HasOne(x => x.Expense)
               .WithMany(x => x.ExpenseApprovals)
               .HasForeignKey(x => x.ExpenseId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Approver)
               .WithMany()
               .HasForeignKey(x => x.ApproverId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Restrict);
    }
}