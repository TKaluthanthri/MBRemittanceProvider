using IdentityModel.Client;
using Majority.RemittanceProvider.API.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.API.Services
{
    public class IdentityServerService : IIdentityServerService
    {
       
        private DiscoveryDocumentResponse _discoveryDocument { get; set; }
        private ApplicationConfigurations _appConfiguration;

        public IdentityServerService(ApplicationConfigurations configurations)
        {
           
            this._appConfiguration = configurations;

            using (var client = new HttpClient())
            {
                _discoveryDocument = client.GetDiscoveryDocumentAsync(_appConfiguration.AuthorizeUrl).Result;
            }
        }

        public async Task<TokenResponse> GetToken(string apiScope)
        {
            using (var client = new HttpClient())
            {
                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = _discoveryDocument.TokenEndpoint,
                    ClientId = _appConfiguration.ClientId,
                    Scope = apiScope,
                    ClientSecret = _appConfiguration.ClientSecret
                });
                if (tokenResponse.IsError)
                {
                   //("Get Token Error: " + tokenResponse.ErrorDescription);
                }
                return tokenResponse;
            }
        }
    }
}
