using IdentitySample.Identity.Domain;
using IdentitySample.Identity.Infrastructure.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentitySample.Identity.Infrastructure;

public class AppIdentityDbInitializer
{
    private const string PASSWORD = "Asd..123";

    private readonly AppIdentityDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public AppIdentityDbInitializer(AppIdentityDbContext dbContext,
                                    UserManager<User> userManager,
                                    RoleManager<Role> roleManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task Seed()
    {
        await _dbContext.Database.EnsureCreatedAsync();

        await SeedRoles();
        await SeedUsers();
    }

    private async Task SeedRoles()
    {
        if (!await _dbContext.Roles.AnyAsync())
        {
            var roles = RoleSeed.GetRoles();
            foreach (var role in roles)
            {
                await _roleManager.CreateAsync(role);
            }
        }
    }

    private async Task SeedUsers()
    {
        if (!await _dbContext.Users.AnyAsync())
        {
            var users = UserSeed.GetUsers();

            foreach (var user in users)
            {
                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, RoleSeed.AdminRole);
                await _userManager.AddPasswordAsync(user, PASSWORD);
            }
        }
    }
}

