using ExpenseManagement.Base.Response;
using ExpenseManagement.Schema.AppUser.Requests;
using ExpenseManagement.Schema.AppUser.Responses;
using MediatR;

namespace ExpenseManagement.Business.AppUserOperations.Commands;

/// <summary>
/// Represents a command to create an application user by their UpdateUserRequest.
/// </summary>
public record CreateAppUserCommand(CreateAppUserRequest Model) : IRequest<ApiResponse<CreatedAppUserResponse>>;

/// <summary>
/// Represents a command to update an application user by their UpdateUserRequest.
/// </summary>
public record UpdateAppUserCommand(UpdateAppUserRequest Model) : IRequest<ApiResponse>;

/// <summary>
/// Represents a command to delete an application user by their Ids.
/// </summary>
public record DeleteAppUserCommand(int Id) : IRequest<ApiResponse>;