using ExpenseManagement.Base.Schema;

namespace ExpenseManagement.Schema.Authentication.Requests;

public class UnBlockAppUserRequest
{
    public required string UserName { get; set; }
    public required string Phone { get; set; }
    public required string IdentityNumber { get; set; }
    public required string TemporaryPassword { get; set; } = null!;
    public required string NewPassword { get; set; } = null!;
    public required string Email { get; set; } = null!;
}
