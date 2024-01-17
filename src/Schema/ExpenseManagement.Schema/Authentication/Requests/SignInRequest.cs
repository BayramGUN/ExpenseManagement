using ExpenseManagement.Base.Schema;

namespace ExpenseManagement.Schema.Authentication.Requests;

public class SignInRequest : BaseRequest
{
    public string? UserName { get; set; }
    public string? Phone { get; set; }
    public string? IdentityNumber { get; set; }
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
}
