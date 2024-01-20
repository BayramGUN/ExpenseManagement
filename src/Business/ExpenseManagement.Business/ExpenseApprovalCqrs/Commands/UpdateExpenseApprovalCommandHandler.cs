using AutoMapper;
using MediatR;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseApprovalManagement.Data.Repositories.Interfaces.Expenses;
using ExpenseManagement.Business.Configurations.Extensions;

namespace ExpenseManagement.Business.ExpenseApprovalCqrs.Commands;
/// Handles the command for user Update user from admin, mapping the request to an ExpenseApproval entity,
/// checking for uniqueness, Updating a expenseApproval and returning the response message.
/// </summary>
public class UpdateExpenseApprovalCommandHandler :
    IRequestHandler<UpdateExpenseApprovalCommand, ApiResponse>
{
    private readonly IMapper mapper;
    private readonly IEfExpenseApprovalRepository expenseApprovalRepository;

    /// <summary>
    /// Initializes a new instance of the UpdateExpenseApprovalCommandHandler class.
    /// </summary>
    /// <param name="mapper">The AutoMapper instance for object mapping.</param>
    /// <param name="expenseApprovalRepository">The repository for interacting with ExpenseApproval entities.</param>
    /// <param name="tokenGenerator">The JWT token generator for Updating authentication tokens.</param>
    public UpdateExpenseApprovalCommandHandler(
        IMapper mapper,
        IEfExpenseApprovalRepository expenseApprovalRepository)
    {
        this.mapper = mapper;
        this.expenseApprovalRepository = expenseApprovalRepository;
    }

    /// <summary>
    /// Handles the Update user from admin command, mapping the request to an ExpenseApproval entity,
    /// checking for uniqueness, Updating a new user, generating a JWT token, and returning the response.
    /// </summary>
    /// <param name="request">The Update user from admin command request.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>An ApiResponse containing a TokenResponse with the generated token and user email.</returns>
    public async Task<ApiResponse> Handle(
        UpdateExpenseApprovalCommand request,
        CancellationToken cancellationToken)
    {

        var entityWillUpdate = await expenseApprovalRepository.GetExpenseApprovalByIdAsync(
                                request.Model.Id, cancellationToken);
                                
        if(entityWillUpdate is null)
            return new ApiResponse(ExceptionMessages.ExpenseApprovalHasNotUniqueId);

        entityWillUpdate.ApprovalStatus = request.Model.ApprovalStatus ?? entityWillUpdate.ApprovalStatus;
        entityWillUpdate.Description = request.Model.Description ?? entityWillUpdate.Description;
        entityWillUpdate.UpdateUserId = request.Model.UserId;
        entityWillUpdate.UpdateDate = request.Model.RequestTimestamp;
        
        await expenseApprovalRepository.UpdateExpenseApprovalAsync(entityWillUpdate, cancellationToken);
        return new ApiResponse(SuccessMessages.UpdatedSuccess(entityWillUpdate.Id.ToString()));
    }
}