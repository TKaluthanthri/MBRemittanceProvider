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
    public class CountryControllerTest
    {
        private readonly Mock<ICountryRepository> countryRepository;
        public CountryControllerTest()
        {
            countryRepository = new Mock<ICountryRepository>();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturn200Status()
        {

            countryRepository.Setup(x => x.GetAllAsync()).Returns(CountryMockData.GetCountryList());
            var controller = new CountryController(null, countryRepository.Object);
            /// Act
            var result = (GenericUseCaseResult)await controller.GetAllCountries();
            /// Assert
            result.HttpStatusCode.ShouldBe(200);
        }

        [Fact]
        public async Task GetAllCountries_ShouldReturn440FailedStatus()
        {

            countryRepository.Setup(x => x.GetAllAsync()).Returns(CountryMockData.GetEmptyCountryList());
            var controller = new CountryController(null, countryRepository.Object);
            /// Act
            var result = (GenericUseCaseResult)await controller.GetAllCountries();
            /// Assert
            result.HttpStatusCode.ShouldBe(440);
        }
    }
}
