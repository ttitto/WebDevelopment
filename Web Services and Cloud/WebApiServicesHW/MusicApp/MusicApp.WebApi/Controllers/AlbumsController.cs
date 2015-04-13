namespace MusicApp.WebApi.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Http;

    using AutoMapper;

    using MusicApp.Data;
    using MusicApp.WebApi.Infrastructure;
    using MusicApp.WebApi.Models;
    using MusicApp.Models;

    public class AlbumsController : MusicAppBaseApiController
    {
        public AlbumsController(IMusicAppData musicAppData, IUserProvider userProvider)
            : base(musicAppData, userProvider)
        {
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var albums = this.musicAppData.Albums.All().Include(a => a.Songs).Include(a => a.Artists);
            var viewAlbums = Mapper.Map<AlbumViewModel>(albums);

            return this.Ok(viewAlbums);
        }

            [HttpGet]
            [Authorize]
            public IHttpActionResult My()
            {
                var currentUserId = this.userProvider.GetUserId();
                var albums = this.musicAppData.Albums.Search(a => a.UserId == currentUserId)
                    .Include(a => a.Songs.Where(s => s.UserId == currentUserId))
                    .Include(a => a.Artists.Where(art => art.UserId == currentUserId));

                var albumsModels = Mapper.Map<ICollection<AlbumViewModel>>(albums);

                return this.Ok(albumsModels);
            }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Create(AlbumDataModel albumModel)
        {
            var currentUserId = this.userProvider.GetUserId();

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }
                
            if (null != currentUserId)
            {
                albumModel.UserId = currentUserId;
                bool isUserSelected = this.ModelState.IsValidField("UserId");
                if (!isUserSelected)
                {
                    return this.BadRequest("Album's UserId is required.");
                }

                Album album = Mapper.Map<Album>(albumModel);
                this.musicAppData.Albums.Add(album);
                this.musicAppData.SaveChanges();

                AlbumViewModel viewAlbum = Mapper.Map<AlbumViewModel>(album);
                return this.Ok(viewAlbum);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut]
        [Authorize]
        public IHttpActionResult Edit(int id, AlbumDataModel dataAlbum)
        {
            if (dataAlbum == null)
            {
                return this.BadRequest("New values to edit album were not passed.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var albumFromDb = this.musicAppData.Albums.Find(id);
            if (null == albumFromDb)
            {
                return this.NotFound();
            }

            if (this.userProvider.GetUserId() != albumFromDb.UserId)
            {
                return this.Unauthorized();
            }

            if (dataAlbum.Title != null)
            {
                albumFromDb.Title = dataAlbum.Title;
            }

            if (dataAlbum.Producer != null)
            {
                albumFromDb.Producer = dataAlbum.Producer;
            }

            if (dataAlbum.Songs != null)
            {
                albumFromDb.Songs = dataAlbum.Songs;
            }
            if (dataAlbum.Artists != null)
            {
                albumFromDb.Artists = dataAlbum.Artists;
            }

            if (dataAlbum.Year != null)
            {
                albumFromDb.Year = dataAlbum.Year;
            }

            int result = this.musicAppData.SaveChanges();
            if (result > 0)
            {
                var viewAlbum = Mapper.Map<AlbumViewModel>(albumFromDb);
                return this.Ok(viewAlbum);
            }
            else
            {
                return this.BadRequest("There weren't any changes");
            }
        }
    }
}