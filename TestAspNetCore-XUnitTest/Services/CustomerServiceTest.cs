using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using TestAspNetCore_Core.Contract;
using TestAspNetCore_Core.Entities;
using TestAspNetCore_Core.Interfaces;
using TestAspNetCore_Infrastructure.Service;
using Xunit;

namespace TestAspNetCore_XUnitTest.Services
{
    public class CustomerServiceTest
    {
        private readonly ICustomerService _customerService;
        private readonly Mock<ILogger<CustomerService>> _logger = new Mock<ILogger<CustomerService>>();
        private readonly Mock<ICustomerRepository> _customerRepository = new Mock<ICustomerRepository>();

        public CustomerServiceTest()
        {
            _customerService = new CustomerService(_customerRepository.Object, _logger.Object);
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
            _customerRepository.Setup(x => x.GetCustomerById(customerId))
                .ReturnsAsync(customerResponse);

            //Act
            var result = await _customerService.GetCustomerById(customerId);

            //Assert
            Assert.Equal(customerId, result.Id);
        }

        [Fact]
        public async Task TestGetCustomerById_WhenNotExistCustomer()
        {
            //Arrange
            _customerRepository.Setup(x => x.GetCustomerById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _customerService.GetCustomerById(1);

            //Assert
            Assert.Null(result);
        }
    }
}
