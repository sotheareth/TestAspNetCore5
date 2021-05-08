using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAspNetCore_Core.Entities
{
    public class Album : BaseEntity
    {

        [Required, MaxLength(160)]
        public string Title { get; set; }

        [Required]
        public int ArtistId { get; set; }

        [ForeignKey("ArtistId")]
        public Artist Artist { get; set; }
    }
}