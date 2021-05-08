using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAspNetCore_Core.Entities
{
    public class Track : BaseEntity
    {
        [Required, MaxLength(200)]
        public string Name { get; set; }

        public int AlbumId { get; set; }

        public int MediaTypeId { get; set; }

        public int GenreId { get; set; }

        [MaxLength(220)]
        public string Composer { get; set; }

        public int Miliseconds { get; set; }

        public int Bytes { get; set; }

        public decimal UnitPrice { get; set; }

        [ForeignKey("AlbumId")]
        public Album Album { get; set; }

        [ForeignKey("MediaTypeId")]
        public MediaType MediaType { get; set; }

        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }
    }
}