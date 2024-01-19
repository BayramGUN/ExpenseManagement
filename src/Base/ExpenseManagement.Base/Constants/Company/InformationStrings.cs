namespace ExpenseManagement.Base.Constants.Company;

public class InformationStrings
{
    public const string CompanyIBAN = "TR740006265889164425369911";
    public static readonly Func<string, string> PaymentDescription = (expenseTitle) 
        => $"Payment for {expenseTitle} expense!";
}