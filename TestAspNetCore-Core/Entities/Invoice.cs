using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAspNetCore_Core.Entities
{
    public class Invoice : BaseEntity
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        [MaxLength(70)]
        public string BillingAddress { get; set; }

        [MaxLength(40)]
        public string BillingCity { get; set; }

        [MaxLength(40)]
        public string BillingState { get; set; }

        [MaxLength(40)]
        public string BillingCountry { get; set; }

        [MaxLength(10)]
        public string BillingPostalCode { get; set; }

        [Required]
        public Decimal Total { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        
        public ICollection<InvoiceLine> InvoiceLines { get; set; }
    }
}