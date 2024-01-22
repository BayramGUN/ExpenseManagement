namespace ExpenseManagement.Base.Constants.Caching;

public abstract class RedisConnectionKeys : RedisKeys
{
    public const string RedisConnectionUrl = "localhost:6379";
    
}

public class RedisKeys
{
    public const string EmployeeExpenseReportKey = "expenseReports";
    public const string ApprovalStatusReportKey = "approvalStatusReports";
    public const string PaymentAndApprovedExpenseDifferenceKey = "paymentAndApprovedDifference";
    
}