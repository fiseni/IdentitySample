using IdentitySample.Identity.Domain;

namespace IdentitySample.Identity.Infrastructure.Seeds;

public static class UserSeed
{
    public static List<User> GetUsers()
    {
        return new()
        {
            new User("Test", "Test", "test@local.com")
        };
    }
}
