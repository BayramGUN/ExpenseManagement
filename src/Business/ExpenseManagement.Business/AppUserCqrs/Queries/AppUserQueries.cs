using ExpenseManagement.Base.Response;
using ExpenseManagement.Schema.AppUser.Requests;
using ExpenseManagement.Schema.AppUser.Responses;
using MediatR;

/// <summary>
/// Represents a query to retrieve an application user by specified parameters.
/// </summary>
public record GetAppUserByParameterQuery(GetUserByParameterRequest Model) 
    : IRequest<ApiResponse<AppUserResponse>>;

/// <summary>
/// Represents a query to retrieve all application users.
/// </summary>
public record GetAllAppUsersQuery() 
    : IRequest<ApiResponse<List<AppUserResponse>>>;

/// <summary>
/// Represents a query to retrieve application users based on specified parameters.
/// </summary>
public record GetAppUsersByParameterQuery(GetUsersByParameterRequest Model) 
    : IRequest<ApiResponse<List<AppUserResponse>>>;
