using AutoMapper;
using ExpenseManagement.Base.Extensions.Encryption;
using ExpenseManagement.Business.Configurations.Extensions;
using ExpenseManagement.Schema.Authentication.Requests;

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
    }
}