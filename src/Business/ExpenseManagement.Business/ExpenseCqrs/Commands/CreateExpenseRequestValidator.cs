using FluentValidation;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Business.Authentication.Extensions;
using ExpenseManagement.Schema.Expense.Requests;
using System.Data;


namespace ExpenseManagement.Business.ExpenseCqrs.Commands;

/// <summary>
/// Validator for the CreateExpenseRequest, providing rules for validating properties...
/// </summary>
public class CreateExpenseValidator : AbstractValidator<CreateExpenseRequest>
{
    // Validation rules for the properties of CreateExpenseRequest go here
    public CreateExpenseValidator()
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