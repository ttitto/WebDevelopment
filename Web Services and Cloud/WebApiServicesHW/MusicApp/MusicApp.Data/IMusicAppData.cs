namespace MusicApp.Data
{
    using MusicApp.Data.Repositories;
    using MusicApp.Models;

    public interface IMusicAppData
    {
        IMusicAppRepository<User> Users { get; }

        IMusicAppRepository<Song> Songs { get; }

        IMusicAppRepository<Artist> Artists { get; }

        IMusicAppRepository<Album> Albums { get; }

        int SaveChanges();
    }
}
