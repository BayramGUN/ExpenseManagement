using AutoMapper;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.Payments;
using ExpenseManagement.Schema.Payment.Responses;
using MediatR;

namespace ExpenseManagement.Business.PaymentCqrs.Queries;

/// <summary>
/// Handles the execution of the GetPaymentsByParameterQuery, retrieving a list of Payments based on specified parameters.
/// </summary>
public class GetPaymentsByQueryHandler : 
    IRequestHandler<GetPaymentsByParameterQuery, ApiResponse<List<PaymentResponse>>>
{
    private readonly IEfPaymentRepository PaymentRepository;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetPaymentsByQueryHandler"/> class.
    /// </summary>
    /// <param name="PaymentRepository">The repository for interacting with Payment entities.</param>
    /// <param name="mapper">The AutoMapper instance for mapping entities to response models.</param>
    public GetPaymentsByQueryHandler(
        IEfPaymentRepository PaymentRepository, 
        IMapper mapper)
    {
        this.PaymentRepository = PaymentRepository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Handles the execution of the query, retrieving a list of Payments based on specified parameters.
    /// </summary>
    /// <param name="request">The GetPaymentsByParameterQuery instance.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>A response containing the list of Payment response models or an error message if not found.</returns>
    public async Task<ApiResponse<List<PaymentResponse>>> Handle(
        GetPaymentsByParameterQuery request, 
        CancellationToken cancellationToken)
    {
        // Retrieve Payments based on the provided parameters.
        var payments = await PaymentRepository.FilterPaymentsByParameterAsync(
            paymentDate: request.Model.PaymentDate,
            amount: request.Model.Amount,
            paymentMethod: request.Model.PaymentMethod,
            appUserId: request.Model.AppUserId,
            cancellationToken: cancellationToken
        );

        // If no payments are found, return an error response.
        if(payments is null)
            return new ApiResponse<List<PaymentResponse>>(ExceptionMessages.NotFoundWithParameter(request.Model));

        // Map the payments to the response models and return a successful response.
        return new ApiResponse<List<PaymentResponse>>(mapper.Map<List<PaymentResponse>>(payments));
    }
}
