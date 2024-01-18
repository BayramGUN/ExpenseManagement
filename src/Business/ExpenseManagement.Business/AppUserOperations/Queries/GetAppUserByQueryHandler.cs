using AutoMapper;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.AppUsers;
using ExpenseManagement.Schema.AppUser.Responses;
using MediatR;

namespace ExpenseManagement.Business.AppUserOperations.Queries;

public class GetUserByQueryHandler : 
    IRequestHandler<GetAppUserByParameterQuery, ApiResponse<AppUserResponse>>
{
    private readonly IAppUserRepository appUserRepository;
    private readonly IMapper mapper;

    public GetUserByQueryHandler(
        IAppUserRepository appUserRepository, 
        IMapper mapper)
    {
        this.appUserRepository = appUserRepository;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<AppUserResponse>> Handle(
        GetAppUserByParameterQuery request, 
        CancellationToken cancellationToken)
    {
        // Retrieve the AppUser based on the provided parameters.
        var appUser = await appUserRepository.GetAppUserByParameter(
            id: request.Model.Id,
            userName: request.Model.UserName,
            email: request.Model.Email,
            phone: request.Model.Phone,
            identityNumber: request.Model.IdentityNumber,
            cancellationToken: cancellationToken
        );

        if(appUser is null)
            return new ApiResponse<AppUserResponse>(ExceptionMessages.NotFoundWithParameter(request.Model));

        return new ApiResponse<AppUserResponse>(mapper.Map<AppUserResponse>(appUser));
    }
}
