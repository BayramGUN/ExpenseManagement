using AutoMapper;
using ExpenseManagement.Base.Extensions.Encryption;
using ExpenseManagement.Base.Mapping;
using ExpenseManagement.Business.Configurations.Extensions;
using ExpenseManagement.Schema.AppUser.Responses;
using ExpenseManagement.Schema.AppUser.Requests;
using ExpenseManagement.Schema.Authentication.Requests;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseManagement.Business.Configurations.Mapper;

/// <summary>
/// Configuration class for AutoMapper mappings between business entities and schema objects.
/// </summary>
public class MapperConfig : Profile
{

    /// <summary>
    /// Initializes a new instance of the MapperConfig class and defines the mapping configurations.
    /// </summary>
    public MapperConfig()
    {
        // Map SignUpRequest to AppUser, specifying custom mappings
        CreateMap<SignUpRequest, AppUser>()
            .ForMember(dest => dest.Role,
                opt => opt.MapFrom(src => src.Role.ParseUserRole()))
            .ForMember(dest => dest.Password,
                opt => opt.MapFrom(src => src.Password.GetSHA256Hash()));

        // Map CreateAppUserRequest to AppUser, specifying custom mappings
        CreateMap<CreateAppUserRequest, AppUser>()
            .ForMember(dest => dest.InsertUserId,
                opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.InsertDate,
                opt => opt.MapFrom(src => src.RequestTimestamp))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => true));
        
        // Map CreateAppUserRequest to AppUser, specifying custom mappings
        CreateMap<AppUser, CreatedAppUserResponse>()
            .ForMember(dest => dest.TemporaryPassword,
                opt => opt.MapFrom(src => src.Password));

        // Map UpdateAppUserRequest to AppUser, specifying custom mappings
        CreateMap<UpdateAppUserRequest, AppUser>()
            .ForMember(dest => dest.UpdateDate, 
                opt => opt.MapFrom(src => src.RequestTimestamp))
            .ForMember(dest => dest.UpdateUserId,
                opt => opt.MapFrom(src => src.UserId));

        // Map UpdateAppUserRequest to AppUser, specifying custom mappings
        CreateMap<AppUser, AppUserResponse>()
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => AppUserMappingStrings.AppUserFullName(src.FirstName, src.LastName)));
    }
}