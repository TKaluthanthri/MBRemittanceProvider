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
    [ServiceFilter(typeof(TokenAuthenticationFilter))]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private readonly ILogger<BankController> _logger;
        private readonly ApplicationConfigurations _appConfiguration;
        private readonly ICountryRepository _countryRepository;
        private readonly IBankRepository _bankRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICustomerRepository _customerRepository;

        public TransactionController(
            ILogger<BankController> logger,
            ApplicationConfigurations appConfiguration,
            ICountryRepository countryRepository,
            IBankRepository bankRepository,
            ITransactionRepository transactionRepository,
            ICustomerRepository customerRepository
            )
        {
            _logger = logger;
            _appConfiguration = appConfiguration;
            _countryRepository = countryRepository;
            _bankRepository = bankRepository;
            _transactionRepository = transactionRepository;
            _customerRepository = customerRepository;
        }

        [HttpPost]
        [Route("get-exchange-rate")]

        public async Task<GenericUseCaseResult> GetExchangeRates([FromBody] ExchangeRateDetailsRequest request)
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
                    response.Status = Enum.GetName(Codes.Success);
                    response.HttpStatusCode = Convert.ToInt32(ResponseCode.Success);
                    response.Result = exchangeRate;
                }
                else
                {
                    response.Status = Enum.GetName(Codes.Failed);
                    response.HttpStatusCode = Convert.ToInt32(ResponseCode.InvalidRequest);
                    response.Result = new { Error = "Failed to get ExchangeRates" };
                }
            }
            catch (Exception ex)
            {
                response.Status = Enum.GetName(Codes.Failed);
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
                var transaction = await _transactionRepository.GetTransactionStatusById(transactionId.Trim());
                response.HttpStatusCode = Convert.ToInt32(ResponseCode.Success);
                response.Status = Enum.GetName(Codes.Success);
                string status = string.Empty;
                switch ((TransactionStatus)transaction.Status)
                {
                    case TransactionStatus.Completed:
                        status = Enum.GetName(Status.Completed);
                        break;
                    case TransactionStatus.Pending:
                        status = Enum.GetName(Status.Pending);
                        break;
                    case TransactionStatus.Declined:
                        status = Enum.GetName(Status.Declined);
                        break;
                    case TransactionStatus.Canceled:
                        status = Enum.GetName(Status.Canceled);
                        break;
                    default:
                        status = "Error";
                        break;
                }


                response.Result = new { transactionId = transaction.TransactionId, status = status };
            }
            catch (Exception ex)
            {
                response.Status = Enum.GetName(Codes.Failed);
                response.Result = new { Error = ex.Message };
                _logger.LogError("Error Transaction Controller:" + ex.InnerException);
            }
            return response;
        }

        [HttpPost]
        [Route("get-fees-list")]

        public async Task<GenericUseCaseResult> GetTransactionFee([FromBody] ExchangeRateDetailsRequest request)
        {
            GenericUseCaseResult response = new GenericUseCaseResult();
            try
            {
                if (!String.IsNullOrEmpty(request.From) && !String.IsNullOrEmpty(request.To))
                {
                    if (request.From == "US")
                    {
                        ExchangeRateResponse exchangeRate = new ExchangeRateResponse();
                        List<TransactionExchangeFeeResponse> exchangeFeesList = new List<TransactionExchangeFeeResponse>();

                        var exchangeRates = await _transactionRepository.GetExchangeRates(request.To);
                        var transactionAmountList = await _transactionRepository.GetTransactionFeeAmount();
                        foreach (var item in transactionAmountList)
                        {
                            TransactionExchangeFeeResponse exchangeFeeObj = new TransactionExchangeFeeResponse();

                            exchangeFeeObj.Amount = (Math.Round(item.FromAmount, 2).ToString() + " - " + Math.Round(item.ToAmount, 2).ToString());
                            exchangeFeeObj.Fee = CalculateExchangeFee(exchangeRates.ExchangeRate, item.Fee);
                            exchangeFeesList.Add(exchangeFeeObj);
                        }

                        response.Status = Enum.GetName(Codes.Success);
                        response.HttpStatusCode = Convert.ToInt32(ResponseCode.Success);
                        response.Result = exchangeFeesList;
                    }
                    else
                    {
                        response.Status = Enum.GetName(Codes.Forbidden);
                        response.HttpStatusCode = Convert.ToInt32(ResponseCode.InvalidRequest);
                        response.Result = new { Error = "All Fee calculated based from US" };
                    }
                }
                else
                {
                    response.Status = Enum.GetName(Codes.InvalidRequest);
                    response.HttpStatusCode = Convert.ToInt32(ResponseCode.InvalidRequest);
                    response.Result = new { Error = "Failed to get Exchange Fee" };
                }
            }
            catch (Exception ex)
            {
                response.Status = Enum.GetName(Codes.Failed);
                response.Result = new { Error = ex.Message };
                _logger.LogError("Error Transaction Controller:" + ex.InnerException);
            }
            return response;
        }



        [HttpPost]
        [Route("submit-transaction")]
        public async Task<GenericUseCaseResult> SubmitTransaction(CommitTransactionRequest request)
        {
            GenericUseCaseResult response = new GenericUseCaseResult();
            try
            {
                var existingCustomer = await _customerRepository.GetCustomerByEmail(request.SenderEmail);
                var bankAccount = await _bankRepository.GetBeneficiaryName(request.ToBankAccountNumber, request.ToBankCode);
                string transfereeId = string.Empty;
                string transferId = string.Empty;
                bool isCustomerExist = existingCustomer != null ? true : false;
                bool isBankAccountExist = bankAccount != null ? true : false;
                if (!isCustomerExist)
                {
                    Core.Entities.Customer customer = new Core.Entities.Customer();
                    customer.FirstName = request.SenderFirstName;
                    customer.LastName = request.SenderLastName;
                    customer.PhoneNumber = request.SenderPhone;
                    customer.State = request.SendFromState;
                    customer.Country = request.SenderCountry;
                    customer.AddressLine = request.SenderAddress;
                    customer.Email = request.SenderEmail;
                    customer.dateOfBirth = request.DateOfBirth;
                    customer.PostalCode = request.SenderPostalCode;
                    customer.CreatedDate = DateTime.Now;
                    customer.CustomerCode = Guid.NewGuid().ToString();
                    isCustomerExist = await _customerRepository.SaveCustomer(customer);
                    transferId = (await _customerRepository.GetCustomerByEmail(request.SenderEmail)).Id.ToString();
                }
                else
                {
                    transferId = existingCustomer.Id.ToString();
                }
                if (!isBankAccountExist)
                {
                    var bankDetails = await _bankRepository.GetBankByCodeAsync(request.ToBankCode);
                    bool isBankExist = bankDetails == null ? false : true;
                    if (!isBankExist)
                    {
                        response.HttpStatusCode = Convert.ToInt32(ResponseCode.InvalidRequest);
                        response.Result = new { Error = "Failed : Bank not Exist" };
                        return response;
                    }
                    else
                    {

                        Core.Entities.Account account = new Core.Entities.Account();
                        account.AccountNumber = request.ToBankAccountNumber;
                        account.BankAccountName = request.ToBankAccountName;
                        account.CreatedDate = DateTime.Now;
                        account.UpdatedDate = DateTime.Now;
                        account.Bank_Id = bankDetails.Id;
                        account.FirstName = request.ToBankAccountName;
                        isBankAccountExist = await _bankRepository.SaveBankAccount(account);
                        transfereeId = (await _bankRepository.GetBeneficiaryName(account.AccountNumber, request.ToBankCode)).Id.ToString();

                    }
                }
                else
                {
                    transfereeId = bankAccount.Id.ToString();
                }

                if (isBankAccountExist && isCustomerExist)
                {
                    var isExistingtransaction = await _transactionRepository.GetTransactionStatusByTransactionNumber(request.TransactionNumber);

                    if (isExistingtransaction == null)
                    {
                        Core.Entities.TransactionInfo transactionDetails = new Core.Entities.TransactionInfo();
                        transactionDetails.TransactionNumber = request.TransactionNumber;
                        transactionDetails.TransactionId = Guid.NewGuid().ToString();
                        transactionDetails.Amount = Convert.ToDecimal(request.FromAmount);
                        transactionDetails.ExchangeRate = request.ExchangeRate;
                        transactionDetails.Fees = Convert.ToDecimal(request.Fees);
                        transactionDetails.CreatedDate = DateTime.Now;
                        transactionDetails.Status = (System.Transactions.TransactionStatus)TransactionStatus.Pending;
                        transactionDetails.Country = request.ToCountry;
                        transactionDetails.SenderId = transferId;
                        transactionDetails.ReceiverId = transfereeId;

                        await _transactionRepository.SaveTransactionDetails(transactionDetails);
                        var successTransactionInfo = await _transactionRepository.GetTransactionStatusById(transactionDetails.TransactionId);

                        response.HttpStatusCode = Convert.ToInt32(ResponseCode.Created);
                        response.Status = Enum.GetName(Codes.Success);
                        response.Result = new { transactionId = successTransactionInfo.TransactionId, transactionStatus = Status.Pending + " " + "payout to beneficiary" };
                    }
                    else
                    {
                        response.HttpStatusCode = Convert.ToInt32(ResponseCode.Success);
                        response.Status = Enum.GetName(Codes.Success);
                        response.Result = new { transactionId = isExistingtransaction.TransactionId };
                    }
                }
                else
                {
                    response.Status = Enum.GetName(Codes.Failed);
                    response.HttpStatusCode = Convert.ToInt32(ResponseCode.Failed);
                    response.Result = new { Error = "Failed to Save Customer Details" };
                }
            }
            catch (Exception ex)
            {
                response.Status = Enum.GetName(Codes.Failed);
                response.Result = new { Error = ex.Message };
                _logger.LogError("Error Transaction Controller:" + ex.InnerException);
            }
            return response;
        }


        private decimal CalculateExchangeFee(decimal Amount, decimal ExchangeFeePercentage)
        {
            decimal exchangeFees = (Amount * ExchangeFeePercentage) / 100;
            return Math.Round(exchangeFees, 2);
        }

    }
}
