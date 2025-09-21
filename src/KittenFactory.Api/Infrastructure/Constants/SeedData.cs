using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using KittenFactory.Api.Features.Users.Entities;

namespace KittenFactory.Api.Infrastructure.Constants;

public static class SeedData
{
    public static class Identity
    {
        public static class Clients
        {
            public static string SwaggerClientSecret = "swagger";

            public static Client Swagger = new()
            {
                ClientId = "swagger",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = [new Secret("swagger".Sha256())],
                AllowedScopes = [IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, ApiScopes.KittensFactoryApiScope.Name],
                AlwaysIncludeUserClaimsInIdToken = true
            };
        }

        public static class ApiScopes
        {
            public static ApiScope KittensFactoryApiScope = new("kittens-factory-api", "Kittens Factory API");
        }

        public static class ApiResources
        {
            public static ApiResource KittensFactoryApi = new(ApiScopes.KittensFactoryApiScope.Name, ApiScopes.KittensFactoryApiScope.DisplayName!)
            {
                Scopes = [ApiScopes.KittensFactoryApiScope.Name]
            };
        }
    }

    public static class Users
    {
        public static string TestUserPassword = "Kittens123!";

        public static User TestUser = new()
        {
            Id = Guid.Empty.ToString(),
            UserName = "TestUser",
            Email = "TestUser@kittens.com"
        };
    }
}
