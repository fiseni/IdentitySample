﻿using IdentitySample.Identity.Setup;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.Bind(IdentityConfig.CONFIG_NAME, IdentityConfig.Instance);
builder.Configuration.Bind(AuthConfig.CONFIG_NAME, AuthConfig.Instance);

builder.Services.AddIdentityServices(IdentityConfig.Instance);

builder.AddAppAuthentication(AuthConfig.Instance);
builder.AddAppAuthorization();

builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "IdentitySample App",
        Version = "v1",
    });
    c.AddLocalIdentity();
    c.EnableAnnotations();
});
builder.Services.AddControllers();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.InitializeIdentityDb();

app.Run();
