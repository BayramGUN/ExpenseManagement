using FluentValidation;
using ExpenseManagement.Schema.Authentication.Requests;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Business.Authentication.Extensions;


namespace ExpenseManagement.Business.Authentication.Commands.SignIn;

public class SignInValidator : AbstractValidator<SignInRequest>
{
    public SignInValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty()
            .EmailAddress()
            .WithMessage((model, email) => ValidationMessages.InvalidEmail(email));

        RuleFor(x => x.Phone)
            .Must((model, phone) => (phone is not null) ? phone!.IsValidPhone() : true)
            .WithMessage((model, phone) => ValidationMessages.InvalidPhoneNumber(phone!));


        RuleFor(x => x.Password).NotNull().NotEmpty()
            .MinimumLength(8)
            .MaximumLength(30);

    }

}