using AutoMapper;
using MediatR;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.Expenses;

namespace ExpenseManagement.Business.ExpenseCqrs.Commands;
/// Handles the command for user Update user from admin, mapping the request to an Expense entity,
/// checking for uniqueness, Updating a expense and returning the response message.
/// </summary>
public class UpdateExpenseCommandHandler :
    IRequestHandler<UpdateExpenseCommand, ApiResponse>
{
    private readonly IMapper mapper;
    private readonly IEfExpenseRepository expenseRepository;

    /// <summary>
    /// Initializes a new instance of the UpdateExpenseCommandHandler class.
    /// </summary>
    /// <param name="mapper">The AutoMapper instance for object mapping.</param>
    /// <param name="expenseRepository">The repository for interacting with Expense entities.</param>
    /// <param name="tokenGenerator">The JWT token generator for Updating authentication tokens.</param>
    public UpdateExpenseCommandHandler(
        IMapper mapper,
        IEfExpenseRepository expenseRepository)
    {
        this.mapper = mapper;
        this.expenseRepository = expenseRepository;
    }

    /// <summary>
    /// Handles the Update user from admin command, mapping the request to an Expense entity,
    /// checking for uniqueness, Updating a new user, generating a JWT token, and returning the response.
    /// </summary>
    /// <param name="request">The Update user from admin command request.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>An ApiResponse containing a TokenResponse with the generated token and user email.</returns>
    public async Task<ApiResponse> Handle(
        UpdateExpenseCommand request,
        CancellationToken cancellationToken)
    {

        var entityWillUpdate = await expenseRepository.GetExpenseByIdAsync(request.Model.Id, cancellationToken);
        if(entityWillUpdate is null)
            return new ApiResponse(ExceptionMessages.ExpenseHasNotUniqueId);

        entityWillUpdate.Amount = request.Model.Amount ?? entityWillUpdate.Amount;
        entityWillUpdate.Description = request.Model.Description ?? entityWillUpdate.Description;
        entityWillUpdate.Title = request.Model.Title ?? entityWillUpdate.Title;
        entityWillUpdate.ReceiptPhotoUrl = request.Model.ReceiptPhotoUrl ?? entityWillUpdate.ReceiptPhotoUrl;
        entityWillUpdate.UpdateUserId = request.Model.UserId;
        entityWillUpdate.UpdateDate = request.Model.RequestTimestamp;
        
        await expenseRepository.UpdateExpenseAsync(entityWillUpdate, cancellationToken);
        return new ApiResponse(SuccessMessages.UpdatedSuccess(entityWillUpdate.Id.ToString()));
    }
}