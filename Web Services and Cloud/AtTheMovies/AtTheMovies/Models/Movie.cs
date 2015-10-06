namespace AtTheMovies.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


public    class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int ReleaseYear { get; set; }

        public int Runtime { get; set; }
    }
}
