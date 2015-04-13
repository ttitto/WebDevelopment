
namespace MusicApp.WebApi.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MusicApp.Models;

    public class AlbumDataModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int? Year { get; set; }

        public string Producer { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}