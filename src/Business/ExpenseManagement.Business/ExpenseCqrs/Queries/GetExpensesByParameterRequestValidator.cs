using FluentValidation;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Business.Authentication.Extensions;
using ExpenseManagement.Schema.Expense.Requests;
using System.Data;
using ExpenseManagement.Base.Enums;


namespace ExpenseManagement.Business.ExpenseCqrs.Commands;

/// <summary>
/// Validator for the GetExpensesByRequest, providing rules for validating properties such as email, phone, first name,
/// last name, identity number, IBAN, and user name.
/// </summary>
public class GetExpensesByParameterRequestValidator : 
    AbstractValidator<GetExpensesByParameterRequest>
{
    // Validation rules for the properties of GetExpensesByParameterRequest go here
    public GetExpensesByParameterRequestValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0).ScalePrecision(2, 18, true);

        RuleFor(x => x.FromExpensedDate).LessThanOrEqualTo(DateTime.Now);

        RuleFor(x => x.ToExpensedDate).LessThanOrEqualTo(DateTime.Now);
        
        RuleFor(x => x.ExpensedDate).LessThanOrEqualTo(DateTime.Now);
        RuleFor(x => x.Status)
            .Equals(Status.Approved)
            .Equals(Status.Rejected)
            .Equals(Status.Pending);
    }
}