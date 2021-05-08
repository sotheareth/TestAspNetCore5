using System;

namespace TestAspNetCore_Core.Dtos.Requests
{

    public class RegisterCustomerRequestDto
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Company { get; set; }

        public AddressRequestDto Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public String Tag { get; set; }

        public EmployeeRequestDto SupportRep { get; set; }
    }
}
