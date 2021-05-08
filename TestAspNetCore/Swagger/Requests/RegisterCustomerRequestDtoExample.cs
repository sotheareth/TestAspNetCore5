using Swashbuckle.AspNetCore.Filters;
using System;
using TestAspNetCore_Core.Dtos.Requests;

namespace TestAspNetCore.Swagger.Requests
{
    public class RegisterCustomerRequestDtoExample : IExamplesProvider<RegisterCustomerRequestDto>
    {
        public RegisterCustomerRequestDto GetExamples()
        {
            return new RegisterCustomerRequestDto()
            {
                FirstName = "test",
                LastName = "test",
                City = "PP",
                Company = "test",
                Country = "Cambodia",
                Email = "test@yahoo.com",
                Fax = "85523 123456",
                Phone = "85592 464906",
                PostalCode = "855",
                State = "PP",
                Tag = "test",
                Address = new AddressRequestDto
                {
                    City = "PP",
                    Street = "144"
                },
                SupportRep = new EmployeeRequestDto()
                {
                    BirthDate = new DateTime(2010, 1, 1, 0, 0, 0),
                    City = "PP",
                    Country = "Cambodia",
                    Email = "test@yahoo.com",
                    Fax = "",
                    FirstName = "test",
                    LastName = "test",
                    Phone = "",
                    HireDate = DateTime.UtcNow,
                    State = "",
                    PostalCode = "855",
                    Title = "Mr."
                }
            };
        }

    }
}
