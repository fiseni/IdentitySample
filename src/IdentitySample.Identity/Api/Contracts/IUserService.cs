using IdentitySample.Identity.Api.Models;

namespace IdentitySample.Identity.Api.Contracts;

public interface IUserService
{
    Task<UserDto> CreateUserAsync(UserCreateDto userCreateDto, CancellationToken cancellationToken);
    Task DeleteUserAsync(Guid id, CancellationToken cancellationToken);
    Task<UserDto> GetUserAsync(Guid id, CancellationToken cancellationToken);
    Task<UserDto> GetUserAsync(string email, CancellationToken cancellationToken);
    Task<List<UserDto>> GetUsersAsync(CancellationToken cancellationToken);
    Task<UserDto> UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto, CancellationToken cancellationToken);
}
