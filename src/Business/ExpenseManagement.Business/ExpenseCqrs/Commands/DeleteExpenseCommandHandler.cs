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
/// Handles the command for user Delete user from admin, mapping the request to an Expense entity,
/// checking for uniqueness, Deleting a expense and returning the response message.
/// </summary>
public class DeleteExpenseCommandHandler :
    IRequestHandler<DeleteExpenseCommand, ApiResponse>
{
    private readonly IEfExpenseRepository expenseRepository;

    /// <summary>
    /// Initializes a new instance of the DeleteExpenseCommandHandler class.
    /// </summary>
    /// <param name="expenseRepository">The repository for interacting with Expense entities.</param>
    public DeleteExpenseCommandHandler(IEfExpenseRepository expenseRepository)
    {
        this.expenseRepository = expenseRepository;
    }

    /// <summary>
    /// Handles the Delete user from admin command, mapping the request to an Expense entity,
    /// checking for uniqueness, Deleting a new user, generating a JWT token, and returning the response.
    /// </summary>
    /// <param name="request">The Delete user from admin command request.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>An ApiResponse containing a TokenResponse with the generated token and user email.</returns>
    public async Task<ApiResponse> Handle(
        DeleteExpenseCommand request,
        CancellationToken cancellationToken)
    {
        var entity = expenseRepository.GetExpenseByIdAsync(request.Id, cancellationToken);
        if(entity is null)
            return new ApiResponse(ExceptionMessages.NotFound(request.Id));

        await expenseRepository.DeleteExpenseAsync(request.Id, cancellationToken);
        return new ApiResponse(SuccessMessages.DeletedSuccess(request.Id));
    }
}