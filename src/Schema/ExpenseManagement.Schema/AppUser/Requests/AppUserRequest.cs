using ExpenseManagement.Base.Schema;

namespace ExpenseManagement.Schema.AppUser.Requests;

public class GetUserByParameterRequest
{
    public int? Id { get; set; }
    public string? UserName { get; set; }
    public string? Phone { get; set; }
    public string? IdentityNumber { get; set; }
    public string? Email { get; set; } = null!;
}