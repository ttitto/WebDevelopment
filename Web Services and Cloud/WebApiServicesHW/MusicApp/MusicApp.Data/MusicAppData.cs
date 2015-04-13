namespace MusicApp.Data
{
    using MusicApp.Data.Repositories;
    using MusicApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MusicAppData : IMusicAppData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public MusicAppData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IMusicAppRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IMusicAppRepository<Song> Songs
        {
            get { return this.GetRepository<Song>(); }
        }

        public IMusicAppRepository<Artist> Artists
        {
            get { return this.GetRepository<Artist>(); }
        }

        public IMusicAppRepository<Album> Albums
        {
            get { return this.GetRepository<Album>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IMusicAppRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(EfMusicAppRepository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IMusicAppRepository<T>)repositories[typeOfRepository];
        }
    }
}
