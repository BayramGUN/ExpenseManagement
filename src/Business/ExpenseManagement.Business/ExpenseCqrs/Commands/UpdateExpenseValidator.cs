using FluentValidation;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Business.Authentication.Extensions;
using ExpenseManagement.Schema.Expense.Requests;
using System.Data;


namespace ExpenseManagement.Business.ExpenseCqrs.Commands;

/// <summary>
/// Validator for the UpdateExpenseRequest, providing rules for validating properties such as email, phone, first name,
/// last name, identity number, IBAN, and user name.
/// </summary>
public class UpdateExpenseValidator : AbstractValidator<UpdateExpenseRequest>
{
    // Validation rules for the properties of UpdateExpenseRequest go here
    public UpdateExpenseValidator()
    {
        RuleFor(x => x.Description).NotNull().NotEmpty()
            .MinimumLength(5)
            .MaximumLength(120);
        
        RuleFor(x => x.Title).NotNull().NotEmpty()
            .MinimumLength(3)
            .MaximumLength(25);

        RuleFor(x => x.Amount).NotNull().NotEmpty()
            .GreaterThan(0)
            .ScalePrecision(2, 18, true);

        RuleFor(x => x.UserId).NotNull().NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);
    }
}