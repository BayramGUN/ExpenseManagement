using AutoMapper;
using MediatR;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.Expenses;
using ExpenseManagement.Business.Common;
using ExpenseManagement.Base.Enums;
using ExpenseManagement.Base.Constants.Authentication;
using System.Text;
using ExpenseManagement.Data.Entities;
using ExpenseManagement.Data.Repositories.Interfaces.Payments;

namespace ExpenseManagement.Business.PaymentCqrs.Commands;
/// Handles the command for user Delete user from admin, mapping the request to an payment entity,
/// checking for uniqueness, Deleting a payment and returning the response message.
/// </summary>
public class DeletePaymentCommandHandler :
    IRequestHandler<DeletePaymentCommand, ApiResponse>
{
    private readonly IEfPaymentRepository paymentRepository;

    /// <summary>
    /// Initializes a new instance of the DeletePaymentCommandHandler class.
    /// </summary>
    /// <param name="PaymentRepository">The repository for interacting with payment entities.</param>
    public DeletePaymentCommandHandler(IEfPaymentRepository paymentRepository)
    {
        this.paymentRepository = paymentRepository;
    }

    /// <summary>
    /// Handles the Delete user from admin command, mapping the request to an payment entity,
    /// checking for uniqueness, Deleting a new user, generating a JWT token, and returning the response.
    /// </summary>
    /// <param name="request">The Delete user from admin command request.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>An ApiResponse containing a TokenResponse with the generated token and user email.</returns>
    public async Task<ApiResponse> Handle(
        DeletePaymentCommand request,
        CancellationToken cancellationToken)
    {
        var entity = paymentRepository.GetPaymentByIdAsync(request.Id, cancellationToken);
        if(entity is null)
            return new ApiResponse(ExceptionMessages.NotFound(request.Id));

        await paymentRepository.DeletePaymentAsync(request.Id, cancellationToken);
        return new ApiResponse(SuccessMessages.DeletedSuccess(request.Id));
    }
}