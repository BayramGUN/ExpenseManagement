using FluentValidation;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Business.Authentication.Extensions;
using ExpenseManagement.Base.Constants.Regex;
using Microsoft.IdentityModel.Tokens;
using ExpenseManagement.Schema.AppUser.Requests;


namespace ExpenseManagement.Business.AppUserCqrs.Commands;

/// <summary>
/// Validator for the UpdateAppUserRequest, providing rules for validating properties such as email, phone, IBAN, and user name
/// during the update of an application user.
/// </summary>
public class UpdateAppUserValidator : AbstractValidator<UpdateAppUserRequest>
{
    // Validation rules for the properties of UpdateAppUserRequest go here
    public UpdateAppUserValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage((model, email) => ValidationMessages.InvalidEmail(email));

        RuleFor(x => x.Phone)
            .Must((model, phone) => phone.IsNullOrEmpty() || phone!.IsValidPhone())
            .WithMessage((model, phone) => ValidationMessages.InvalidPhoneNumber(phone!));
        
        RuleFor(x => x.AccountNumber);

        RuleFor(x => x.UserName).MinimumLength(4).MaximumLength(20);

    }

}