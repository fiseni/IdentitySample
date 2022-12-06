namespace IdentitySample.Identity.Api.Models;

public class SignInDto
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
