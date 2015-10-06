namespace AtTheMovies.Models
{
    using System.Data.Entity;

    public class MoviesEntities : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }
}
