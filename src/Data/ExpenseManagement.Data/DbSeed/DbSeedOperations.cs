using System.Diagnostics;
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
        ExpenseManagementDbContext dbContext, 
        ConfigurationManager configuration)
    {
        seedUserIFNotExist(dbContext, configuration);
    }

    /// <summary>
    /// Seeds the database with default users if no users exist.
    /// </summary>
    /// <param name="dbContext">The DbContext for the ExpenseManagement database.</param>
    /// <param name="configuration">The configuration containing necessary settings.</param>
    private static void seedUserIFNotExist(
        ExpenseManagementDbContext dbContext, 
        IConfiguration configuration)
    {
        if(!dbContext.AppUsers!.Any())
        {
            var AppUsers = new List<AppUser> {
                new()
                {
                    Id = 0,
                    UserName = DefaultUsers.DefaultAdminUserName,
                    IdentityNumber = "11111111111",
                    IBAN = "TR310006285458942714869531",
                    Role = UserRole.Admin,
                    Email = DefaultUsers.DefaultAdminEmail,
                    FirstName = DefaultUsers.DefaultAdminFirstName,
                    LastName = DefaultUsers.DefaultAdminLastName,
                    InsertUserId = 1,
                    InsertDate = DateTime.Now,
                    PasswordRetryCount = 0,
                    Status = true,
                    Password = configuration[DefaultUsers.DefaultAdminPassword]!.GetSHA256Hash()
                },
                new()
                {
                    Id = 0,
                    IdentityNumber = "11111111102",
                    IBAN = "TR660006241864652728546171",
                    UserName = DefaultUsers.DefaultEmployeeUserName,
                    Role = UserRole.Employee,
                    Email = DefaultUsers.DefaultEmployeeEmail,
                    FirstName = DefaultUsers.DefaultEmployeeFirstName,
                    LastName = DefaultUsers.DefaultEmployeeLastName,
                    InsertUserId = 1,
                    InsertDate = DateTime.Now,
                    PasswordRetryCount = 0,
                    Status = true,
                    Password = configuration[DefaultUsers.DefaultEmployeePassword]!.GetSHA256Hash()
                }
            };
            dbContext.AppUsers!.AddRange(AppUsers);
            dbContext.SaveChanges();
        }
    }
}