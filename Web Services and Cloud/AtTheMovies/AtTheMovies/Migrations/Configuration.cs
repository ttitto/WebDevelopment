namespace AtTheMovies.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AtTheMovies.Models.MoviesEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AtTheMovies.Models.MoviesEntities context)
        {
            context.Movies.AddOrUpdate(m => m.Title,
            new Movie
            {
                Title = "Star Wars",
                ReleaseYear = 1977,
                Runtime = 121
            },
            new Movie
            {
                Title = "Inception",
                ReleaseYear = 2010,
                Runtime = 148
            },
            new Movie
            {
                Title = "Toy Story",
                ReleaseYear = 1995,
                Runtime = 81
            }
            );
        }
    }
}
