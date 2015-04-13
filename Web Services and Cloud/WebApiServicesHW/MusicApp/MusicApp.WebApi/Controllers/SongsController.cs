namespace MusicApp.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    using MusicApp.Data;
    using MusicApp.WebApi.Infrastructure;
    using AutoMapper;
    using MusicApp.WebApi.Models;
    using MusicApp.Models;

    public class SongsController : MusicAppBaseApiController
    {
        public SongsController(IMusicAppData musicAppData, IUserProvider userProvider)
            : base(musicAppData, userProvider)
        {
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var songs = this.musicAppData.Songs.All().Include(s => s.Artists).Include(s => s.Album);
            var viewSongs = Mapper.Map<SongViewModel>(songs);

            return this.Ok(viewSongs);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult My()
        {
            var currentUserId = this.userProvider.GetUserId();
            var songs = this.musicAppData.Songs.All()
                .Include(s => s.Artists.Where(a => a.UserId == currentUserId))
                .Include(s => s.Album);

            var songsModels = Mapper.Map<ICollection<SongViewModel>>(songs);

            return this.Ok(songsModels);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Create(SongDataModel dataSong)
        {
            var currentUserId = this.userProvider.GetUserId();
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (null != currentUserId)
            {
                dataSong.UserId = currentUserId;
                bool isUserSelected = this.ModelState.IsValidField("UserId");

                if (!isUserSelected)
                {
                    return this.BadRequest("Song's UserId is required.");
                }

                Song song = Mapper.Map<Song>(dataSong);
                this.musicAppData.Songs.Add(song);
                SongViewModel viewSong = Mapper.Map<SongViewModel>(song);

                return this.Ok(viewSong);
            }
            else
            {
                return this.Unauthorized();
            }
        }

        [HttpPut]
        [Authorize]
        public IHttpActionResult Edit(int id, SongDataModel dataSong)
        {
            if (dataSong == null)
            {
                return this.BadRequest("New values to edit album were not passed.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var songFromDb = this.musicAppData.Songs.Find(id);
            if (null == songFromDb)
            {
                return this.NotFound();
            }

            if (this.userProvider.GetUserId() != songFromDb.UserId)
            {
                return this.Unauthorized();
            }

            if (dataSong.Title != null)
            {
                songFromDb.Title = dataSong.Title;
            }

            if (dataSong.Genre != null)
            {
                songFromDb.Genre = dataSong.Genre;
            }

            if (dataSong.Artists != null)
            {
                songFromDb.Artists = dataSong.Artists;
            }

            if (dataSong.AlbumId != null)
            {
                songFromDb.AlbumId = dataSong.AlbumId;
            }

            if (dataSong.Year != null)
            {
                songFromDb.Year = dataSong.Year;
            }

            int result = this.musicAppData.SaveChanges();
            if (result > 0)
            {
                var viewSong = Mapper.Map<SongViewModel>(songFromDb);
                return this.Ok(viewSong);
            }
            else
            {
                return this.BadRequest("There weren't any changes");
            }
        }
    }
}