using FluentValidation;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Business.Authentication.Extensions;
using ExpenseManagement.Schema.Expense.Requests;
using System.Data;
using ExpenseManagement.Schema.Payment.Requests;


namespace ExpenseManagement.Business.PaymentCqrs.Commands;

/// <summary>
/// Validator for the UpdatePaymentRequest, providing rules for validating properties such as email, phone, first name,
/// last name, identity number, IBAN, and user name.
/// </summary>
public class UpdatePaymentValidator : AbstractValidator<UpdatePaymentRequest>
{
    // Validation rules for the properties of UpdatePaymentRequest go here
    public UpdatePaymentValidator()
    {
        RuleFor(x => x.Amount).NotNull().NotEmpty()
            .GreaterThan(0)
            .ScalePrecision(2, 18, true);

        RuleFor(x => x.UserId).NotNull().NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);
            
        RuleFor(x => x.Id).NotNull().NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);
    }
}