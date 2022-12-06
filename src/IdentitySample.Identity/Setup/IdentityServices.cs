using IdentitySample.Identity.Api.Contracts;
using IdentitySample.Identity.Api.Services;
using IdentitySample.Identity.Domain;
using IdentitySample.Identity.Infrastructure;
using IdentitySample.Identity.Setup.Authentication;
using IdentitySample.Identity.Setup.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IdentitySample.Identity.Setup;

public static class IdentityServices
{
    public static async Task InitializeIdentityDb(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var identityDbInitializer = scope.ServiceProvider.GetRequiredService<AppIdentityDbInitializer>();
            await identityDbInitializer.Seed();
        }

        await InitializePermissions(host);
    }

    public static async Task InitializePermissions(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<IdentityMarker>>();

            try
            {
                var permissionValidator = scope.ServiceProvider.GetRequiredService<IPermissionValidator>();
                var roleService = scope.ServiceProvider.GetRequiredService<IRoleService>();
                var roles = await roleService.GetRolesAsync(CancellationToken.None);
                permissionValidator.UpdateCache(roles);
            }
            catch (Exception ex)
            {
                logger.LogError("System critical error thrown during Permissions initialization : " + ex.Message);
                throw;
            }
        }
    }

    public static void AddIdentityServices(this IServiceCollection services, IdentityConfig identityConfig)
    {
        services.AddAutoMapper(typeof(IdentityMarker).Assembly);

        services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(identityConfig.ConnectionString));
        services.AddScoped<AppIdentityDbInitializer>();

        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        });

        services.AddSingleton<IPermissionValidator>(services => PermissionValidator.Instance);
        services.AddSingleton<ITokenHandler, TokenHandler>();
        services.AddSingleton<ITokenCache, TokenCacheMemory>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IAccountService, AccountService>();
    }
}
