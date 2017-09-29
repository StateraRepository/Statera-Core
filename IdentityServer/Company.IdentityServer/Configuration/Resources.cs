using System.Collections.Generic;
using IdentityServer4.Models;

namespace Company.IdentityServer.Example.Configuration
{
    internal class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                },
                new IdentityResource
                {
                    Name = "accountapi"
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "accountapi",
                    DisplayName = "Account API",
                    Description = "Account API Access",
                    UserClaims = new List<string> {"role"},
                    //ApiSecrets = new List<Secret> {new Secret("scopeSecret".Sha256())},
                    Scopes = new List<Scope>
                    {
                        new Scope("accountApi.Read"),
                        new Scope("accountApi.Write")
                    }
                }
            };
        }
    }
}