using ExpenseManagement.Base.Schema;

namespace ExpenseManagement.Schema.AppUser.Requests;

public class UpdateAppUserRequest : BaseRequest
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public int? AccountNumber { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}
