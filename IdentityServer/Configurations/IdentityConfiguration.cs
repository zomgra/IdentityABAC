using Duende.IdentityServer.Models;
using Duende.IdentityServer;
using IdentityModel;

namespace IdentityServer.Configurations
{
    public static class IdentityConfiguration
    {
        public static string Admin = "Admin";
        public static string Customer = "Customer";

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            yield return new IdentityResources.OpenId();
            yield return new IdentityResources.Email();
            yield return new IdentityResources.Profile();
        }
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            yield return new ApiScope("order", "Order Server");
        }
        public static IEnumerable<Client> GetClients()
        {
            
            yield return new Client
            {
                ClientId = "client.mvc",
                ClientSecrets = { new Secret("secret".ToSha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:7076/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:7076/signout-callback-oidc" },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "order"
                },
            };
        }
    }
}
