using Majority.RemittanceProvider.API.Common;
using Majority.RemittanceProvider.API.Models;
using Majority.RemittanceProvider.API.Services;
using Majority.RemittanceProvider.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IIdentityServerService _iIdentityServerService;
        private readonly ILogger<BankController> _logger;
        private readonly ApplicationConfigurations _appConfiguration;
        private readonly ICountryRepository _countryRepository;
        private readonly IBankRepository _bankRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(
            ILogger<BankController> logger,
            IIdentityServerService IidentityServerService,
            ApplicationConfigurations appConfiguration,
            ICountryRepository countryRepository,
            IBankRepository bankRepository,
            ITransactionRepository transactionRepository
            )
        {
            _logger = logger;
            _iIdentityServerService = IidentityServerService;
            _appConfiguration = appConfiguration;
            _countryRepository = countryRepository;
            _bankRepository = bankRepository;
            _transactionRepository = transactionRepository;
        }

        [HttpPost]
        [Route("get-exchange-rate")]

        public async Task<GenericUseCaseResult> GetExchangeRates(ExchangeRateDetailsRequest request)
        {
            GenericUseCaseResult response = new GenericUseCaseResult();
            try
            {
                if (!String.IsNullOrEmpty(request.From) && !String.IsNullOrEmpty(request.To))
                {
                    ExchangeRateResponse exchangeRate = new ExchangeRateResponse();
                    var exchangeRatesObject = await _transactionRepository.GetExchangeRates(request.To);
                    if (exchangeRatesObject != null)
                    {
                        exchangeRate.DestinationCountry = exchangeRatesObject.ToCountry;
                        exchangeRate.ExchangeRate = exchangeRatesObject.ExchangeRate;
                        exchangeRate.SourceCountry = exchangeRatesObject.FromCountry;
                        exchangeRate.ExchangeRateToken = exchangeRatesObject.ExchangeRateToken;
                    }
                    response.IsSuccess = true;
                    response.HttpStatusCode = Convert.ToInt32(ResponseCode.Success);
                    response.Result = exchangeRate;
                }
                else
                {
                    response.IsSuccess = false;
                    response.HttpStatusCode = Convert.ToInt32(ResponseCode.InvalidRequest);
                    response.Result = new { Error = "Failed to get ExchangeRates" };
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.HttpStatusCode = Convert.ToInt32(ResponseCode.Failed);
                response.Result = new { Error = ex.Message };
                _logger.LogError("Error Bank Controller:" + ex.InnerException);
            }
            return response;
        }

        [HttpPost]
        [Route("get-transaction-state")]

        public async Task<GenericUseCaseResult> GetTransactionStatus(string transactionId)
        {
            GenericUseCaseResult response = new GenericUseCaseResult();
            try
            {
                var transaction = _transactionRepository.GetTransactionStatus(transactionId.Trim());
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Result = new { Error = ex.Message };
                _logger.LogError("Error Transaction Controller:" + ex.InnerException);
            }
            return response;
        }

        [HttpPost]
        [Route("get-fees-list")]

        public async Task<GenericUseCaseResult> GetTransactionFee(ExchangeRateDetailsRequest request)
        {
            GenericUseCaseResult response = new GenericUseCaseResult();
            try
            {
                if (!String.IsNullOrEmpty(request.From) && !String.IsNullOrEmpty(request.To))
                {
                    ExchangeRateResponse exchangeRate = new ExchangeRateResponse();
                    List<TransactionExchangeFeeResponse> exchangeFeesList = new List<TransactionExchangeFeeResponse>();

                    var exchangeRates = await _transactionRepository.GetExchangeRates(request.To);
                    var transactionAmountList = await _transactionRepository.GetTransactionFeeAmount();
                    foreach (var item in transactionAmountList)
                    {
                        TransactionExchangeFeeResponse exchangeFeeObj = new TransactionExchangeFeeResponse();
                        
                        exchangeFeeObj.Amount =  (Math.Round(item.FromAmount, 2).ToString() + " - " + Math.Round(item.ToAmount, 2).ToString());
                        exchangeFeeObj.Fee = await CalculateExchangeFee(exchangeRates.ExchangeRate, item.Fee);
                        exchangeFeesList.Add(exchangeFeeObj);
                    }
                    
                    response.IsSuccess = true;
                    response.HttpStatusCode = Convert.ToInt32(ResponseCode.Success);
                    response.Result = exchangeFeesList;
                }
                else
                {
                    response.IsSuccess = false;
                    response.HttpStatusCode = Convert.ToInt32(ResponseCode.InvalidRequest);
                    response.Result = new { Error = "Failed to get Exchange Fee" };
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Result = new { Error = ex.Message };
                _logger.LogError("Error Transaction Controller:" + ex.InnerException);
            }
            return response;
        }

        private async Task<decimal> CalculateExchangeFee(decimal Amount,decimal ExchangeFeePercentage)
        {
            decimal exchangeFees = (Amount * ExchangeFeePercentage) / 100;
            return Math.Round(exchangeFees, 2);
        }

    }
}
