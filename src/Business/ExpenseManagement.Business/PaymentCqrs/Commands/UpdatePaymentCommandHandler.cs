using AutoMapper;
using MediatR;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.Payments;

namespace ExpenseManagement.Business.PaymentCqrs.Commands;

/// Handles the command for payment Update payment from admin, mapping the request to an Payment entity,
/// checking for uniqueness, Updating a Payment and returning the response message.
/// </summary>
public class UpdatePaymentCommandHandler :
    IRequestHandler<UpdatePaymentCommand, ApiResponse>
{
    private readonly IEfPaymentRepository PaymentRepository;

    /// <summary>
    /// Initializes a new instance of the UpdatePaymentCommandHandler class.
    /// </summary>
    /// <param name="paymentRepository">The repository for interacting with Payment entities.</param>
    /// <param name="tokenGenerator">The JWT token generator for Updating authentication tokens.</param>
    public UpdatePaymentCommandHandler(IEfPaymentRepository PaymentRepository)
    {
        this.PaymentRepository = PaymentRepository;
    }

    /// <summary>
    /// Handles the Update user from admin command, mapping the request to an Payment entity,
    /// checking for uniqueness, Updating a new user, generating a JWT token, and returning the response.
    /// </summary>
    /// <param name="request">The Update user from admin command request.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>An ApiResponse containing a TokenResponse with the generated token and user email.</returns>
    public async Task<ApiResponse> Handle(
        UpdatePaymentCommand request,
        CancellationToken cancellationToken)
    {

        var entityWillUpdate = await PaymentRepository.GetPaymentByIdAsync(request.Model.Id, cancellationToken);
        if(entityWillUpdate is null)
            return new ApiResponse(ExceptionMessages.NotFound(request.Model.Id));

        entityWillUpdate.Amount = request.Model.Amount;
        entityWillUpdate.ExpenseId = request.Model.ExpenseId;
        entityWillUpdate.PaymentDate = request.Model.PaymentDate;
        entityWillUpdate.PaymentMethod = request.Model.PaymentMethod ?? entityWillUpdate.PaymentMethod;
        entityWillUpdate.UpdateUserId = request.Model.UserId;
        entityWillUpdate.UpdateDate = request.Model.RequestTimestamp;
        
        await PaymentRepository.UpdatePaymentAsync(entityWillUpdate, cancellationToken);
        return new ApiResponse(SuccessMessages.UpdatedSuccess(entityWillUpdate.Id.ToString()));
    }
}