using AutoMapper;
using MediatR;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Schema.Authentication.Responses;
using ExpenseManagement.Base.Extensions.Encryption;
using ExpenseManagement.Data.Repositories.Interfaces.AppUsers;
using ExpenseManagement.Business.Common;
using ExpenseManagement.Schema.AppUser.Responses;
using ExpenseManagement.Base.Enums;
using ExpenseManagement.Base.Constants.Authentication;
using System.Text;

namespace ExpenseManagement.Business.AppUserCqrs.Commands;
/// Handles the command for user create user from admin, mapping the request to an AppUser entity,
/// checking for uniqueness, creating a new user, generating a JWT token, and returning the response.
/// </summary>
public class CreateAppUserCommandHandler :
    IRequestHandler<CreateAppUserCommand, ApiResponse<CreatedAppUserResponse>>
{
    private readonly IMapper mapper;
    private readonly IEfAppUserRepository appUserRepository;

    /// <summary>
    /// Initializes a new instance of the CreateAppUserCommandHandler class.
    /// </summary>
    /// <param name="mapper">The AutoMapper instance for object mapping.</param>
    /// <param name="appUserRepository">The repository for interacting with AppUser entities.</param>
    /// <param name="tokenGenerator">The JWT token generator for creating authentication tokens.</param>
    public CreateAppUserCommandHandler(
        IMapper mapper,
        IEfAppUserRepository appUserRepository)
    {
        this.mapper = mapper;
        this.appUserRepository = appUserRepository;
    }

    /// <summary>
    /// Handles the create user from admin command, mapping the request to an AppUser entity,
    /// checking for uniqueness, creating a new user, generating a JWT token, and returning the response.
    /// </summary>
    /// <param name="request">The create user from admin command request.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>An ApiResponse containing a TokenResponse with the generated token and user email.</returns>
    public async Task<ApiResponse<CreatedAppUserResponse>> Handle(
        CreateAppUserCommand request,
        CancellationToken cancellationToken)
    {
        
        var entity = mapper.Map<AppUser>(request.Model);

        entity.Password = CreateRandomPassword();

        if (!await appUserRepository.IsUniqueAsync(entity, cancellationToken)) 
            return new ApiResponse<CreatedAppUserResponse>(ExceptionMessages.UserIsNotUnique);

        var user = await appUserRepository.CreateAppUserAsync(entity, cancellationToken);

        var response = mapper.Map<CreatedAppUserResponse>(user);

        return new ApiResponse<CreatedAppUserResponse>(response);
    }

    private string CreateRandomPassword(int length = 8)
    {
        StringBuilder result = new StringBuilder();
        Random random = new();

        while (0 < length--)
            result.Append(DefaultUsers.ValidChars[random.Next(DefaultUsers.ValidChars.Length)]);
        
        return result.ToString();
    }
}