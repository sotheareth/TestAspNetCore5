using Swashbuckle.AspNetCore.Filters;
using TestAspNetCore_Core.Dtos.Responses;

namespace TestAspNetCore.Swagger.Responses
{
    public class CustomerResponseDtoExample : IExamplesProvider<CustomerResponseDto>
    {
        public CustomerResponseDto GetExamples()
        {
            return new CustomerResponseDto()
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
                AddressId = 1,
                SupportRepId = 1
            };
        }
    }
}
