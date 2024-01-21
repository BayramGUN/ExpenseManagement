using ExpenseManagement.Base.Constants.Authorization;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Base.Routes;
using ExpenseManagement.Business.ExpenseCqrs.Commands;
using ExpenseManagement.Business.ExpenseCqrs.Queries;
using ExpenseManagement.Schema.Expense.Requests;
using ExpenseManagement.Schema.Expense.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Api.Controllers;

[Authorize(Roles = RoleStrings.Both)]
[Route(ControllerRoute.BaseRoute)]
[ApiController]
public class ExpenseController : ControllerBase
{  
    private readonly IMediator mediator;

    public ExpenseController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<ApiResponse<List<ExpenseResponse>>> GetAll()
    {
        var operation = new GetAllExpensesQuery();
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet(EndpointRoute.GetByAppUser)]
    public async Task<ApiResponse<List<ExpenseResponse>>> GetByAppUserId([FromQuery] int appUserId)
    {
        var operation = new GetExpensesByAppUserIdQuery(appUserId);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet(EndpointRoute.GetBy)]
    public async Task<ApiResponse<List<ExpenseResponse>>> GetByParameter([FromQuery] GetExpensesByParameterRequest parameters)
    {
        var operation = new GetExpensesByParameterQuery(parameters);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet(EndpointRoute.GetExpenseById)]
    public async Task<ApiResponse<ExpenseResponse>> GetExpenseById([FromRoute] int id)
    {
        var operation = new GetExpenseByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }


    [HttpPost(EndpointRoute.Create)]
    public async Task<ApiResponse> CreateExpense([FromBody] CreateExpenseRequest request)
    {
        var operation = new CreateExpenseCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete(EndpointRoute.Delete)]
    public async Task<ApiResponse> DeleteExpense([FromRoute] int id)
    {
        var operation = new DeleteExpenseCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [Authorize(Roles = RoleStrings.AdminRole)]
    [HttpPatch(EndpointRoute.Approve)]
    public async Task<ApiResponse> ApproveExpense([FromBody] ApproveExpenseRequest request)
    {
        var operation = new ApproveExpenseCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }

    [Authorize(Roles = RoleStrings.AdminRole)]
    [HttpPut(EndpointRoute.Update)]
    public async Task<ApiResponse> UpdateExpense([FromBody] UpdateExpenseRequest request)
    {
        var operation = new UpdateExpenseCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
}