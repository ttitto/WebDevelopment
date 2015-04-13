namespace MusicApp.WebApi.Models
{
    using MusicApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class ArtistViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public DateTime? BirthDate { get; set; }
        public ICollection<Song> Songs { get; set; }

        public ICollection<Album> Albums { get; set; }
    }
}