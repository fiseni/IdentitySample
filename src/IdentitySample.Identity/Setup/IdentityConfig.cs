namespace IdentitySample.Identity.Setup;

public class IdentityConfig
{
    public const string CONFIG_NAME = "Identity";

    public static IdentityConfig Instance { get; } = new IdentityConfig();
    private IdentityConfig() { }

    public string ConnectionString { get; set; } = default!;
}
