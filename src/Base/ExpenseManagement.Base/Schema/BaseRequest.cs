namespace ExpenseManagement.Base.Schema;

public abstract class BaseRequest
{
    public string? AccessToken { get; set; }
    public int UserId { get; set; }
    public DateTime RequestTimestamp { get; set; }
}