

using ExpenseManagement.Base.Response;
using ExpenseManagement.Schema.Authentication.Requests;
using MediatR;

namespace ExpenseManagement.Business.AppUserOperations.Commands;

/// <summary>
/// Represents a command to update an application user by their UpdateUserRequest.
/// </summary>
public record UpdateAppUserCommand(UpdateAppUserRequest Model) : IRequest<ApiResponse>;

/// <summary>
/// Represents a command to delete an application user by their Ids.
/// </summary>
public record DeleteAppUserCommand(int Id) : IRequest<ApiResponse>;