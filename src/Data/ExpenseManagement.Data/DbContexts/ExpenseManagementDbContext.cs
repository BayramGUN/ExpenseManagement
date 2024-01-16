using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.Data.DbContexts;

/// <summary>
/// Represents the Entity Framework Core DbContext for the ExpenseManagement application, providing access to database tables.
/// </summary>
public class ExpenseManagementDbContext : DbContext
{   
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ExpenseManagementDbContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to be used by the DbContext.</param>
    public ExpenseManagementDbContext(
        DbContextOptions<ExpenseManagementDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the DbSet for the AppUser entity, providing access to the AppUsers table.
    /// </summary>
    public DbSet<AppUser> AppUsers { get; set; } = null!;

    /// <summary>
    /// Configures the model of the database, including applying entity configurations from the ExpenseManagement assembly.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to construct the database model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(ExpenseManagementDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}