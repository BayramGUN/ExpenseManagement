using AutoMapper;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.AppUsers;
using MediatR;


namespace ExpenseManagement.Business.AppUserCqrs.Commands;

/// <summary>
/// Handles the logic for the UpdateAppUserCommandHandler to update users.
/// </summary>
public class UpdateAppUserCommandHandler : 
    IRequestHandler<UpdateAppUserCommand, ApiResponse>
{
    private readonly IEfAppUserRepository appUserRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateAppUserCommandHandler"/> class.
    /// </summary>
    /// <param name="appUserRepository">The repository for AppUser entities.</param>
    public UpdateAppUserCommandHandler(IEfAppUserRepository appUserRepository)
    {
        this.appUserRepository = appUserRepository;
    }
    
    /// <summary>
    /// Handles the UpdateAppUserCommand by updating an AppUser entity in the repository.
    /// </summary>
    /// <param name="request">The command containing the model for updating the AppUser.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>An ApiResponse indicating the outcome of the update operation.</returns>
    public async Task<ApiResponse> Handle(
        UpdateAppUserCommand request, 
        CancellationToken cancellationToken)
    {
        // Retrieve the AppUser based on the provided parameters.
        var userWillUpdate = await appUserRepository.GetAppUserByParameterAsync(
            id: request.Model.Id,
            userName: request.Model.UserName,
            email: request.Model.Email,
            phone: request.Model.Phone,
            identityNumber: null,
            cancellationToken: cancellationToken
        );
        if(userWillUpdate is null)
            return new ApiResponse<string>(ExceptionMessages.NotFound(request.Model.Id));
            
        userWillUpdate.Email = request.Model.Email ?? userWillUpdate.Email;
        userWillUpdate.AccountNumber = request.Model.AccountNumber ?? userWillUpdate.AccountNumber;
        userWillUpdate.Phone = request.Model.Phone ?? userWillUpdate.Phone;
        userWillUpdate.UserName = request.Model.UserName ?? userWillUpdate.UserName;
        userWillUpdate.UpdateDate = request.Model.RequestTimestamp;
        userWillUpdate.UpdateUserId = request.Model.UserId;
        
        var result = await appUserRepository.UpdateAppUserAsync(userWillUpdate, cancellationToken);
        
        return new ApiResponse(SuccessMessages.UpdatedSuccess(result.UserName ?? result.Id.ToString()));
    }
}