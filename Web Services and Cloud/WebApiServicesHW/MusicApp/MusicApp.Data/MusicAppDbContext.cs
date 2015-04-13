namespace MusicApp.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;
    using MusicApp.Models;
    using MusicApp.Data.Migrations;

    public class MusicAppDbContext : IdentityDbContext<User>
    {
        public MusicAppDbContext()
            : base("MusicAppConn", throwIfV1Schema: false)
        {
            Database.SetInitializer<MusicAppDbContext>(new MigrateDatabaseToLatestVersion<MusicAppDbContext, Configuration>());
        }

        public static MusicAppDbContext Create()
        {
            return new MusicAppDbContext();
        }

        public IDbSet<Album> Albums { get; set; }

        public IDbSet<Song> Songs { get; set; }

        public IDbSet<Artist> Artists { get; set; }
    }
}
