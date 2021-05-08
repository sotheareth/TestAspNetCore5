using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAspNetCore_Core.Entities
{
    public class InvoiceLine : BaseEntity
    {
        public int? InvoiceId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int TrackId { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [ForeignKey("TrackId")]
        public Track Track { get; set; }

        
        public Invoice Invoice { get; set; }
    }
}