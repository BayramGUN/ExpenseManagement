using AutoMapper;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.AppUsers;
using ExpenseManagement.Schema.AppUser.Responses;
using MediatR;

namespace ExpenseManagement.Business.AppUserCqrs.Queries;

/// <summary>
/// Handles the execution of the GetAllAppUsersQuery, retrieving a list of all application users.
/// </summary>
public class GetAllAppUsersQueryHandler : 
    IRequestHandler<GetAllAppUsersQuery, ApiResponse<List<AppUserResponse>>>
{
    private readonly IEfAppUserRepository appUserRepository;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllAppUsersQueryHandler"/> class.
    /// </summary>
    /// <param name="appUserRepository">The repository for interacting with application user entities.</param>
    /// <param name="mapper">The AutoMapper instance for mapping entities to response models.</param>
    public GetAllAppUsersQueryHandler(
        IEfAppUserRepository appUserRepository, 
        IMapper mapper)
    {
        this.appUserRepository = appUserRepository;
        this.mapper = mapper;
    }
    
    /// <summary>
    /// Handles the execution of the query, retrieving a list of all application users.
    /// </summary>
    /// <param name="request">The GetAllAppUsersQuery instance.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>A response containing a list of application user responses.</returns>
    public async Task<ApiResponse<List<AppUserResponse>>> Handle(
        GetAllAppUsersQuery request,
        CancellationToken cancellationToken)
    {
        var appUsers = await appUserRepository.GetAllAppUsersAsync(cancellationToken);
        return new ApiResponse<List<AppUserResponse>>(mapper.Map<List<AppUserResponse>>(appUsers));
    }
}
