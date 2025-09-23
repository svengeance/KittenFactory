using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace KittenFactory.Api.Infrastructure.Constants;

public static partial class SeedData
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
}
