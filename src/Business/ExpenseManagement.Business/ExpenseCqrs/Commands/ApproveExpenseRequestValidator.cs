using FluentValidation;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Business.Authentication.Extensions;
using ExpenseManagement.Schema.Expense.Requests;
using System.Data;


namespace ExpenseManagement.Business.ExpenseCqrs.Commands;

/// <summary>
/// Validator for the ApproveExpenseRequest, providing rules for validating properties such as email, phone, first name,
/// </summary>
public class ApproveExpenseValidator : AbstractValidator<ApproveExpenseRequest>
{
    // Validation rules for the properties of ApproveExpenseRequest go here
    public ApproveExpenseValidator()
    {
        RuleFor(x => x.Description).NotNull().NotEmpty()
            .MinimumLength(5)
            .MaximumLength(120);

        RuleFor(x => x.UserId).NotNull().NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);

        RuleFor(x => x.ExpenseId).NotNull().NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);

        RuleFor(x => x.ExpenseId).NotNull().NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);
    }
}