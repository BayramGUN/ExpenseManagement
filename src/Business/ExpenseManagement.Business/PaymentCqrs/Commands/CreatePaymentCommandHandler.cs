using AutoMapper;
using MediatR;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Entities;
using ExpenseManagement.Data.Repositories.Interfaces.Payments;

namespace ExpenseManagement.Business.PaymentCqrs.Commands;

/// Handles the command for user create user from admin, mapping the request to an payment entity,
/// checking for uniqueness, creating a new user, generating a JWT token, and returning the response.
/// </summary>
public class CreatePaymentCommandHandler :
    IRequestHandler<CreatePaymentCommand, ApiResponse>
{
    private readonly IMapper mapper;
    private readonly IEfPaymentRepository paymentRepository;

    /// <summary>
    /// Initializes a new instance of the CreatePaymentCommandHandler class.
    /// </summary>
    /// <param name="mapper">The AutoMapper instance for object mapping.</param>
    /// <param name="PaymentRepository">The repository for interacting with payment entities.</param>
    /// <param name="tokenGenerator">The JWT token generator for creating authentication tokens.</param>
    public CreatePaymentCommandHandler(
        IMapper mapper,
        IEfPaymentRepository paymentRepository)
    {
        this.mapper = mapper;
        this.paymentRepository = paymentRepository;
    }

    /// <summary>
    /// Handles the create user from admin command, mapping the request to an payment entity,
    /// checking for uniqueness, creating a new user, generating a JWT token, and returning the response.
    /// </summary>
    /// <param name="request">The create user from admin command request.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>An ApiResponse containing a TokenResponse with the generated token and user email.</returns>
    public async Task<ApiResponse> Handle(
        CreatePaymentCommand request,
        CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Payment>(request.Model);
        var isUnique = await paymentRepository.IsUniqueAsync(entity, cancellationToken);
        if(!isUnique)
            return new ApiResponse(ExceptionMessages.PaymentHasNotUniqueId);

        await paymentRepository.CreatePaymentAsync(entity, cancellationToken);
        
        return new ApiResponse(SuccessMessages.CreatedSuccess(entity.Id.ToString()));
    }
}