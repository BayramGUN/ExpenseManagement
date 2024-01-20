using AutoMapper;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.AppUsers;
using MediatR;


namespace ExpenseManagement.Business.AppUserCqrs.Commands;

/// <summary>
/// Handles the logic for the DeleteAppUserCommandHandler to delete users.
/// </summary>
public class DeleteAppUserCommandHandler : 
    IRequestHandler<DeleteAppUserCommand, ApiResponse>
{
    private readonly IEfAppUserRepository appUserRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteAppUserCommandHandler"/> class.
    /// </summary>
    /// <param name="appUserRepository">The repository for AppUser entities.</param>
    public DeleteAppUserCommandHandler(IEfAppUserRepository appUserRepository)
    {
        this.appUserRepository = appUserRepository;
    }
    
    /// <summary>
    /// Handles the DeleteAppUserCommand by updating an AppUser entity in the repository.
    /// </summary>
    /// <param name="request">The command containing the model for updating the AppUser.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>An ApiResponse indicating the outcome of the delete operation.</returns>
    public async Task<ApiResponse> Handle(
        DeleteAppUserCommand request,
        CancellationToken cancellationToken)
    {
        var userWillDelete = appUserRepository.GetAppUserByParameterAsync(id: request.Id);
        if(userWillDelete is null)
            return new ApiResponse<string>(ExceptionMessages.NotFound(request.Id));
        await appUserRepository.DeleteAppUserAsync(request.Id, cancellationToken);
        return new ApiResponse<string>(SuccessMessages.DeletedSuccess(request.Id));
    }
}