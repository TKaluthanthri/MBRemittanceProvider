
using Majority.RemittanceProvider.API.Controllers;
using Majority.RemittanceProvider.API.Models;
using Majority.RemittanceProvider.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Majority.RemittanceProvider.UnitTest
{
    public class BankControllerTest
    {
        private readonly Mock<IBankRepository> bankRepository;
   
        public BankControllerTest()
        {
            bankRepository = new Mock<IBankRepository>();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturn440FailedStatus()
        {
            
            bankRepository.Setup(x => x.GetAllBanksAsync()).Returns(BankMockData.GetEmptySampleBankList());
            var controller = new BankController(null, bankRepository.Object);
            /// Act
            var result = (GenericUseCaseResult)await controller.GetAllBanks();
            /// Assert
            result.HttpStatusCode.ShouldBe(440);
        }

        [Fact]
        public async Task GetAllBanks_ShouldReturn200Status()
        {

            bankRepository.Setup(x => x.GetAllBanksAsync()).Returns(BankMockData.GetSampleBankList());
            var controller = new BankController(null, bankRepository.Object);
            /// Act
            var result = (GenericUseCaseResult)await controller.GetAllBanks();
            /// Assert
            result.HttpStatusCode.ShouldBe(200);
        }
    }
}
