namespace ExpenseManagement.Schema.Authentication.Requests;

public class UpdateAppUserRequest 
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; } = null!;
    public string? IBAN { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public string? Phone { get; set; } = null!;
}
