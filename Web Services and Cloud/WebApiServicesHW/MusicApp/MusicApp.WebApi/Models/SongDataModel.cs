namespace MusicApp.WebApi.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MusicApp.Models;

    public class SongDataModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        public int? Year { get; set; }

        public Genre? Genre { get; set; }

        public int? AlbumId { get; set; }

        public virtual Album Album { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }
    }
}