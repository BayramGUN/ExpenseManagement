using FluentValidation;
using ExpenseManagement.Base.Enums;
using ExpenseManagement.Schema.Payment.Requests;


namespace ExpenseManagement.Business.PaymentCqrs.Commands;

/// <summary>
/// Validator for the GetPaymentsByRequest, providing rules for validating properties such as email, phone, first name,
/// last name, identity number, IBAN, and user name.
/// </summary>
public class GetPaymentsByParameterRequestValidator : 
    AbstractValidator<GetPaymentsByParameterRequest>
{
    // Validation rules for the properties of GetPaymentsByParameterRequest go here
    public GetPaymentsByParameterRequestValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0).ScalePrecision(2, 18, true);

        RuleFor(x => x.PaymentDate).LessThanOrEqualTo(DateTime.Now);
        
        RuleFor(x => x.PaymentMethod).MaximumLength(20);
        RuleFor(x => x.AppUserId).GreaterThan(0);
    }
}