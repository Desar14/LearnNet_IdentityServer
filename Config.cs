using Duende.IdentityServer.Models;
using System.Security.Claims;

namespace LearnNet_IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("role", new [] { ClaimTypes.Role })
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(name: "manager.read"),
                new ApiScope(name: "manager.create"),
                new ApiScope(name: "manager.update"),
                new ApiScope(name: "manager.delete"),

                new ApiScope(name: "buyer.read"),

            };

        public static IEnumerable<ApiResource> ApiResources => 
            new List<ApiResource>
            {
                new ApiResource("urn:catalog", "Catalog service API")
                {
                    Scopes = { "buyer.read", "manager.read", "manager.create", "manager.update", "manager.delete" },

                    RequireResourceIndicator = true,

                    UserClaims =
                    {
                        ClaimTypes.Role
                    }
                },

                new ApiResource("urn:carting", "Carting Service API")
                {
                    Scopes = { "buyer.read", "manager.read" },

                    RequireResourceIndicator = true,

                    UserClaims =
                    {
                        ClaimTypes.Role
                    }
                }
            };
        

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "ApiClient_Manager",
                    ClientSecrets = { new Secret("49C1A7E1-AAAA-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    RedirectUris = { "https://localhost:44300/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "role", "manager.read", "manager.create", "manager.update", "manager.delete" },

                    AlwaysIncludeUserClaimsInIdToken = true,
                    AlwaysSendClientClaims = true,
                },

                new Client
                {
                    ClientId = "ApiClient_Buyer",
                    ClientSecrets = { new Secret("49C1A7E1-BBBB-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    RedirectUris = { "https://localhost:44300/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "role", "buyer.read" },

                    AlwaysIncludeUserClaimsInIdToken = true,
                    AlwaysSendClientClaims = true,
                },
            };
    }
}
