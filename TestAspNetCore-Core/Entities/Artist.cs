using System.ComponentModel.DataAnnotations;

namespace TestAspNetCore_Core.Entities
{
    public class Artist : BaseEntity
    {

        [Required, MaxLength(120)]
        public string Name { get; set; }
    }
}