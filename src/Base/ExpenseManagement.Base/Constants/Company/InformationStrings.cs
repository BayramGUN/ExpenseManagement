namespace ExpenseManagement.Base.Constants.Company;

public class InformationStrings
{
    public const int CompanyAccountNumber = 22802097;
    public static readonly Func<string, string> PaymentDescription = (expenseTitle) 
        => $"Payment for {expenseTitle} expense!";
}