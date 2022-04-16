using IdentityServer4.Models;
using System.Collections.Generic;

namespace Majority.RemittanceProvider.IdentityServer.IdentityConfiguration

{
    public class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };
        }


        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource
                {
                    Name = "RemittanceProviderApi",
                    DisplayName = "RemittanceProvider Api",
                    Description = "Allow the application to access RemittanceProvider Api on your behalf",
                    Scopes = new List<string> { "RemittanceProvider.read", "RemittanceProvider.write"},
                    ApiSecrets = new List<Secret> {new Secret("Test71263718".Sha256())},
                    UserClaims = new List<string> {"role"}
                }
            };
        }
    }
}
