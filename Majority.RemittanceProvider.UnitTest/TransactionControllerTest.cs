using Majority.RemittanceProvider.API.Controllers;
using Majority.RemittanceProvider.API.Models;
using Majority.RemittanceProvider.Application.Interfaces;
using Majority.RemittanceProvider.UnitTest.MockData;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Majority.RemittanceProvider.UnitTest
{
    public class TransactionControllerTest
    {
        private readonly Mock<ICountryRepository> countryRepository;
        private readonly Mock<IBankRepository> _bankRepository;
        private readonly Mock<ITransactionRepository> _transactionRepository;
        private readonly Mock<ICustomerRepository> _customerRepository;

        public TransactionControllerTest()
        {
            countryRepository = new Mock<ICountryRepository>();
            _bankRepository = new Mock<IBankRepository>();
            _transactionRepository = new Mock<ITransactionRepository>();
            _customerRepository = new Mock<ICustomerRepository>();
        }

        [Fact]
        public async Task GetTransactionStatus_ShouldReturn200Status()
        {
            ExchangeRateDetailsRequest requestData = new ExchangeRateDetailsRequest{ From = "US", To = "SE" };
            _transactionRepository.Setup(x => x.GetExchangeRates(requestData.To)).Returns(TransactionMockData.GetExchangeRates());
            var controller = new TransactionController(null,null, countryRepository.Object, _bankRepository.Object, _transactionRepository.Object, _customerRepository.Object);
            /// Act
            var result = (GenericUseCaseResult)await controller.GetExchangeRates(requestData);
            /// Assert
            result.HttpStatusCode.ShouldBe(200);
        }

       
    }
}
