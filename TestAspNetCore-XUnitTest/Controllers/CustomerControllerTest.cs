using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TestAspNetCore;
using TestAspNetCore_Core.Entities;
using Xunit;
using FluentAssertions;
using System.Net;

namespace TestAspNetCore_XUnitTest.Controllers
{
    public class CustomerControllerTest : IntegrationTestBase
    {
        public CustomerControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {

        }

        [Fact]
        public async Task GetCustomerTest()
        {
            //Arrange
            _customerContext.Customer.RemoveRange(await _customerContext.Customer.ToListAsync());
            await _customerContext.SaveChangesAsync();

            var manager = new Employee
            {
                FirstName = "manager",
                LastName = "manager",
                Title = "Mr.",
                BirthDate = DateTime.Now,
                Email = "manager@gmail.com"
            };
            var employee = new Employee
            {
                FirstName = "test",
                LastName = "test",
                Title = "Mr.",
                BirthDate = DateTime.Now,
                Email = "sotheareth.ham@gmail.com"
            };
            var customer = new Customer
            {
                SupportRep = employee,
                Address = new Address
                {
                    City = "test",
                    Street = "test"
                },
                City = "test",
                Company = "test",
                Country = "Cambodia",
                Email = "sothearet.ham@gmail.com",
                FirstName = "test",
                LastName = "test",
                Tag = "test"
            };
            var request = _client.BuildPostRequest<Customer>("/Customers", customer);
            var response = await _client.SendAsync(request);
            response.StatusCode.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            //Action
            var result = await _client.GetRequest<Customer>("/customers/2");
            var customers = await _customerContext.Customer.ToListAsync();

            //Assert
            result.Should().NotBeNull();
        }

    }
}
