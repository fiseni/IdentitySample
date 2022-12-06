namespace IdentitySample.Identity.Api.Models;

public class UserUpdateDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public List<Guid>? RoleIds { get; set; }
}
