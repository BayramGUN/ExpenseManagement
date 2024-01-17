using AutoMapper;
using MediatR;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Schema.Authentication.Responses;
using ExpenseManagement.Base.Extensions.Encryption;
using ExpenseManagement.Data.Repositories.Interfaces.AppUsers;
using ExpenseManagement.Business.Common;

namespace ExpenseManagement.Business.Authentication.Commands.SignUp;

/// <summary>
/// Handles the command for user sign-up, mapping the request to an AppUser entity,
/// checking for uniqueness, creating a new user, generating a JWT token, and returning the response.
/// </summary>
public class SignUpCommandHandler :
    IRequestHandler<SignUpCommand, ApiResponse<TokenResponse>>
{
    private readonly IMapper mapper;
    private readonly IAppUserRepository appUserRepository;
    private readonly IJwtTokenGenerator tokenGenerator;

    /// <summary>
    /// Initializes a new instance of the SignUpCommandHandler class.
    /// </summary>
    /// <param name="mapper">The AutoMapper instance for object mapping.</param>
    /// <param name="appUserRepository">The repository for interacting with AppUser entities.</param>
    /// <param name="tokenGenerator">The JWT token generator for creating authentication tokens.</param>
    public SignUpCommandHandler(
        IMapper mapper,
        IAppUserRepository appUserRepository,
        IJwtTokenGenerator tokenGenerator)
    {
        this.mapper = mapper;
        this.appUserRepository = appUserRepository;
        this.tokenGenerator = tokenGenerator;
    }

    /// <summary>
    /// Handles the sign-up command, mapping the request to an AppUser entity,
    /// checking for uniqueness, creating a new user, generating a JWT token, and returning the response.
    /// </summary>
    /// <param name="request">The sign-up command request.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>An ApiResponse containing a TokenResponse with the generated token and user email.</returns>
    public async Task<ApiResponse<TokenResponse>> Handle(
        SignUpCommand request,
        CancellationToken cancellationToken)
    {
        var entity = mapper.Map<AppUser>(request.Model);

        if (!await appUserRepository.IsUniqueAsync(entity, cancellationToken)) 
            return new ApiResponse<TokenResponse>(ExceptionMessages.UserIsNotUnique);

        var user = await appUserRepository.CreateAppUserAsync(entity, cancellationToken);
             
        return (user is not null) ? new ApiResponse<TokenResponse>(new TokenResponse
        {
            Token = tokenGenerator.GenerateToken(user),
            Email = user!.Email
        }) : new ApiResponse<TokenResponse>(ExceptionMessages.TokenException);
    }
}