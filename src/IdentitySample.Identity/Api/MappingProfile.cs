using AutoMapper;
using IdentitySample.Identity.Api.Models;
using IdentitySample.Identity.Domain;

namespace IdentitySample.Identity.Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Permission, PermissionDto>()
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

        CreateMap<User, UserDto>()
            .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.UserRoles.Select(x => x.Role)));

        CreateMap<Role, RoleDto>()
            .ForMember(x => x.Permissions, opt => opt.MapFrom(x => x.RolePermissions.Select(x => x.Permission)));

        CreateMap<Role, RoleNameDto>();

    }
}
