using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Threading.Tasks;
using TestAspNetCore_Core.Contract;
using TestAspNetCore_Core.Entities;
using TestAspNetCore_Core.Interfaces;
using TestAspNetCore_Infrastructure.Service;
using Xunit;

namespace TestAspNetCore_XUnitTest.Services
{
    public class CustomerServiceUsingNSubstituteTest
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerService> _logger = Substitute.For<ILogger<CustomerService>>();
        private readonly ICustomerRepository _customerRepository = Substitute.For<ICustomerRepository>();

        public CustomerServiceUsingNSubstituteTest()
        {
            _customerService = new CustomerService(_customerRepository, _logger);
        }

        [Fact]
        public async Task TestGetCustomerById()
        {
            //Arrange
            var customerId = 2;
            var customerResponse = new Customer
            {
                Id = 2,
                FirstName = "test"
            };

            _customerRepository.GetCustomerById(customerId).Returns(customerResponse);

            //Act
            var result = await _customerService.GetCustomerById(customerId);

            //Assert
            Assert.Equal(customerId, result.Id);
        }

        [Fact]
        public async Task TestGetCustomerById_WhenNotExistCustomer()
        {
            //Arrange
            _customerRepository.GetCustomerById(Arg.Any<int>()).ReturnsNull();

            //Act
            var result = await _customerService.GetCustomerById(1);

            //Assert
            Assert.Null(result);
        }
    }
}
