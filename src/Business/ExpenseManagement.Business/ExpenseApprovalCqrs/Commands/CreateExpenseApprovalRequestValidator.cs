using ExpenseManagement.Base.Enums;
using ExpenseManagement.Schema.ExpenseApproval.Requests;
using FluentValidation;


namespace ExpenseManagement.Business.ExpenseApprovalCqrs.Commands;

/// <summary>
/// Validator for the CreateExpenseApprovalRequest, providing rules for validating properties such as email, phone, first name,
/// last name, identity number, IBAN, and user name.
/// </summary>
public class CreateExpenseApprovalValidator : AbstractValidator<CreateExpenseApprovalRequest>
{
    // Validation rules for the properties of CreateExpenseApprovalRequest go here
    public CreateExpenseApprovalValidator()
    {
        RuleFor(x => x.Description).NotNull().NotEmpty()
            .MinimumLength(5)
            .MaximumLength(120);
    

        RuleFor(x => x.UserId).NotNull().NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);

        RuleFor(x => x.ApproverId).NotNull().NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);

        RuleFor(x => x.ExpenseId).NotNull().NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);

        RuleFor(x => x.ApprovalStatus).NotNull().NotEmpty()
            .Equals(Status.Approved)
            .Equals(Status.Rejected)
            .Equals(Status.Pending);
    }
}