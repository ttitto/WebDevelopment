namespace MusicApp.WebApi.Models
{
    using System.Collections.Generic;

    using MusicApp.Models;

    public class SongViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int? Year { get; set; }

        public Genre? Genre { get; set; }

        public int? AlbumId { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }
    }
}