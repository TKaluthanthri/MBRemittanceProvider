﻿using Majority.RemittanceProvider.API.Models;
using Majority.RemittanceProvider.API.Services;
using Majority.RemittanceProvider.Application.Interfaces;
using Majority.RemittanceProvider.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Majority.RemittanceProvider.API.Configuration
{
    public static class ServiceConfiguration
    {
        public static void AddRemittanceProviderCoreConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            services.AddSingleton(connectionString);
            services.AddSingleton<IIdentityServerService, IdentityServerService>();
            
            services.Configure<ApplicationConfigurations>(configuration.GetSection("ApplicationConfig"));
            services.AddSingleton(sp =>
            {
                var config = configuration.GetSection("ApplicationConfig").Get<ApplicationConfigurations>();
                return new ApplicationConfigurations
                {
                    ClientId = config.ClientId,
                    ClientSecret = config.ClientSecret,
                    ClientName = config.ClientName,
                    ClientScope = config.ClientScope,
                    AuthorizeUrl = config.AuthorizeUrl,
                    TokenIssuer = config.TokenIssuer
                };
            });
            services.AddScoped<TokenAuthenticationFilter>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IBankRepository, BankRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            
        }
    }
}
