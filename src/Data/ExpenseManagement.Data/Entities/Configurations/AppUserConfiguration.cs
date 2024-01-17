
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManagement.Data.Entities.Configurations;

/// <summary>
/// Configuration for the AppUser entity, defining the database schema and constraints.
/// </summary>
public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    /// <summary>
    /// Configures the properties and relationships of the AppUser entity.
    /// </summary>
    /// <param name="builder">The entity type builder for the AppUser entity.</param>
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.InsertUserId).IsRequired(true);
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.UpdateUserId).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
    
        builder.Property(x => x.IdentityNumber).IsRequired(true).HasMaxLength(11);
        builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.Email).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.Phone).IsRequired(true).HasMaxLength(10);
        builder.Property(x => x.Password).IsRequired(true).HasMaxLength(250);
        builder.Property(x => x.Role).IsRequired(true).HasMaxLength(30);
        builder.Property(x => x.LastActivityDate).IsRequired(true);
        builder.Property(x => x.PasswordRetryCount).IsRequired(true);
        builder.Property(x => x.Status).IsRequired(true);
        builder.Property(x => x.IBAN).IsRequired(true).HasMaxLength(34);

        builder.HasIndex(x => x.IdentityNumber).IsUnique(true);
        builder.HasIndex(x => x.Email).IsUnique(true);
        builder.HasIndex(x => x.Phone).IsUnique(true);

        builder.HasMany(x => x.Expenses)
            .WithOne(x => x.AppUser)
            .HasForeignKey(x => x.AppUserId);
    }
}