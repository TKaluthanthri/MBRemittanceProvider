using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Majority.RemittanceProvider.IdentityServer.Models
{
    public static class ServiceConfiguration
    {
        public static void AddRemittanceProviderCoreConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApplicationConfigurations>(configuration.GetSection("RemittanceProviderConfiguration"));
            services.AddSingleton(sp =>
            {
                var config = configuration.GetSection("RemittanceProviderConfiguration").Get<ApplicationConfigurations>();
                return new ApplicationConfigurations
                {
                    ClientId = config.ClientId,
                    ClientSecret = config.ClientSecret,
                    ClientName = config.ClientName,
                    ClientScope = config.ClientScope,

                };
            });

        }
    }
}
