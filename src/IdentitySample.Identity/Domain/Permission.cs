namespace IdentitySample.Identity.Domain;

public class Permission
{
    public PermissionKey Key { get; private set; }
    public bool Value { get; private set; }

    private Permission() { }
    public Permission(PermissionKey key, bool value = false)
    {
        Key = key;
        Value = value;
    }
}
