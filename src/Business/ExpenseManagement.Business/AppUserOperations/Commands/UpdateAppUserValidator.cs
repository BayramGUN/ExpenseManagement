using FluentValidation;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Business.Authentication.Extensions;
using ExpenseManagement.Base.Constants.Regex;
using Microsoft.IdentityModel.Tokens;
using ExpenseManagement.Schema.AppUser.Requests;


namespace ExpenseManagement.Business.AppUserOperations.Commands;

public class UpdateAppUserValidator : AbstractValidator<UpdateAppUserRequest>
{
    public UpdateAppUserValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage((model, email) => ValidationMessages.InvalidEmail(email));

        RuleFor(x => x.Phone)
            .Must((model, phone) => phone.IsNullOrEmpty() || phone!.IsValidPhone())
            .WithMessage((model, phone) => ValidationMessages.InvalidPhoneNumber(phone!));
        
        RuleFor(x => x.IBAN)
            .MaximumLength(34);

        RuleFor(x => x.UserName).MinimumLength(4).MaximumLength(20);

    }

}