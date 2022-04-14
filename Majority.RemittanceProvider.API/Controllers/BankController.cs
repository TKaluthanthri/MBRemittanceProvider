using Majority.RemittanceProvider.API.Common;
using Majority.RemittanceProvider.API.Models;
using Majority.RemittanceProvider.API.Services;
using Majority.RemittanceProvider.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.API.Controllers
{
    [Route("[controller]")]
    [ServiceFilter(typeof(TokenAuthenticationFilter))]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IIdentityServerService _iIdentityServerService;
        private readonly ILogger<BankController> _logger;
        private readonly ApplicationConfigurations _appConfiguration;
        private readonly ICountryRepository _countryRepository;
        private readonly IBankRepository _bankRepository;
        public BankController(
            ILogger<BankController> logger, 
            IIdentityServerService IidentityServerService, 
            ApplicationConfigurations appConfiguration, 
            ICountryRepository countryRepository,
            IBankRepository bankRepository
            )
        {
            _logger = logger;
            _iIdentityServerService = IidentityServerService;
            _appConfiguration = appConfiguration;
            _countryRepository = countryRepository;
            _bankRepository = bankRepository;
        } 


        [HttpPost]
        [Route("get-bank-list")]

        public async Task<GenericUseCaseResult> GetAllBanks()
        {
            GenericUseCaseResult response = new GenericUseCaseResult();
            try
            {
                var bankList = await _bankRepository.GetAllBanksAsync();
                response.HttpStatusCode = Convert.ToInt32(ResponseCode.Success);
                response.Status = Enum.GetName(Codes.Success);

                response.Result = (from bank in bankList
                                   select new { bank.Name, bank.BankCode }).ToArray();
            }
            catch (Exception ex)
            {
                response.Status = Enum.GetName(Codes.Failed);
                response.Result = new { Error = ex.Message };
                _logger.LogError("Error Bank Controller:" + ex.InnerException);
            }
            return response;
        }


        [HttpPost]
        [Route("get-beneficiary-name")]
        public async Task<GenericUseCaseResult> GetBeneficiaryName([FromBody] AccountDetailsRequest request)
        {
            GenericUseCaseResult response = new GenericUseCaseResult();
            try
            {
                if (!String.IsNullOrEmpty(request.AccountNumber) && !String.IsNullOrEmpty(request.BankCode))
                {
                    var accountDetails = await _bankRepository.GetBeneficiaryName(request.AccountNumber, request.BankCode);
                    response.HttpStatusCode = Convert.ToInt32(ResponseCode.Success);
                    response.Status = Enum.GetName(Codes.Success);
                    string fullName = (!String.IsNullOrEmpty(accountDetails.FirstName) ? accountDetails.FirstName : "")+" "+(!String.IsNullOrEmpty(accountDetails.LastName) ? accountDetails.LastName : "") ;
                    response.Result = new { accountName = fullName };
                }
                else {

                    response.Status = Enum.GetName(Codes.InvalidRequest);
                    response.Result = new { Error = "Failed to get Beneficiary Name " };
                }
            }
            catch (Exception ex)
            {
                response.Status = Enum.GetName(Codes.Failed);
                response.Result = new { Error = ex.Message };
                _logger.LogError("Error Bank Controller:" + ex.InnerException);

            }
            return response;
        }

    }
}
