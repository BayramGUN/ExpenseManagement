using ExpenseManagement.Base.Response;
using ExpenseManagement.Schema.Payment.Requests;
using ExpenseManagement.Schema.Payment.Responses;
using MediatR;

namespace ExpenseManagement.Business.PaymentCqrs.Queries;

public record GetPaymentsByParameterQuery(GetPaymentsByParameterRequest Model) 
    : IRequest<ApiResponse<List<PaymentResponse>>>;
public record GetAllPaymentsQuery() 
    : IRequest<ApiResponse<List<PaymentResponse>>>;
public record GetPaymentByIdQuery(int Id) 
    : IRequest<ApiResponse<PaymentResponse>>;
public record GetPaymentByExpenseIdQuery(int ExpenseId) 
    : IRequest<ApiResponse<PaymentResponse>>;
