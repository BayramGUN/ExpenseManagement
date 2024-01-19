using AutoMapper;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Extensions.Encryption;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Business.Common.Interfaces.Token;
using ExpenseManagement.Data.Repositories.Interfaces.AppUsers;
using ExpenseManagement.Schema.Authentication.Responses;
using MediatR;

namespace ExpenseManagement.Business.Authentication.Commands.UnBlockAppUser;

/// <summary>
/// Handles the logic for the UnBlockAppUserCommand to authenticate users.
/// </summary>
public class UnBlockAppUserCommandHandler : 
    IRequestHandler<UnBlockAppUserCommand, ApiResponse<TokenResponse>>
{
    private readonly IMapper mapper;
    private readonly IEfAppUserRepository appUserRepository;
    private readonly IJwtTokenGenerator tokenGenerator;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnBlockAppUserCommandHandler"/> class.
    /// </summary>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="appUserRepository">The repository for AppUser entities.</param>
    /// <param name="tokenGenerator">The JWT token generator service.</param>
    public UnBlockAppUserCommandHandler(
        IMapper mapper,
        IEfAppUserRepository appUserRepository,
        IJwtTokenGenerator tokenGenerator)
    {
        this.mapper = mapper;
        this.appUserRepository = appUserRepository;
        this.tokenGenerator = tokenGenerator;
    }
    
    /// <summary>
    /// Handles the UnBlockAppUserCommand, authenticating users based on provided credentials.
    /// </summary>
    /// <param name="request">The UnBlockAppUserCommand containing user credentials.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>An ApiResponse containing a TokenResponse or an error message.</returns>
    public async Task<ApiResponse<TokenResponse>> Handle(UnBlockAppUserCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the AppUser based on the provided credentials
        var appUser = await appUserRepository.GetAppUserByParameter(
            userName: request.Model.UserName,
            email: request.Model.Email,
            phone: request.Model.Phone,
            identityNumber: request.Model.IdentityNumber,
            cancellationToken: cancellationToken
        );

        // If no user is found, return an error response
        if(appUser is null)
            return new ApiResponse<TokenResponse>(ExceptionMessages.InvalidCredentials);

        // Check if the provided password matches the stored hashed password
        string hashedPasswordCredential = request.Model.TemporaryPassword;
        if(!hashedPasswordCredential.Equals(appUser.Password))
        {
            // Update user status and password-related properties on failed login attempts
            if(appUser.PasswordRetryCount > 3)
            {
                await appUserRepository.DeleteAppUserAsync(appUser.Id, cancellationToken);
                return new ApiResponse<TokenResponse>(ExceptionMessages.DeletedUser);
            }
            appUser.LastActivityDate = DateTime.UtcNow;
            appUser.PasswordRetryCount++;
            await appUserRepository.UpdateAppUserAsync(appUser, cancellationToken);
            return new ApiResponse<TokenResponse>(ExceptionMessages.InvalidCredentials);
        }

        

        // If the user is blocked, return an error response
        if(!appUser.Status)
            // TODO: May add a save account service.
            return new ApiResponse<TokenResponse>(ExceptionMessages.BlockedUser);

        // Update user properties after successful login.
        appUser.LastActivityDate = DateTime.UtcNow;
        appUser.PasswordRetryCount = 0;
        appUser.Status = false;
        appUser.Password = request.Model.NewPassword.GetSHA256Hash();
        await appUserRepository.UpdateAppUserAsync(appUser, cancellationToken);

        // Generate JWT token and return a success response.
        string token = tokenGenerator.GenerateToken(appUser);
        return new ApiResponse<TokenResponse>(new TokenResponse
        {
            Email = appUser.Email,
            Token = token
        });
    }
}