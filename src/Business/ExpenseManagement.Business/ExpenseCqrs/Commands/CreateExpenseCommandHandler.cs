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

namespace ExpenseManagement.Business.ExpenseCqrs.Commands;
/// Handles the command for user create user from admin, mapping the request to an Expense entity,
/// checking for uniqueness, creating a new user, generating a JWT token, and returning the response.
/// </summary>
public class CreateExpenseCommandHandler :
    IRequestHandler<CreateExpenseCommand, ApiResponse>
{
    private readonly IMapper mapper;
    private readonly IEfExpenseRepository expenseRepository;

    /// <summary>
    /// Initializes a new instance of the CreateExpenseCommandHandler class.
    /// </summary>
    /// <param name="mapper">The AutoMapper instance for object mapping.</param>
    /// <param name="expenseRepository">The repository for interacting with Expense entities.</param>
    /// <param name="tokenGenerator">The JWT token generator for creating authentication tokens.</param>
    public CreateExpenseCommandHandler(
        IMapper mapper,
        IEfExpenseRepository expenseRepository)
    {
        this.mapper = mapper;
        this.expenseRepository = expenseRepository;
    }

    /// <summary>
    /// Handles the create user from admin command, mapping the request to an Expense entity,
    /// checking for uniqueness, creating a new user, generating a JWT token, and returning the response.
    /// </summary>
    /// <param name="request">The create user from admin command request.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>An ApiResponse containing a TokenResponse with the generated token and user email.</returns>
    public async Task<ApiResponse> Handle(
        CreateExpenseCommand request,
        CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Expense>(request.Model);
        var isUnique = await expenseRepository.IsUniqueAsync(entity, cancellationToken);
        if(!isUnique)
            return new ApiResponse(ExceptionMessages.ExpenseHasNotUniqueId);
        await expenseRepository.CreateExpenseAsync(entity, cancellationToken);
        return new ApiResponse(SuccessMessages.CreatedSuccess(entity.Title));
    }
}