using IdentityServer4.Models;
using System.Collections.Generic;

namespace Majority.RemittanceProvider.IdentityServer.IdentityConfiguration
{
    public class Scopes
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("RemittanceProviderApi.read", "Read Access to RemittanceProviderApi API"),
                new ApiScope("RemittanceProviderApi.write", "Write Access to RemittanceProviderApi API"),
            };
        }
    }
}
