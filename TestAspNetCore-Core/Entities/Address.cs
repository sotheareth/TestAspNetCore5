using System.ComponentModel.DataAnnotations;

namespace TestAspNetCore_Core.Entities
{
    public class Address : BaseEntity
    {

        public string Street { get; set; }

        public string City { get; set; }

    }
}
