using AutoMapper;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.Payments;
using ExpenseManagement.Schema.Payment.Responses;
using MediatR;

namespace ExpenseManagement.Business.PaymentCqrs.Queries;

/// <summary>
/// Handles the execution of the GetPaymentByIdQuery, retrieving an Payment by its unique identifier.
/// </summary>
public class GetPaymentByIdQueryHandler : 
    IRequestHandler<GetPaymentByIdQuery, ApiResponse<PaymentResponse>>
{
    private readonly IEfPaymentRepository paymentRepository;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetPaymentByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="paymentRepository">The repository for interacting with Payment entities.</param>
    /// <param name="mapper">The AutoMapper instance for mapping entities to response models.</param>
    public GetPaymentByIdQueryHandler(
        IEfPaymentRepository paymentRepository, 
        IMapper mapper)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Handles the execution of the query, retrieving an Payment by its unique identifier.
    /// </summary>
    /// <param name="request">The GetPaymentByIdQuery instance.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>A response containing the Payment response model or an error message if not found.</returns>
    public async Task<ApiResponse<PaymentResponse>> Handle(
        GetPaymentByIdQuery request, 
        CancellationToken cancellationToken)
    {
        // Retrieve the Payment based on the provided id parameter.
        var Payment = await paymentRepository.GetPaymentByIdAsync(request.Id, cancellationToken);

        // If no Payment is found, return an error response.
        if(Payment is null)
            return new ApiResponse<PaymentResponse>(ExceptionMessages.NotFoundWithParameter(request.Id));

        // Map the Payment to the response model and return a successful response.
        return new ApiResponse<PaymentResponse>(mapper.Map<PaymentResponse>(Payment));
    }
}
