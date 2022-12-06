namespace IdentitySample.Identity.Api.Models;

public class RoleDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public List<PermissionDto> Permissions { get; set; } = new();
}
