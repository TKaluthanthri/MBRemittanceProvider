using Majority.RemittanceProvider.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.API.Services
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class TokenAuthenticationFilter : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private ApplicationConfigurations _appConfiguration;
        private string clientId;
        public TokenAuthenticationFilter(ApplicationConfigurations configurations)
        {
            this._appConfiguration = configurations;
            this.clientId = configurations.ClientId;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            List<string> Scopes = new List<string>();
            JwtSecurityToken tokenDecryptValues = new JwtSecurityToken();
            string tokenString = await GetBeaereToken(context);

            if (string.IsNullOrEmpty(tokenString))
            {
                context.Result = new UnauthorizedObjectResult(new { error = "authorization token does not contains request" });
                return;
            }
            else
            {
                // Decrypt Access Token
                tokenDecryptValues = DecodeJwtAccessToken(tokenString);
                if (Convert.ToDateTime(tokenDecryptValues.ValidTo) <= DateTime.UtcNow)
                {
                    context.Result = new UnauthorizedObjectResult(new { error = "token expired" });
                    return;
                }
                if (tokenDecryptValues.Issuer != _appConfiguration.TokenIssuer)
                {
                    context.Result = new UnauthorizedObjectResult(new { error = "JWT Token has been tampered" });
                    return;
                }
                if (!tokenDecryptValues.Payload["client_id"].ToString().Contains(this.clientId))
                {
                    context.Result = new UnauthorizedObjectResult(new { error = "Invalid Application" });
                    return;
                }

            }
        }

        private async Task<string> GetBeaereToken(AuthorizationFilterContext context)
        {
            var remittanceAuthorization = await GetHeaderData(context, "Authorization");
            if (!string.IsNullOrEmpty(remittanceAuthorization)
                && remittanceAuthorization.Contains("Bearer")
                && remittanceAuthorization.Split(' ').Count() > 1)
            {
                return remittanceAuthorization.Split(' ')[1];
            }
            else
                return string.Empty;
        }

        private async Task<string> GetHeaderData(AuthorizationFilterContext context, string name)
        {
            var data =  context.HttpContext.Request.Headers[name];
            if (data.Count > 0)
                return data.First();
            else
                return string.Empty;
        }

        private JwtSecurityToken DecodeJwtAccessToken(string token)
        {
            //decrypt token
            var jwtToken = token;
            var handler = new JwtSecurityTokenHandler();
            var tokenValues = handler.ReadJwtToken(jwtToken);
            return tokenValues as JwtSecurityToken;

        }

    }
}
