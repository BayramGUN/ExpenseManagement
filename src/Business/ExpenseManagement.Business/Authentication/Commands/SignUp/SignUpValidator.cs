using FluentValidation;
using ExpenseManagement.Schema.Authentication.Requests;
using System.Text.RegularExpressions;
using System.Data;
using ExpenseManagement.Base.Constants.Regex;
using ExpenseManagement.Base.Constants.Messages;

namespace ExpenseManagement.Business.Authentication.Commands.SignUp;

public class SignUpValidator : AbstractValidator<SignUpRequest>
{
    public SignUpValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty()
            .EmailAddress()
            .WithMessage((model, email) => ValidationMessages.InvalidEmail(email));

        RuleFor(x => x.Phone).NotNull().NotEmpty()
            .Must((model, phone) => IsValidPhone(phone))
            .WithMessage((model, phone) => ValidationMessages.InvalidPhoneNumber(phone));

        RuleFor(x => x.FirstName).NotNull().NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50);
            
        RuleFor(x => x.LastName).NotNull().NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50);
        
        RuleFor(x => x.IdentityNumber).NotNull().NotEmpty()
            .MaximumLength(11);
        
        RuleFor(x => x.IBAN).NotNull().NotEmpty()
            .MaximumLength(34);
        
        RuleFor(x => x.Password).NotNull().NotEmpty()
            .MinimumLength(8)
            .MaximumLength(16)
            .Matches(RegexStrings.LowerCasesRegex).WithMessage(ValidationMessages.PasswordMustHasLowerCase)
            .Matches(RegexStrings.UpperCasesRegex).WithMessage(ValidationMessages.PasswordMustHasUpperCase)
            .Matches(RegexStrings.DigitsRegex).WithMessage(ValidationMessages.PasswordMustHasDigit)
            .Matches(RegexStrings.SymbolsRegex).WithMessage(ValidationMessages.PasswordMustHasSymbol);

        RuleFor(x => x.UserName).MinimumLength(4).MaximumLength(20);
    }
    private bool IsValidPhone(string Phone)
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