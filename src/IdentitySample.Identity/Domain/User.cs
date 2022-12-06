using Microsoft.AspNetCore.Identity;

namespace IdentitySample.Identity.Domain;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public IEnumerable<UserRole> UserRoles => _userRoles.AsEnumerable();
    private readonly List<UserRole> _userRoles = new();

    private User()
    {
        FirstName = default!;
        LastName = default!;
    }

    public User(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public void Update(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public void UpdateRoles(IEnumerable<Guid> roleIds)
    {
        if (roleIds is null) return;

        _userRoles.Clear();

        foreach (var roleId in roleIds)
        {
            var userRole = new UserRole(Id, roleId);

            _userRoles.Add(userRole);
        }
    }
}
