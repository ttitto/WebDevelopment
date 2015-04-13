namespace MusicApp.Data
{
    using MusicApp.Data.Repositories;
    using MusicApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMusicAppData
    {
        IMusicAppRepository<User> Users { get; }

        IMusicAppRepository<Song> Songs { get; }

        IMusicAppRepository<Artist> Artists { get; }

        IMusicAppRepository<Album> Albums { get; }

        int SaveChanges();
    }
}
