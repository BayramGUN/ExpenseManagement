namespace ExpenseManagement.Base.Constants.DataBase.Dapper.ExpenseQueries;

public class ExpenseReportsQuerySQLStrings
{
    public const string GetEmployeeExpenseReport = "SELECT * FROM EmployeeExpenseReport WHERE AppUserId = @AppUserId";
    public const string GetApprovalStatusReport = "GetApprovalStatusReport";
    public const string GetPaymentAndApprovedExpenseDifference = "GetPaymentAndApprovedExpenseDifference";
    
}