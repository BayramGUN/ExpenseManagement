using ExpenseManagement.Base.Response;
using ExpenseManagement.Schema.AppUser.Requests;
using ExpenseManagement.Schema.AppUser.Responses;
using MediatR;

namespace ExpenseManagement.Business.AppUserOperations.Queries;

public record GetAppUserByParameterQuery(GetUserByParameterRequest Model) 
    : IRequest<ApiResponse<AppUserResponse>>;

public record GetAllAppUsersQuery() 
    : IRequest<ApiResponse<List<AppUserResponse>>>;
public record GetAppUsersByParameterQuery(GetUsersByParameterRequest Model) 
    : IRequest<ApiResponse<List<AppUserResponse>>>;