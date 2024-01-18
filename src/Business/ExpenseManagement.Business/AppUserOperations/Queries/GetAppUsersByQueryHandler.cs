using AutoMapper;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.AppUsers;
using ExpenseManagement.Schema.AppUser.Responses;
using MediatR;

namespace ExpenseManagement.Business.AppUserOperations.Queries;

public class GetUsersByQueryHandler : 
    IRequestHandler<GetAppUsersByParameterQuery, ApiResponse<List<AppUserResponse>>>
{
    private readonly IAppUserRepository appUserRepository;
    private readonly IMapper mapper;

    public GetUsersByQueryHandler(
        IAppUserRepository appUserRepository, 
        IMapper mapper)
    {
        this.appUserRepository = appUserRepository;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<AppUserResponse>>> Handle(
        GetAppUsersByParameterQuery request, 
        CancellationToken cancellationToken)
    {
        // Retrieve the AppUser based on the provided parameters.
        var appUsers = await appUserRepository.FilterAppUsersByParameter(
            firstName: request.Model.FirstName,
            lastName: request.Model.LastName,
            role: request.Model.Role,
            isActive: request.Model.IsActive,
            status: request.Model.Status,
            cancellationToken: cancellationToken
        );

        if(appUsers is null)
            return new ApiResponse<List<AppUserResponse>>(ExceptionMessages.NotFoundWithParameter(request.Model));

        return new ApiResponse<List<AppUserResponse>>(mapper.Map<List<AppUserResponse>>(appUsers));
    }
}
