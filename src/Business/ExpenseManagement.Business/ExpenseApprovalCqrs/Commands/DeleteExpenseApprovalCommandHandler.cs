using AutoMapper;
using MediatR;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseApprovalManagement.Data.Repositories.Interfaces.Expenses;

namespace ExpenseManagement.Business.ExpenseApprovalCqrs.Commands;
/// Handles the command for user Delete user from admin, mapping the request to an ExpenseApproval entity,
/// checking for uniqueness, Deleting a expenseApproval and returning the response message.
/// </summary>
public class DeleteExpenseApprovalCommandHandler :
    IRequestHandler<DeleteExpenseApprovalCommand, ApiResponse>
{
    private readonly IMapper mapper;
    private readonly IEfExpenseApprovalRepository expenseApprovalRepository;

    /// <summary>
    /// Initializes a new instance of the DeleteExpenseApprovalCommandHandler class.
    /// </summary>
    /// <param name="mapper">The AutoMapper instance for object mapping.</param>
    /// <param name="expenseApprovalRepository">The repository for interacting with ExpenseApproval entities.</param>
    public DeleteExpenseApprovalCommandHandler(
        IMapper mapper,
        IEfExpenseApprovalRepository expenseApprovalRepository)
    {
        this.mapper = mapper;
        this.expenseApprovalRepository = expenseApprovalRepository;
    }

    /// <summary>
    /// Handles the Delete user from admin command, mapping the request to an ExpenseApproval entity,
    /// checking for uniqueness, Deleting a new user, generating a JWT token, and returning the response.
    /// </summary>
    /// <param name="request">The Delete user from admin command request.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>An ApiResponse containing a TokenResponse with the generated token and user email.</returns>
    public async Task<ApiResponse> Handle(
        DeleteExpenseApprovalCommand request,
        CancellationToken cancellationToken)
    {
        var entity = expenseApprovalRepository.GetExpenseApprovalByIdAsync(request.Id, cancellationToken);
        if(entity is null)
            return new ApiResponse(ExceptionMessages.NotFound(request.Id));

        await expenseApprovalRepository.DeleteExpenseApprovalAsync(request.Id, cancellationToken);
        return new ApiResponse(SuccessMessages.DeletedSuccess(request.Id));
    }
}