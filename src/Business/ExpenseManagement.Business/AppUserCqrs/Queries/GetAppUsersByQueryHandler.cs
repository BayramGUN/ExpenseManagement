using AutoMapper;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.AppUsers;
using ExpenseManagement.Schema.AppUser.Responses;
using MediatR;

namespace ExpenseManagement.Business.AppUserCqrs.Queries;

/// <summary>
/// Handles the execution of the GetAppUsersByParameterQuery, retrieving a list of application users based on specified parameters.
/// </summary>
public class GetUsersByQueryHandler : 
    IRequestHandler<GetAppUsersByParameterQuery, ApiResponse<List<AppUserResponse>>>
{
    private readonly IEfAppUserRepository appUserRepository;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUsersByQueryHandler"/> class.
    /// </summary>
    /// <param name="appUserRepository">The repository for interacting with application user entities.</param>
    /// <param name="mapper">The AutoMapper instance for mapping entities to response models.</param>
    public GetUsersByQueryHandler(
        IEfAppUserRepository appUserRepository, 
        IMapper mapper)
    {
        this.appUserRepository = appUserRepository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Handles the execution of the query, retrieving a list of application users based on specified parameters.
    /// </summary>
    /// <param name="request">The GetAppUsersByParameterQuery instance.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>A response containing the list of application user responses or an error message if not found.</returns>
    public async Task<ApiResponse<List<AppUserResponse>>> Handle(
        GetAppUsersByParameterQuery request, 
        CancellationToken cancellationToken)
    {
        // Retrieve the AppUsers based on the provided parameters.
        var appUsers = await appUserRepository.FilterAppUsersByParameterAsync(
            firstName: request.Model.FirstName,
            lastName: request.Model.LastName,
            role: request.Model.Role,
            isActive: request.Model.IsActive,
            status: request.Model.Status,
            cancellationToken: cancellationToken
        );

        // If no AppUsers are found, return an error response.
        if(appUsers is null)
            return new ApiResponse<List<AppUserResponse>>(ExceptionMessages.NotFoundWithParameter(request.Model));

        // Map the AppUsers to the response models and return a successful response.
        return new ApiResponse<List<AppUserResponse>>(mapper.Map<List<AppUserResponse>>(appUsers));
    }
}