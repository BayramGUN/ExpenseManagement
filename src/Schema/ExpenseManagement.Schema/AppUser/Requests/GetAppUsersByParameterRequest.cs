using ExpenseManagement.Base.Enums;

namespace ExpenseManagement.Schema.AppUser.Requests;

public class GetUsersByParameterRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public UserRole? Role { get; set; }
    public bool? IsActive { get; set; }
    public bool? Status { get; set; }
}