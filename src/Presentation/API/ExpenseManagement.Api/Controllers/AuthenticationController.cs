using ExpenseManagement.Base.Response;
using ExpenseManagement.Base.Routes;
using ExpenseManagement.Business.Authentication.Commands;
using ExpenseManagement.Business.Authentication.Commands.SignUp;
using ExpenseManagement.Business.Authentication.Commands.UnBlockAppUser;
using ExpenseManagement.Schema.Authentication.Requests;
using ExpenseManagement.Schema.Authentication.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Api.Controllers;


[Route(ControllerRoute.BaseRoute)]
[ApiController]
public class AuthenticationController : ControllerBase
{  
   private readonly IMediator mediator;

    public AuthenticationController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost(EndpointRoute.SignUp)]
    public async Task<ApiResponse<TokenResponse>> SignUp([FromBody] SignUpRequest request)
    {
        var operation = new SignUpCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
    [HttpPost(EndpointRoute.SignIn)]
    public async Task<ApiResponse<TokenResponse>> SignIn([FromBody] SignInRequest request)
    {
        var operation = new SignInCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
    [HttpPost(EndpointRoute.UnBlockAppUser)]
    public async Task<ApiResponse<TokenResponse>> SignIn([FromBody] UnBlockAppUserRequest request)
    {
        var operation = new UnBlockAppUserCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
}