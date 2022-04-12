using Majority.RemittanceProvider.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
        public TokenAuthenticationFilter(ApplicationConfigurations configurations)
        {
            this._appConfiguration = configurations;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            List<string> Scopes = new List<string>();
            JwtSecurityToken tokenDecryptValues = new JwtSecurityToken();
            string tokenString = GetBeaereToken(context);

            if (string.IsNullOrEmpty(tokenString))
            {
                context.Result = new UnauthorizedObjectResult(new { error = "authorization token does not contains request" });
                return;
            }
            else
            {
                //decrypt token
                tokenDecryptValues = await DecodeJwtAccessToken(tokenString);
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
            }
        }

        private string GetBeaereToken(AuthorizationFilterContext context)
        {
            var remittanceAuthorization = GetHeaderData(context, "Authorization");
            if (!string.IsNullOrEmpty(remittanceAuthorization)
                && remittanceAuthorization.Contains("Bearer")
                && remittanceAuthorization.Split(' ').Count() > 1)
            {
                return remittanceAuthorization.Split(' ')[1];
            }
            else
                return string.Empty;
        }

        private string GetHeaderData(AuthorizationFilterContext context, string name)
        {
            var data = context.HttpContext.Request.Headers[name];
            if (data.Count > 0)
                return data.First();
            else
                return string.Empty;
        }

        private async Task<JwtSecurityToken> DecodeJwtAccessToken(string token)
        {
            try
            {
                //decrypt token
                var jwtToken = token;
                var handler = new JwtSecurityTokenHandler();
                var tokenValues= handler.ReadJwtToken(jwtToken);
                return tokenValues as JwtSecurityToken;
            }
            catch (Exception ex)
            {
                throw ex;
                //TO DO need to log errors
            }

        }



    }
}
