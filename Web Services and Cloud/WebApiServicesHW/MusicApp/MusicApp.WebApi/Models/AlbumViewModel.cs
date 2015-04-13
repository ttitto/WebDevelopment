namespace MusicApp.WebApi.Models
{
    using System.Collections.Generic;

    using MusicApp.Models;

    public class AlbumViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int? Year { get; set; }

        public string Producer { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}