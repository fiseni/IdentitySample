namespace IdentitySample.Identity.Api.Models;

public class UserCreateDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!; 

    public List<Guid>? RoleIds { get; set; }
}
