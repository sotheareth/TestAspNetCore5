using System.ComponentModel.DataAnnotations;

namespace TestAspNetCore_Core.Entities
{
    public class Genre : BaseEntity
    {
        [MaxLength(120)]
        public string Name { get; set; }
    }
}