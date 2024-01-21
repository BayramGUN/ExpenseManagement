using ExpenseManagement.Base.Constants.Authorization;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Base.Routes;
using ExpenseManagement.Business.PaymentCqrs.Commands;
using ExpenseManagement.Business.PaymentCqrs.Queries;
using ExpenseManagement.Schema.Payment.Requests;
using ExpenseManagement.Schema.Payment.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Api.Controllers;

[Authorize]
[Route(ControllerRoute.BaseRoute)]
[ApiController]
public class PaymentController : ControllerBase
{  
    private readonly IMediator mediator;

    public PaymentController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<ApiResponse<List<PaymentResponse>>> GetAll()
    {
        var operation = new GetAllPaymentsQuery();
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet(EndpointRoute.GetPaymentByExpenseId)]
    public async Task<ApiResponse<PaymentResponse>> GetPaymentByExpenseId(
        [FromRoute] int id)
    {
        var operation = new GetPaymentByExpenseIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet(EndpointRoute.GetBy)]
    public async Task<ApiResponse<List<PaymentResponse>>> GetByParameter(
        [FromQuery] GetPaymentsByParameterRequest parameters)
    {
        var operation = new GetPaymentsByParameterQuery(parameters);
        var result = await mediator.Send(operation);
        return result;
    }
    [HttpGet(EndpointRoute.GetPaymentById)]
    public async Task<ApiResponse<PaymentResponse>> GetPaymentById([FromRoute] int id)
    {
        var operation = new GetPaymentByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }


    [Authorize(Roles = RoleStrings.AdminRole)]
    [HttpPost(EndpointRoute.Create)]
    public async Task<ApiResponse> CreatePayment([FromBody] CreatePaymentRequest request)
    {
        var operation = new CreatePaymentCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [Authorize(Roles = RoleStrings.AdminRole)]
    [HttpDelete(EndpointRoute.Delete)]
    public async Task<ApiResponse> DeletePayment([FromRoute] int id)
    {
        var operation = new DeletePaymentCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }


    [Authorize(Roles = RoleStrings.AdminRole)]
    [HttpPut(EndpointRoute.Update)]
    public async Task<ApiResponse> UpdatePayment([FromBody] UpdatePaymentRequest request)
    {
        var operation = new UpdatePaymentCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
}