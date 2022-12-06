using AutoMapper;
using IdentitySample.Identity.Api.Models;
using IdentitySample.Identity.Domain;

namespace IdentitySample.Identity.Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapping
        CreateMap<Permission, PermissionDto>()
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

        CreateMap<User, UserDto>()
            .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.UserRoles.Select(x => x.Role)));

        CreateMap<Role, RoleDto>()
            .ForMember(x => x.Permissions, opt => opt.MapFrom(x => x.RolePermissions.Select(x => x.Permission)));

        CreateMap<Role, RoleNameDto>();

        // Reverse mapping

        CreateMap<SignUpDto, User>()
            .ConvertUsing((src, dest, context) =>
            {
                var user = new User(src.FirstName, src.LastName, src.Email);
                return user;
            });

        CreateMap<UserCreateDto, User>()
            .ConvertUsing((src, dest, context) =>
            {
                var user = new User(src.FirstName, src.LastName, src.Email);
                user.UpdateRoles(src.RoleIds);
                return user;
            });

        CreateMap<UserUpdateDto, User>()
            .ConvertUsing((src, dest, context) =>
            {
                if (dest is null) throw new ArgumentNullException(nameof(dest), "The object to be updated must be provided!");
                dest.Update(src.FirstName, src.LastName);
                dest.UpdateRoles(src.RoleIds);
                return dest;
            });

        CreateMap<RoleCreateDto, Role>()
            .ConvertUsing((src, dest, context) =>
            {
                var role = new Role(src.Name);
                role.ClearAndAddPermissions(src.SelectedPermissionKeys);
                return role;
            });

        CreateMap<RoleUpdateDto, Role>()
            .ConvertUsing((src, dest, context) =>
            {
                dest.Name = src.Name;
                dest.ClearAndAddPermissions(src.SelectedPermissionKeys);
                return dest;
            });
    }
}
