namespace IdentitySample.Identity.Api.Models;

public class UserDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }

    public List<RoleNameDto> Roles { get; set; } = new();
}
