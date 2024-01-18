using FluentValidation;
using ExpenseManagement.Schema.Authentication.Requests;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Business.Authentication.Extensions;
using ExpenseManagement.Base.Constants.Regex;


namespace ExpenseManagement.Business.Authentication.Commands;

public class UnBlockAppUserValidator : AbstractValidator<UnBlockAppUserRequest>
{
    public UnBlockAppUserValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty()
            .EmailAddress()
            .WithMessage((model, email) => ValidationMessages.InvalidEmail(email));

        RuleFor(x => x.Phone)
            .Must((model, phone) => (phone is not null) ? phone!.IsValidPhone() : true)
            .WithMessage((model, phone) => ValidationMessages.InvalidPhoneNumber(phone!));


        RuleFor(x => x.TemporaryPassword).NotNull().NotEmpty()
            .MinimumLength(8)
            .MaximumLength(16);

        RuleFor(x => x.NewPassword).NotNull().NotEmpty()
            .MinimumLength(8)
            .MaximumLength(16)
            .Matches(RegexStrings.LowerCasesRegex).WithMessage(ValidationMessages.PasswordMustHasLowerCase)
            .Matches(RegexStrings.UpperCasesRegex).WithMessage(ValidationMessages.PasswordMustHasUpperCase)
            .Matches(RegexStrings.DigitsRegex).WithMessage(ValidationMessages.PasswordMustHasDigit)
            .Matches(RegexStrings.SymbolsRegex).WithMessage(ValidationMessages.PasswordMustHasSymbol);
    }

}