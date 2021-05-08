using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAspNetCore_Core.Entities
{
    public class Discount
    {
        public int MinimumItemCount { get; set; }
        public double MinimumBillAmount { get; set; }
        public double Percentage { get; set; }
    }
}
