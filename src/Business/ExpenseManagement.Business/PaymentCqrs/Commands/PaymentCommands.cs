using ExpenseManagement.Base.Response;
using ExpenseManagement.Schema.Expense.Requests;
using ExpenseManagement.Schema.Payment.Requests;
using MediatR;

namespace ExpenseManagement.Business.PaymentCqrs.Commands;

public record CreatePaymentCommand(CreatePaymentRequest Model) 
    : IRequest<ApiResponse>;
public record UpdatePaymentCommand(UpdatePaymentRequest Model) 
    : IRequest<ApiResponse>;
public record DeletePaymentCommand(int Id) 
    : IRequest<ApiResponse>;