using ExpenseManagement.Base.Response;
using ExpenseManagement.Base.Routes;
using ExpenseManagement.Business.AppUserOperations.Commands;
using ExpenseManagement.Business.AppUserOperations.Queries;
using ExpenseManagement.Business.Authentication.Commands;
using ExpenseManagement.Business.Authentication.Commands.SignUp;
using ExpenseManagement.Schema.AppUser.Requests;
using ExpenseManagement.Schema.AppUser.Responses;
using ExpenseManagement.Schema.Authentication.Requests;
using ExpenseManagement.Schema.Authentication.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Api.Controllers;


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

    [HttpGet(EndpointRoute.GetAppUserById)]
    public async Task<ApiResponse<AppUserResponse>> GetUserById(
        [FromRoute] int id)
    {
        var operation = new GetAppUserByParameterQuery(new GetUserByParameterRequest
        {
            UserId = id
        });
        var result = await mediator.Send(operation);
        return result;
    }
    
    [Authorize(Roles = "Admin")]
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
    public async Task<ApiResponse<CreatedAppUserResponse>> SignUp([FromBody] CreateAppUserRequest request)
    {
        var operation = new CreateAppUserCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
}