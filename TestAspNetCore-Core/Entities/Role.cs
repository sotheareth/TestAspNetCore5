using System;
using System.Collections.Generic;
using System.Text;

namespace TestAspNetCore_Core.Entities
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
}
