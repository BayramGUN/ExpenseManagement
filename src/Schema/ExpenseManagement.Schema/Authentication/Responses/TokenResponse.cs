using ExpenseManagement.Base.Schema;

namespace ExpenseManagement.Schema.Authentication.Responses;

public class TokenResponse : BaseResponse
{
    public string Token { get; set; } = null!;
    public string Email { get; set; } = null!;
}