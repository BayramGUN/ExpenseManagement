namespace ExpenseManagement.Schema.Authentication.Requests;

public class SignInRequest
{
    public string? UserName { get; set; }
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
}
