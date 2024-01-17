
namespace ExpenseManagement.Base.Constants.Regex;

public class RegexStrings
{
    public const string PhoneRegex = @"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$";
    public const string UpperCasesRegex = @"[A-Z]+";
    public const string LowerCasesRegex = @"[a-z]+";
    public const string DigitsRegex = @"[0-9]+";
    public const string SymbolsRegex = @"[\!\?\*\.]+";
    
}
