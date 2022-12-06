namespace IdentitySample.Identity.Api.Models;

public class RoleCreateDto
{
    public string Name { get; set; } = default!;

    public List<int>? SelectedPermissionKeys { get; set; }
}
