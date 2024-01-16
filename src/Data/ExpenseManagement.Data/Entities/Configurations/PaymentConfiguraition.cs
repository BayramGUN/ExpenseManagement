
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManagement.Data.Entities.Configurations;

/// <summary>
/// Configuration for the Payment entity, defining the database schema and constraints.
/// </summary>
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    /// <summary>
    /// Configures the properties and relationships of the Payment entity.
    /// </summary>
    /// <param name="builder">The entity type builder for the Payment entity.</param>
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.InsertUserId).IsRequired(true);
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.UpdateUserId).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.Amount).IsRequired(true).HasPrecision(18,2);
        builder.Property(x => x.IsActive).IsRequired(true);

        builder.HasOne(e => e.Expense)
               .WithOne(p => p.Payment)
               .IsRequired(false);
    }
}