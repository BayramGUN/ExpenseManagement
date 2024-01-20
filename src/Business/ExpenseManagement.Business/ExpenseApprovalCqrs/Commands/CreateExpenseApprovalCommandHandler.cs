using AutoMapper;
using MediatR;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Entities;
using ExpenseApprovalManagement.Data.Repositories.Interfaces.Expenses;

namespace ExpenseManagement.Business.ExpenseApprovalCqrs.Commands;
/// Handles the command for user create user from admin, mapping the request to an ExpenseApproval entity,
/// checking for uniqueness, creating a new user, generating a JWT token, and returning the response.
/// </summary>
public class CreateExpenseApprovalCommandHandler :
    IRequestHandler<CreateExpenseApprovalCommand, ApiResponse>
{
    private readonly IMapper mapper;
    private readonly IEfExpenseApprovalRepository expenseApprovalRepository;

    /// <summary>
    /// Initializes a new instance of the CreateExpenseApprovalCommandHandler class.
    /// </summary>
    /// <param name="mapper">The AutoMapper instance for object mapping.</param>
    /// <param name="expenseApprovalRepository">The repository for interacting with ExpenseApproval entities.</param>
    /// <param name="tokenGenerator">The JWT token generator for creating authentication tokens.</param>
    public CreateExpenseApprovalCommandHandler(
        IMapper mapper,
        IEfExpenseApprovalRepository expenseApprovalRepository)
    {
        this.mapper = mapper;
        this.expenseApprovalRepository = expenseApprovalRepository;
    }

    /// <summary>
    /// Handles the create user from admin command, mapping the request to an ExpenseApproval entity,
    /// checking for uniqueness, creating a new user, generating a JWT token, and returning the response.
    /// </summary>
    /// <param name="request">The create user from admin command request.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>An ApiResponse containing a TokenResponse with the generated token and user email.</returns>
    public async Task<ApiResponse> Handle(
        CreateExpenseApprovalCommand request,
        CancellationToken cancellationToken)
    {
        var entity = mapper.Map<ExpenseApproval>(request.Model);
        var isUnique = await expenseApprovalRepository.IsUniqueAsync(entity, cancellationToken);
        if(!isUnique)
            return new ApiResponse(ExceptionMessages.ExpenseApprovalHasNotUniqueId);
        await expenseApprovalRepository.CreateExpenseApprovalAsync(entity, cancellationToken);
        return new ApiResponse(SuccessMessages.CreatedSuccess(entity.Id.ToString()));
    }
}