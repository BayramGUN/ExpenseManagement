using AutoMapper;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.AppUsers;
using ExpenseManagement.Schema.AppUser.Responses;
using MediatR;

namespace ExpenseManagement.Business.AppUserCqrs.Queries;

/// <summary>
/// Handles the execution of the GetAppUserByParameterQuery, retrieving an application user based on specified parameters.
/// </summary>
public class GetUserByQueryHandler : 
    IRequestHandler<GetAppUserByParameterQuery, ApiResponse<AppUserResponse>>
{
    private readonly IEfAppUserRepository appUserRepository;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserByQueryHandler"/> class.
    /// </summary>
    /// <param name="appUserRepository">The repository for interacting with application user entities.</param>
    /// <param name="mapper">The AutoMapper instance for mapping entities to response models.</param>
    public GetUserByQueryHandler(
        IEfAppUserRepository appUserRepository, 
        IMapper mapper)
    {
        this.appUserRepository = appUserRepository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Handles the execution of the query, retrieving an application user based on specified parameters.
    /// </summary>
    /// <param name="request">The GetAppUserByParameterQuery instance.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>A response containing the application user response or an error message if not found.</returns>
    public async Task<ApiResponse<AppUserResponse>> Handle(
        GetAppUserByParameterQuery request, 
        CancellationToken cancellationToken)
    {
        // Retrieve the AppUser based on the provided parameters.
        var appUser = await appUserRepository.GetAppUserByParameterAsync(
            id: request.Model.Id,
            userName: request.Model.UserName,
            email: request.Model.Email,
            phone: request.Model.Phone,
            identityNumber: request.Model.IdentityNumber,
            cancellationToken: cancellationToken
        );

        // If no AppUser is found, return an error response.
        if(appUser is null)
            return new ApiResponse<AppUserResponse>(ExceptionMessages.NotFoundWithParameter(request.Model));

        // Map the AppUser to the response model and return a successful response.
        return new ApiResponse<AppUserResponse>(mapper.Map<AppUserResponse>(appUser));
    }
}
