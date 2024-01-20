using FluentValidation;
using ExpenseManagement.Schema.ExpenseApproval.Requests;


namespace ExpenseManagement.Business.ExpenseApprovalCqrs.Commands;

/// <summary>
/// Validator for the UpdateExpenseApprovalRequest, providing rules for validating properties such as email, phone, first name,
/// last name, identity number, IBAN, and user name.
/// </summary>
public class UpdateExpenseApprovalValidator : AbstractValidator<UpdateExpenseApprovalRequest>
{
    // Validation rules for the properties of UpdateExpenseApprovalRequest go here
    public UpdateExpenseApprovalValidator()
    {
        RuleFor(x => x.Description).NotNull().NotEmpty()
            .MinimumLength(5)
            .MaximumLength(120);

        RuleFor(x => x.UserId).NotNull().NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);
            
        RuleFor(x => x.Id).NotNull().NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);
    }
}