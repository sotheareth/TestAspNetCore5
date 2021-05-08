using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestAspNetCore_Core.Entities
{
    public class PlaylistTrack : BaseEntity
    {
        [Key, Column(Order = 2)]
        public int TrackId { get; set; }

        [ForeignKey("PlaylistId")]
        public Playlist Playlist { get; set; }

        [ForeignKey("TrackId")]
        public Track Track { get; set; }
    }
}
