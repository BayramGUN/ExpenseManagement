using ExpenseManagement.Base.Schema;

namespace ExpenseManagement.Schema.AppUser.Responses;
public class CreatedAppUserResponse: BaseResponse
{
    public string? UserName { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    public DateTime LastActivityDate { get; set; }
    public int PasswordRetryCount { get; set; }
    public int Status { get; set; }
    public string? TemporaryPassword { get; set; }
}