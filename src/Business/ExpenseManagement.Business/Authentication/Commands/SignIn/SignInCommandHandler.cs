using AutoMapper;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Extensions.Encryption;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Business.Common;
using ExpenseManagement.Data.Repositories.Interfaces.AppUsers;
using ExpenseManagement.Schema.Authentication.Responses;
using MediatR;
using Microsoft.Identity.Client;

namespace ExpenseManagement.Business.Authentication.Commands.SignIn;

public class SignInCommandHandler : 
    IRequestHandler<SignInCommand, ApiResponse<TokenResponse>>
{
    private readonly IMapper mapper;
    private readonly IAppUserRepository appUserRepository;
    private readonly IJwtTokenGenerator tokenGenerator;

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
        var appUser = await appUserRepository.GetAppUserByParameter(
            userName: request.Model.UserName,
            email: request.Model.Email,
            phone: request.Model.Phone,
            identityNumber: request.Model.IdentityNumber,
            cancellationToken: cancellationToken
        );
        if(appUser is null)
            return new ApiResponse<TokenResponse>(ExceptionMessages.InvalidCredentials);

        string hashedPasswordCredential = request.Model.Password.GetSHA256Hash();

        if(!hashedPasswordCredential.Equals(appUser.Password))
        {
            appUser.LastActivityDate = DateTime.UtcNow;
            appUser.PasswordRetryCount++;
            await appUserRepository.UpdateAppUserAsync(appUser, cancellationToken);
            return new ApiResponse<TokenResponse>(ExceptionMessages.InvalidCredentials);
        }

        if(appUser.PasswordRetryCount > 3)
        {
            appUser.Status = true;
            await appUserRepository.UpdateAppUserAsync(appUser, cancellationToken);
            return new ApiResponse<TokenResponse>(ExceptionMessages.BlockedUser);
        }

        if(appUser.Status)
            return new ApiResponse<TokenResponse>(ExceptionMessages.BlockedUser);

        appUser.LastActivityDate = DateTime.UtcNow;
        appUser.PasswordRetryCount = 0;
        await appUserRepository.UpdateAppUserAsync(appUser, cancellationToken);

        string token = tokenGenerator.GenerateToken(appUser);

        return new ApiResponse<TokenResponse>(new TokenResponse
        {
            Email = appUser.Email,
            Token = token
        });
    }
}