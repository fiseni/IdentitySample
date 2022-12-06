using IdentitySample.Identity.Setup;
using IdentitySample.Identity.Setup.Authentication;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.Bind(IdentityConfig.CONFIG_NAME, IdentityConfig.Instance);
builder.Configuration.Bind(AuthConfig.CONFIG_NAME, AuthConfig.Instance);

builder.Services.AddIdentityServices(IdentityConfig.Instance);
builder.AddLocalAuthentication(AuthConfig.Instance);
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
