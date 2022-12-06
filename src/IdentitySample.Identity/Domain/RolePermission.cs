namespace IdentitySample.Identity.Domain;

public class RolePermission
{
    public Guid Id { get; private set; }
    public Permission Permission { get; set; }

    public Guid RoleId { get; private set; }
    public Role Role { get; private set; } = default!;

    private RolePermission() 
    {
        Permission = default!;
    }

    public RolePermission(Permission permission)
    {
        Permission = permission;
    }
}
