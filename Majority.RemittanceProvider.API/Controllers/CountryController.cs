﻿using Majority.RemittanceProvider.API.Common;
using Majority.RemittanceProvider.API.Models;
using Majority.RemittanceProvider.API.Services;
using Majority.RemittanceProvider.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IIdentityServerService _iIdentityServerService;
        private readonly ILogger<BankController> _logger;
        private readonly ApplicationConfigurations _appConfiguration;
        private readonly ICountryRepository _countryRepository;

        public CountryController(ILogger<BankController> logger, IIdentityServerService IidentityServerService, ApplicationConfigurations appConfiguration, ICountryRepository countryRepository)
        {
            _logger = logger;
            _iIdentityServerService = IidentityServerService;
            _appConfiguration = appConfiguration;
            _countryRepository = countryRepository;
        }


        [HttpPost]
        [Route("get-country-list")]

        public async Task<GenericUseCaseResult> GetAllCountries()
        {
            GenericUseCaseResult response = new GenericUseCaseResult();
            try
            {
                var countryList = await _countryRepository.GetAllAsync();
                response.HttpStatusCode = Convert.ToInt32(ResponseCode.Success);
                response.IsSuccess = true;
                if (countryList.Count > 0)
                {
                    response.Result = (from country in countryList
                                       select new { country.Name, country.Code }).ToArray();
                }
                else
                {
                    response.Result = new { message = "No Available countries" };
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Result = new { Error = ex.Message };
                _logger.LogError("Error Country Controller:" + ex.InnerException);
            }
            return response;
        }


        [HttpPost]
        [Route("get-state-list")]

        public async Task<GenericUseCaseResult> GetAllStates()
        {
            GenericUseCaseResult response = new GenericUseCaseResult();
            try
            {
                var stateList = await _countryRepository.GetAllStatesAsync();
                response.HttpStatusCode = Convert.ToInt32(ResponseCode.Success);
                response.IsSuccess = true;
                if (stateList.Count > 0)
                {
                    response.Result = (from state in stateList
                                       select new { state.Name, state.Code }).ToArray();
                }
                else
                {
                    response.Result = new { message = "No Available states" };
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Result = new { Error = ex.Message };
                _logger.LogError("Error Country Controller:" + ex.InnerException);
            }
            return response;
        }
    }
}