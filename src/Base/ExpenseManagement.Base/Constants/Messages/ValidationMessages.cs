namespace ExpenseManagement.Base.Constants.Messages;

public class ValidationMessages
{
    public static readonly Func<string, string> InvalidPhoneNumber 
        = (phoneNumber) => $"{phoneNumber} format is invalid!";
    public static readonly Func<string, string> InvalidEmail 
        = (email) => $"{email} format is invalid!";
    
    public const string PasswordMustHasLowerCase = "Your password must contain at least one lowercase letter.";
    public const string PasswordMustHasUpperCase = "Your password must contain at least one uppercase letter.";
    public const string PasswordMustHasDigit = "Your password must contain at least one digit.";
    public const string PasswordMustHasSymbol = "Your password must contain at least one (!? *.).";

}