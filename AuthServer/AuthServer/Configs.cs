using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServer4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthServer
{
    public class Configs
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
            new Client {
                ClientId = "oauthClient",
                ClientName = "Example Client Credentials Client Application",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = new List<Secret> {
                    new Secret("superSecretPassword".Sha256())},
                AllowedScopes = new List<string> {"customAPI.read"}
            }, new Client {
                    ClientId = "openIdConnectClient",
                    ClientName = "Example Implicit Client Application",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "role",
                        "customAPI.write"
                    },
                RedirectUris = new List<string> { "http://localhost:4200/auth-callback" },
                PostLogoutRedirectUris = new List<string> { "http://localhost:4200/" },
                AllowedCorsOrigins = new List<string> { "http://localhost:4200" },
                AllowAccessTokensViaBrowser = true
}
        };
        }
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource> {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource {
                Name = "role",
                UserClaims = new List<string> {"role"}
            }
        };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> {
            new ApiResource {
                Name = "customAPI",
                DisplayName = "Custom API",
                Description = "Custom API Access",
                UserClaims = new List<string> {"role"},
                ApiSecrets = new List<Secret> {new Secret("scopeSecret".Sha256())},
                Scopes = new List<Scope> {
                    new Scope("customAPI.read"),
                    new Scope("customAPI.write")
                }
            }
        };
        }
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser> {
            new TestUser {
                SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                Username = "raghav",
                Password = "sharma",
                Claims = new List<Claim> {
                    new Claim(JwtClaimTypes.Email, "raghavs280@gmail.com"),
                    new Claim(JwtClaimTypes.Role, "admin")
                }
            }
        };
        }
    }
}
