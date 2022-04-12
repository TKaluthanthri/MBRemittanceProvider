using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Majority.RemittanceProvider.IdentityServer.Models
{
    public static class ServiceConfiguration
    {
        public static void AddRemittanceProviderCoreConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RemittanceProviderConfiguration>(configuration.GetSection("ClientSecret"));
            services.Configure<RemittanceProviderConfiguration>(configuration.GetSection("ClientId"));
            services.Configure<RemittanceProviderConfiguration>(configuration.GetSection("ClientName"));
        }
    }
}
