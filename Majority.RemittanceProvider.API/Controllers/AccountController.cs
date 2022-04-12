using Majority.RemittanceProvider.API.Common;
using Majority.RemittanceProvider.API.Models;
using Majority.RemittanceProvider.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.API.Controllers
{
    [ApiController]
    [Route("[api/controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityServerService _iIdentityServerService;
        private readonly ILogger<AccountController> _logger;
        private readonly ApplicationConfigurations _appConfiguration;

        public AccountController(ILogger<AccountController> logger, IIdentityServerService IidentityServerService, ApplicationConfigurations appConfiguration)
        {
            _logger = logger;
            _iIdentityServerService = IidentityServerService;
            _appConfiguration = appConfiguration;
        }

        [HttpGet]
        [Route("get-token")]
        public async Task<GenericUseCaseResult> GetToken(string Scope)
        {
            GenericUseCaseResult response = new GenericUseCaseResult();
            try
            {
                var OAuth2Token = await _iIdentityServerService.GetToken(Scope);
                if ((ResponseCode)OAuth2Token.HttpStatusCode == ResponseCode.Success)
                {
                    response.IsSuccess = true;
                    response.HttpStatusCode = Convert.ToInt32(ResponseCode.Success);
                    response.Result = new { AccessToken = OAuth2Token.AccessToken, Scope = OAuth2Token.Scope };
                }
                else
                {
                    response.IsSuccess = false;
                    response.HttpStatusCode = Convert.ToInt32(OAuth2Token.HttpStatusCode);
                    response.Result = new { Error = OAuth2Token.Error.ToString() };
                }


            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Result = new { Error = ex.Message };
                _logger.LogError("Error Account Controller:" + ex.InnerException);
            }

            return response;
        }


    }
}
