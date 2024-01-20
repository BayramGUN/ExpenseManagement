using ExpenseManagement.Base.Constants.Authorization;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Base.Routes;
using ExpenseManagement.Business.ExpenseApprovalCqrs.Commands;
using ExpenseManagement.Business.ExpenseApprovalCqrs.Queries;
using ExpenseManagement.Business.ExpenseCqrs.Commands;
using ExpenseManagement.Business.ExpenseCqrs.Queries;
using ExpenseManagement.Schema.Expense.Requests;
using ExpenseManagement.Schema.Expense.Responses;
using ExpenseManagement.Schema.ExpenseApproval.Requests;
using ExpenseManagement.Schema.ExpenseApproval.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Api.Controllers;

[Route(ControllerRoute.BaseRoute)]
[ApiController]
public class ExpenseApprovalController : ControllerBase
{  
    private readonly IMediator mediator;

    public ExpenseApprovalController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<ApiResponse<List<ExpenseApprovalResponse>>> GetAll()
    {
        var operation = new GetAllExpenseApprovalsQuery();
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet(EndpointRoute.GetByApprover)]
    public async Task<ApiResponse<List<ExpenseApprovalResponse>>> GetByApproverId([FromQuery] int approverId)
    {
        var operation = new GetExpenseApprovalsByApproverIdQuery(approverId);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet(EndpointRoute.GetBy)]
    public async Task<ApiResponse<List<ExpenseApprovalResponse>>> GetByParameter([FromQuery] GetExpenseApprovalsByParameterRequest parameters)
    {
        var operation = new GetExpenseApprovalsByParameterQuery(parameters);
        var result = await mediator.Send(operation);
        return result;
    }
    [HttpGet(EndpointRoute.GetExpenseApprovalById)]
    public async Task<ApiResponse<ExpenseApprovalResponse>> GetExpenseApprovalById([FromRoute] int id)
    {
        var operation = new GetExpenseApprovalByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }


    [Authorize(Roles = RoleStrings.Both)]
    [HttpPost(EndpointRoute.Create)]
    public async Task<ApiResponse> CreateExpenseApproval([FromBody] CreateExpenseApprovalRequest request)
    {
        var operation = new CreateExpenseApprovalCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [Authorize(Roles = RoleStrings.Both)]
    [HttpDelete(EndpointRoute.Delete)]
    public async Task<ApiResponse> DeleteExpenseApproval([FromRoute] int id)
    {
        var operation = new DeleteExpenseApprovalCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut(EndpointRoute.Update)]
    public async Task<ApiResponse> UpdateExpenseApproval([FromBody] UpdateExpenseApprovalRequest request)
    {
        var operation = new UpdateExpenseApprovalCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
}