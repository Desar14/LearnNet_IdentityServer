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
                new ApiScope(name: "products.read"),
                new ApiScope(name: "products.create"),
                new ApiScope(name: "products.update"),
                new ApiScope(name: "products.delete"),

                new ApiScope(name: "categories.read"),
                new ApiScope(name: "categories.create"),
                new ApiScope(name: "categories.update"),
                new ApiScope(name: "categories.delete"),

                new ApiScope(name: "carts.read"),
                new ApiScope(name: "carts.create"),
                new ApiScope(name: "carts.update"),
                new ApiScope(name: "carts.delete"),
            };

        public static IEnumerable<ApiResource> ApiResources => 
            new List<ApiResource>
            {
                new ApiResource("urn:catalog_categories", "Catalog service API: Categories")
                {
                    Scopes = { "categories.read", "categories.create", "categories.update", "categories.delete" },

                    RequireResourceIndicator = true,

                    UserClaims =
                    {
                        ClaimTypes.Role
                    }
                },

                new ApiResource("urn:catalog_products", "Catalog service API: Products")
                {
                    Scopes = { "products.read", "products.create", "products.update", "products.delete" },

                    RequireResourceIndicator = true,

                    UserClaims =
                    {
                        ClaimTypes.Role
                    }
                },

                new ApiResource("urn:carting", "Carting Service API")
                {
                    Scopes = { "carts.read", "carts.create", "carts.update", "carts.delete" },

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
                    ClientId = "ApiClient_categories",
                    ClientSecrets = { new Secret("49C1A7E1-AAAA-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    RedirectUris = { "https://localhost:44300/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "role", 
                        "products.read", "products.create", "products.update", "products.delete",
                        "categories.read", "categories.create", "categories.update", "categories.delete",
                        "carts.read", "carts.create", "carts.update", "carts.delete"},

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
                    AllowedScopes = { "openid", "profile", "role", "products.read",  "categories.read", "carts.read", "carts.create", "carts.update", "carts.delete" },

                    AlwaysIncludeUserClaimsInIdToken = true,
                    AlwaysSendClientClaims = true,
                },
            };
    }
}
