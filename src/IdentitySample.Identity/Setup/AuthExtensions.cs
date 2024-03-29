﻿using IdentitySample.Identity.Domain;
using IdentitySample.Identity.Setup.Authentication;
using IdentitySample.Identity.Setup.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace IdentitySample.Identity.Setup;

public static class AuthExtensions
{
    public const string JWT_KEY = "This is my supper secret key for testing";

    public static AuthorizationPolicy DefaultPolicy { get; } = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    public static void AddAppAuthentication(this WebApplicationBuilder builder, AuthConfig authConfig)
    {
        var tokenOptions = authConfig.GetTokenOptions(builder.Configuration);
        builder.Services.AddSingleton(tokenOptions);

        var signingConfigurations = new SigningConfigurations(authConfig.Secret);
        builder.Services.AddSingleton(signingConfigurations);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(jwtBearerOptions =>
        {
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = authConfig.Issuer,
                ValidAudience = authConfig.Audience,
                IssuerSigningKey = signingConfigurations.SecurityKey,
                ClockSkew = TimeSpan.FromMinutes(5)
            };
        });
    }

    public static void AddAppAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options =>
        {
            options.DefaultPolicy = DefaultPolicy;

            foreach (var permissionKey in Enum.GetValues(typeof(PermissionKey)).Cast<PermissionKey>())
            {
                options.AddPolicy(permissionKey.ToString(), policy => policy.RequirePermission((int)permissionKey));
            }
        });
    }
}
