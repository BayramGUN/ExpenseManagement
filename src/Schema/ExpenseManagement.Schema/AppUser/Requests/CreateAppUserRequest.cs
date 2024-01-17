using ExpenseManagement.Base.Schema;

namespace ExpenseManagement.Schema.AppUser.Requests;

public class CreateAppUserRequest : BaseRequest
{
    public string? UserName { get; set; }
    public string IBAN { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string IdentityNumber { get; set; } = null!;
    public string Phone { get; set; } = null!;
}
