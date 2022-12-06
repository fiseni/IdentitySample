namespace IdentitySample.Identity.Api.Models;

public class RoleUpdateDto
{
    public string Name { get; set; } = default!;

    public List<int>? SelectedPermissionIds { get; set; }
}
