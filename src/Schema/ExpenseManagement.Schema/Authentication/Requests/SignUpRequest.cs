namespace ExpenseManagement.Schema.Authentication.Requests;

public class SignUpRequest
{
    
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public string? FirstName { get; set; } = null!;
    public string? LastName { get; set; } = null!;
    public required string Email { get; set; }
    public UserRole Role { get; set;}
    public DateTime LastActivityDate { get; set; }
}

public enum UserRole
{
    Admin,
    Employee
}