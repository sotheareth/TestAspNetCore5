using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestAspNetCore_Core.Entities
{
    public class Customer : BaseEntity
    {
        [Required, MaxLength(20)]
        public string FirstName { get; set; }

        [Required, MaxLength(20)]
        public string LastName { get; set; }

        [MaxLength(80)]
        public string Company { get; set; }

        [ForeignKey("AddressId")]
        public int AddressId { get; set; }

        public Address Address { get; set; }

        [MaxLength(40)]
        public string City { get; set; }

        [MaxLength(40)]
        public string State { get; set; }

        [MaxLength(40)]
        public string Country { get; set; }

        [MaxLength(10)]
        public string PostalCode { get; set; }

        [MaxLength(24)]
        public string Phone { get; set; }

        [MaxLength(24)]
        public string Fax { get; set; }

        [MaxLength(60)]
        public string Email { get; set; }

        public int SupportRepId { get; set; }

        public String Tag { get; set; }

        [ForeignKey("SupportRepId")]
        public Employee SupportRep { get; set; }

        public ICollection<Invoice> Invoices { get; set; }

    }
}
