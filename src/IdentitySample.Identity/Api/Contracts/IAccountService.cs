using IdentitySample.Identity.Api.Models;

namespace IdentitySample.Identity.Api.Contracts;

public interface IAccountService
{
    Task<UserDto> SignInAsync(SignInDto signInDto, CancellationToken cancellationToken);
    Task<UserDto> SignUpAsync(SignUpDto signUpDto, CancellationToken cancellationToken);
}
