using AutoMapper;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.Expenses;
using ExpenseManagement.Data.Repositories.Interfaces.Payments;
using ExpenseManagement.Schema.Expense.Responses;
using ExpenseManagement.Schema.Payment.Responses;
using MediatR;

namespace ExpenseManagement.Business.PaymentCqrs.Queries;

public class GetAllPaymentsQueryHandler : 
    IRequestHandler<GetAllPaymentsQuery, ApiResponse<List<PaymentResponse>>>
{
    private readonly IEfPaymentRepository paymentRepository;
    private readonly IMapper mapper;

    public GetAllPaymentsQueryHandler(
        IEfPaymentRepository paymentRepository, 
        IMapper mapper)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<PaymentResponse>>> Handle(
        GetAllPaymentsQuery request,
        CancellationToken cancellationToken)
    {
        var Payments = await paymentRepository.GetAllPaymentsAsync(cancellationToken);
        return new ApiResponse<List<PaymentResponse>>(mapper.Map<List<PaymentResponse>>(Payments));
    }
}
