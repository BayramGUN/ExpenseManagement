using FluentValidation;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Business.Authentication.Extensions;
using ExpenseManagement.Schema.AppUser.Requests;


namespace ExpenseManagement.Business.AppUserCqrs.Commands;

/// <summary>
/// Validator for the CreateAppUserRequest, providing rules for validating properties such as email, phone, first name,
/// last name, identity number, IBAN, and user name.
/// </summary>
public class CreateAppUserValidator : AbstractValidator<CreateAppUserRequest>
{
    // Validation rules for the properties of CreateAppUserRequest go here
    public CreateAppUserValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty()
            .EmailAddress()
            .WithMessage((model, email) => ValidationMessages.InvalidEmail(email));

        RuleFor(x => x.Phone).NotNull().NotEmpty()
            .Must((model, phone) => phone.IsValidPhone())
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
        
        RuleFor(x => x.UserName).MinimumLength(4).MaximumLength(20);
    }
}