using AutoMapper;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.Payments;
using ExpenseManagement.Schema.Payment.Responses;
using MediatR;

namespace ExpenseManagement.Business.PaymentCqrs.Queries;

/// <summary>
/// Handles the execution of the GetPaymentByExpenseIdQuery, retrieving an Payment by its unique Expenseidentifier.
/// </summary>
public class GetPaymentByExpenseIdQueryHandler : 
    IRequestHandler<GetPaymentByExpenseIdQuery, ApiResponse<PaymentResponse>>
{
    private readonly IEfPaymentRepository paymentRepository;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetPaymentsByExpenseIdQueryHandler"/> class.
    /// </summary>
    /// <param name="paymentRepository">The repository for interacting with Payment entities.</param>
    /// <param name="mapper">The AutoMapper instance for mapping entities to response models.</param>
    public GetPaymentByExpenseIdQueryHandler(
        IEfPaymentRepository paymentRepository, 
        IMapper mapper)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Handles the execution of the query, retrieving an Payment by its unique Expense identifier.
    /// </summary>
    /// <param name="request">The GetPaymentByExpenseIdQuery instance.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>A response containing the Payment response model or an error message if not found.</returns>
    public async Task<ApiResponse<PaymentResponse>> Handle(
        GetPaymentByExpenseIdQuery request, 
        CancellationToken cancellationToken)
    {
        // Retrieve the Payment based on the provExpenseied Expenseid parameter.
        var payment = await paymentRepository.GetPaymentByExpenseIdAsync(
                request.ExpenseId, 
                cancellationToken);

        // If no Payment is found, return an error response.
        if(payment is null)
            return new ApiResponse<PaymentResponse>(ExceptionMessages.NotFoundWithParameter(request.ExpenseId));

        // Map the Payment to the response model and return a successful response.
        return new ApiResponse<PaymentResponse>(mapper.Map<PaymentResponse>(payment));
    }
}
