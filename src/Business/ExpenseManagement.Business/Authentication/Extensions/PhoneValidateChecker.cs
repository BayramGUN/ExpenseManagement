using System.Text.RegularExpressions;
using ExpenseManagement.Base.Constants.Regex;

namespace ExpenseManagement.Business.Authentication.Extensions;

public static class PhoneValidateChecker
{
    public static bool IsValidPhone(this string Phone)
    {
        try
        {
            if (string.IsNullOrEmpty(Phone))
                return false;
            var regex = new Regex(RegexStrings.PhoneRegex);
            return regex.IsMatch(Phone);
        }
        catch (ArgumentNullException exception)
        {
            throw new ArgumentNullException(exception.Message);
        }
        catch (RegexMatchTimeoutException exception)
        {
            throw new RegexMatchTimeoutException(exception.Message);
        }
    }
}