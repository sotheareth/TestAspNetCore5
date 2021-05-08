using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAspNetCore_Core.Entities
{
    public class Playlist : BaseEntity
    {

        [Required, MaxLength(120)]
        public string Name { get; set; }
    }
}
