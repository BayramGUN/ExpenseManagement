using FluentValidation;
using ExpenseManagement.Schema.Authentication.Requests;
using System.Text.RegularExpressions;
using System.Data;
using ExpenseManagement.Base.Constants.Regex;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Business.Authentication.Extensions;
using ExpenseManagement.Schema.AppUser.Requests;


namespace ExpenseManagement.Business.AppUserOperations.Commands;

public class CreateAppUserValidator : AbstractValidator<CreateAppUserRequest>
{
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