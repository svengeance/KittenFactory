using KittenFactory.Api.Infrastructure.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace KittenFactory.Api.Infrastructure;

public static class AuthHostBuilderExtensions
{
    public static void AddKittenFactoryAuth(this WebApplicationBuilder builder, string apiBaseUrl)
    {
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(j =>
            {
                j.Authority = apiBaseUrl;
                j.MapInboundClaims = false;
                j.TokenValidationParameters.ValidIssuers = [apiBaseUrl];
                j.TokenValidationParameters.ValidAudiences = [SeedData.Identity.ApiResources.KittensFactoryApi.Name];
            });

        builder.Services
            .AddAuthorizationBuilder()
            .AddDefaultPolicy("default", p =>
            {
                p.RequireAuthenticatedUser();
                p.AddAuthenticationSchemes(IdentityConstants.ApplicationScheme, JwtBearerDefaults.AuthenticationScheme);
            });
    }

    public static void UseKittenFactoryAuth(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
