using AutoMapper;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Extensions.Encryption;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Business.Common;
using ExpenseManagement.Data.Repositories.Interfaces.AppUsers;
using ExpenseManagement.Schema.Authentication.Responses;
using MediatR;

namespace ExpenseManagement.Business.Authentication.Commands.SignIn;

/// <summary>
/// Handles the logic for the SignInCommand to authenticate users.
/// </summary>
public class SignInCommandHandler : 
    IRequestHandler<SignInCommand, ApiResponse<TokenResponse>>
{
    private readonly IMapper mapper;
    private readonly IAppUserRepository appUserRepository;
    private readonly IJwtTokenGenerator tokenGenerator;

    /// <summary>
    /// Initializes a new instance of the <see cref="SignInCommandHandler"/> class.
    /// </summary>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="appUserRepository">The repository for AppUser entities.</param>
    /// <param name="tokenGenerator">The JWT token generator service.</param>
    public SignInCommandHandler(
        IMapper mapper,
        IAppUserRepository appUserRepository,
        IJwtTokenGenerator tokenGenerator)
    {
        this.mapper = mapper;
        this.appUserRepository = appUserRepository;
        this.tokenGenerator = tokenGenerator;
    }

    public async Task<ApiResponse<TokenResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
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
        string hashedPasswordCredential = request.Model.Password.GetSHA256Hash();
        if(!hashedPasswordCredential.Equals(appUser.Password))
        {
            // Update user status and password-related properties on failed login attempts
            appUser.Status = (appUser.PasswordRetryCount > 3) ? true : false;
            appUser.LastActivityDate = DateTime.UtcNow;
            appUser.PasswordRetryCount++;
            await appUserRepository.UpdateAppUserAsync(appUser, cancellationToken);
            return new ApiResponse<TokenResponse>(ExceptionMessages.InvalidCredentials);
        }

        

        // If the user is blocked, return an error response
        if(appUser.Status)
            // TODO: May add a save account service.
            return new ApiResponse<TokenResponse>(ExceptionMessages.BlockedUser);

        // Update user properties after successful login.
        appUser.LastActivityDate = DateTime.UtcNow;
        appUser.PasswordRetryCount = 0;
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