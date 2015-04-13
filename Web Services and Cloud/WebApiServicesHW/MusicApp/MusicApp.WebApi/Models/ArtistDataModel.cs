namespace MusicApp.WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MusicApp.Models;

    public class ArtistDataModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        public string Country { get; set; }

        public DateTime? BirthDate { get; set; }

        public string UserId { get; set; }

        public ICollection<Album> Albums { get; set; }

        public ICollection<Song> Songs { get; set; }
    }
}