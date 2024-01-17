using AutoMapper;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.AppUsers;
using ExpenseManagement.Schema.AppUser.Responses;
using MediatR;

namespace ExpenseManagement.Business.AppUserOperations.Queries;

public class GetAllAppUsersQueryHandler : 
    IRequestHandler<GetAllAppUsersQuery, ApiResponse<List<AppUserResponse>>>
{
    private readonly IAppUserRepository appUserRepository;
    private readonly IMapper mapper;

    public GetAllAppUsersQueryHandler(
        IAppUserRepository appUserRepository, 
        IMapper mapper)
    {
        this.appUserRepository = appUserRepository;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<AppUserResponse>>> Handle(
        GetAllAppUsersQuery request,
        CancellationToken cancellationToken)
    {
        var appUsers = await appUserRepository.GetAllAppUsersAsync(cancellationToken);
        return new ApiResponse<List<AppUserResponse>>(mapper.Map<List<AppUserResponse>>(appUsers));
    }
}
