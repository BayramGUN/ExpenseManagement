using ExpenseManagement.Base.Constants.Authentication;
using ExpenseManagement.Base.Enums;
using ExpenseManagement.Base.Extensions.Encryption;
using ExpenseManagement.Data.DbContexts;
using Microsoft.Extensions.Configuration;

namespace ExpenseManagement.Data.DbSeed;

/// <summary>
/// Provides methods for seeding initial data into the ExpenseManagement database.
/// </summary>
public static class DbSeedOperations
{
    /// <summary>
    /// Seeds the database with initial data, including default users, if they do not exist.
    /// </summary>
    /// <param name="dbContext">The DbContext for the ExpenseManagement database.</param>
    /// <param name="configuration">The configuration containing necessary settings.</param>
    public static void SeedDatabase(
        EfExpenseManagementDbContext dbContext, 
        ConfigurationManager configuration)
    {
        SeedUserIfNotExist(dbContext, configuration);
    }

    /// <summary>
    /// Seeds the database with default users if no users exist.
    /// </summary>
    /// <param name="dbContext">The DbContext for the ExpenseManagement database.</param>
    /// <param name="configuration">The configuration containing necessary settings.</param>
    private static void SeedUserIfNotExist(
        EfExpenseManagementDbContext dbContext, 
        IConfiguration configuration)
    {
        if(!dbContext.AppUsers!.Any())
        {
            var AppUsers = new List<AppUser> {
                new()
                {
                    UserName = DefaultUsers.DefaultAdminUserName,
                    IdentityNumber = DefaultUsers.DefaultAdminIdentityNumber,
                    IBAN = DefaultUsers.DefaultAdminIBAN,
                    Role = UserRole.Admin,
                    Email = DefaultUsers.DefaultAdminEmail,
                    Phone = DefaultUsers.DefaultAdminPhone,
                    FirstName = DefaultUsers.DefaultAdminFirstName,
                    LastName = DefaultUsers.DefaultAdminLastName,
                    InsertUserId = 1,
                    InsertDate = DateTime.Now,
                    PasswordRetryCount = 0,
                    Status = false,
                    Password = configuration[DefaultUsers.DefaultAdminPassword]!.GetSHA256Hash()
                },
                new()
                {
                    IdentityNumber = DefaultUsers.DefaultEmployeeIdentityNumber,
                    IBAN = DefaultUsers.DefaultEmployeeIBAN,
                    UserName = DefaultUsers.DefaultEmployeeUserName,
                    Role = UserRole.Employee,
                    Email = DefaultUsers.DefaultEmployeeEmail,
                    Phone = DefaultUsers.DefaultEmployeePhone,
                    FirstName = DefaultUsers.DefaultEmployeeFirstName,
                    LastName = DefaultUsers.DefaultEmployeeLastName,
                    InsertUserId = 1,
                    InsertDate = DateTime.Now,
                    PasswordRetryCount = 0,
                    Status = false,
                    Password = configuration[DefaultUsers.DefaultEmployeePassword]!.GetSHA256Hash()
                }
            };
            dbContext.AppUsers!.AddRange(AppUsers);
            dbContext.SaveChanges();
        }
    }
}