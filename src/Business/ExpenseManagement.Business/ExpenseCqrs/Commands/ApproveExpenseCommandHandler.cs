using AutoMapper;
using MediatR;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.Expenses;
using ExpenseManagement.Business.Common;
using ExpenseManagement.Base.Enums;
using ExpenseManagement.Base.Constants.Authentication;
using System.Text;
using ExpenseManagement.Data.Entities;
using ExpenseApprovalManagement.Data.Repositories.Interfaces.Expenses;
using System.Data.Entity.Core.Objects.DataClasses;
using ExpenseManagement.Business.Common.Interfaces.EventBus;
using ExpenseManagement.Business.ExpenseCqrs.Events;
using ExpenseManagement.Base.Constants.Company;

namespace ExpenseManagement.Business.ExpenseCqrs.Commands;

/// Handles the command for approve expense, mapping the request to an Expense entity,
/// checking for uniqueness, creating a new expense, generating a JWT token, and returning the response.
/// </summary>
public class ApproveExpenseCommandHandler :
    IRequestHandler<ApproveExpenseCommand, ApiResponse>
{

    
    private readonly IMapper mapper;
    private readonly IEventBus eventBus;
    private readonly IEfExpenseRepository expenseRepository;
    private readonly IEfExpenseApprovalRepository expenseApprovalRepository;
    /// <summary>
    /// Initializes a new instance of the ApproveExpenseCommandHandler class.
    /// </summary>
    /// <param name="mapper">The AutoMapper instance for object mapping.</param>
    /// <param name="expenseRepository">The repository for interacting with Expense entities.</param>
    /// <param name="tokenGenerator">The JWT token generator for creating authentication tokens.</param>
    /// <param name="eventBus">The event bus for creating event bus to add queue.</param>
    public ApproveExpenseCommandHandler(
        IEfExpenseRepository expenseRepository,
        IEfExpenseApprovalRepository expenseApprovalRepository,
        IMapper mapper,
        IEventBus eventBus)
    {
        this.expenseRepository = expenseRepository;
        this.expenseApprovalRepository = expenseApprovalRepository;
        this.mapper = mapper;
        this.eventBus = eventBus;
    }

    /// <summary>
    /// Handles the Approve expense from admin command, mapping the request to an Expense entity,
    /// checking for uniqueness, creating a new expense, generating a JWT token, and returning the response.
    /// </summary>
    /// <param name="request">The Approve expense from admin command request.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>An ApiResponse containing a TokenResponse with the generated token and expense email.</returns>
    public async Task<ApiResponse> Handle(
        ApproveExpenseCommand request,
        CancellationToken cancellationToken)
    {
        var expense = await expenseRepository.GetExpenseById(request.Model.ExpenseId, cancellationToken);
        if(expense is null)
            return new ApiResponse(ExceptionMessages.NotFound(request.Model.ExpenseId));

        expense.Status = request.Model.Status;
        expense.UpdateUserId = request.Model.UserId;
        if(expense.Status is Status.Rejected)
            expense.Description = request.Model.Description;

        var result = await expenseRepository.UpdateExpenseAsync(expense, cancellationToken);

        if(result is null)
            return new ApiResponse(ExceptionMessages.InternalError);

        var expenseApproval = mapper.Map<ExpenseApproval>(request.Model);

        await expenseApprovalRepository.CreateExpenseApprovalAsync(expenseApproval, cancellationToken);
        if(expenseApproval.ApprovalStatus is Status.Approved)
            await PublishPaymentEvent(expense, cancellationToken);

            
        return new ApiResponse(SuccessMessages.UpdatedSuccess(expense.Title));
    }

    private async Task PublishPaymentEvent(Expense expense, CancellationToken cancellationToken)
    {
        await eventBus.PublishAsync(new PaymentEvent
            {
                FromAccountNumber = InformationStrings.CompanyIBAN,
                ToAccountNumber = expense.AppUser.IBAN,
                Amount = expense.Amount,
                Description = InformationStrings.PaymentDescription(expense.Title)
            }, cancellationToken);
    }
}