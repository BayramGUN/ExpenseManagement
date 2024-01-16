namespace ExpenseManagement.Schema.Authentication.Requests;

public class SignUpRequest
{
    
    public string? UserName { get; set; }
    public string Password { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public UserRole Role { get; set; }
}

public enum UserRole
{
    Admin,
    Employee
}