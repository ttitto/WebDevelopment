namespace MusicApp.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Artist> artists;
        private ICollection<Song> songs;
        private ICollection<Album> albums;

        public User()
        {
            this.artists = new HashSet<Artist>();
            this.albums = new HashSet<Album>();
            this.songs = new HashSet<Song>();
        }

        public ICollection<Artist> Artists
        {
            get { return this.artists; }
            set { this.artists = value; }
        }

        public ICollection<Album> Albums
        {
            get { return this.albums; }
            set { this.albums = value; }
        }

        public ICollection<Song> Songs
        {
            get { return this.songs; }
            set { this.songs = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
