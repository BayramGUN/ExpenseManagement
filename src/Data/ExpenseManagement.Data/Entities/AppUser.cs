using ExpenseManagement.Base.Entity;
using ExpenseManagement.Base.Enums;

public class AppUser : BaseEntityWithId
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required UserRole Role { get; set;}
    public DateTime LastActivityDate { get; set; }
    public required int PasswordRetryCount { get; set; }
    public required bool Status { get; set; }
}