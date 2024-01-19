using ExpenseManagement.Base.Constants.Authorization;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Base.Routes;
using ExpenseManagement.Business.AppUserCqrs.Commands;
using ExpenseManagement.Business.AppUserCqrs.Queries;
using ExpenseManagement.Schema.AppUser.Requests;
using ExpenseManagement.Schema.AppUser.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Api.Controllers;


[Authorize(Roles = RoleStrings.AdminRole)]
[Route(ControllerRoute.BaseRoute)]
[ApiController]
public class AppUserController : ControllerBase
{  
    private readonly IMediator mediator;

    public AppUserController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<ApiResponse<List<AppUserResponse>>> GetAll()
    {
        var operation = new GetAllAppUsersQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet(EndpointRoute.GetAppUserBy)]
    public async Task<ApiResponse<AppUserResponse>> GetUserByAnyParameter(
        [FromQuery] GetUserByParameterRequest request)
    {
        var operation = new GetAppUserByParameterQuery(request);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet(EndpointRoute.GetBy)]
    public async Task<ApiResponse<List<AppUserResponse>>> GetUsersByAnyParameter(
        [FromQuery] GetUsersByParameterRequest request)
    {
        var operation = new GetAppUsersByParameterQuery(request);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet(EndpointRoute.GetAppUserById)]
    public async Task<ApiResponse<AppUserResponse>> GetUserById(
        [FromRoute] int id)
    {
        var operation = new GetAppUserByParameterQuery(new GetUserByParameterRequest
        {
            Id = id
        });
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete(EndpointRoute.Delete)]
    public async Task<ApiResponse> DeleteAppUser([FromRoute] int id)
    {
        var operation = new DeleteAppUserCommand(Id: id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut(EndpointRoute.Update)]
    public async Task<ApiResponse> UpdateAppUser([FromBody] UpdateAppUserRequest request)
    {
        var operation = new UpdateAppUserCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost(EndpointRoute.Create)]
    public async Task<ApiResponse<CreatedAppUserResponse>> SignUp(
        [FromBody] CreateAppUserRequest request)
    {
        var operation = new CreateAppUserCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
}