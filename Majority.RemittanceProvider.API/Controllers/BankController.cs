using Majority.RemittanceProvider.API.Models;
using Majority.RemittanceProvider.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IIdentityServerService _iIdentityServerService;
        private readonly ILogger<BankController> _logger;
        private readonly ApplicationConfigurations _appConfiguration;

        public BankController(ILogger<BankController> logger, IIdentityServerService IidentityServerService, ApplicationConfigurations appConfiguration)
        {
            _logger = logger;
            _iIdentityServerService = IidentityServerService;
            _appConfiguration = appConfiguration;
        }

        
        [HttpPost]
        [Route("get-bank-list")]
        [Authorize(Roles = "IRSUser, IRSAdmin")]
        public async Task<GenericUseCaseResult> GetAllBanks([FromBody] string countryName)
        {
            GenericUseCaseResult response = new GenericUseCaseResult();
            try
            {

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Result = new { Error = ex.Message };
                _logger.LogError("Error Bank Controller:" + ex.InnerException);

            }
            return response;
        }


        [HttpPost]
        [Route("get-beneficiary-name")]
        [Authorize(Roles = "IRSUser, IRSAdmin")]
        public async Task<GenericUseCaseResult> GetBeneficiaryName(string Scope)
        {
            GenericUseCaseResult response = new GenericUseCaseResult();
            try
            {

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Result = new { Error = ex.Message };
                _logger.LogError("Error Bank Controller:" + ex.InnerException);

            }
            return response;
        }
        
    }
}
