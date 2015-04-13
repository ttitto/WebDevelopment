namespace MusicApp.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using System.Data.Entity;

    using AutoMapper;
    using Microsoft.AspNet.Identity;

    using MusicApp.Data;
    using MusicApp.Models;
    using MusicApp.WebApi.Models;
    using MusicApp.WebApi.Infrastructure;

    public class ArtistsController : MusicAppBaseApiController
    {
        public ArtistsController(IMusicAppData musicAppData, IUserProvider userProvider)
            : base(musicAppData, userProvider)
        {
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var artists = this.musicAppData.Artists
                .All()
                .Include(a => a.Songs)
                .Include(a => a.Songs);

            var viewArtists = Mapper.Map<ICollection<ArtistViewModel>>(artists);

            return Ok(viewArtists);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult My()
        {
            var currentUserId = this.userProvider.GetUserId();
            if (null != currentUserId)
            {
                return Unauthorized();
            }

            var myArtists = this.musicAppData.Artists
                .Search(a => a.UserId == currentUserId)
                .Include(a => a.Songs)
                .Include(a => a.Albums);
            var myArtistModels = Mapper.Map<ICollection<ArtistViewModel>>(myArtists);

            return Ok(myArtistModels);
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Create(ArtistDataModel artistModel)
        {
            string currentUserId = this.userProvider.GetUserId();

            if (null == currentUserId)
            {
                return Unauthorized();
            }
            else
            {
                if (!this.ModelState.IsValid)
                {
                    return this.BadRequest(this.ModelState);
                }
                else
                {
                    artistModel.UserId = currentUserId;
                    var isUserIdSelected = this.ModelState.IsValidField("UserId");
                    if (!isUserIdSelected)
                    {
                        return BadRequest("Artist's UserId is required.");
                    }

                    Artist artist = Mapper.Map<Artist>(artistModel);
                    this.musicAppData.Artists.Add(artist);
                    this.musicAppData.SaveChanges();

                    ArtistViewModel viewArtist = Mapper.Map<ArtistViewModel>(artist);
                    return Ok(viewArtist);
                }
            }
        }

        [HttpPut]
        [Authorize]
        public IHttpActionResult Edit(int id, ArtistDataModel artistModel)
        {
            if (artistModel == null)
            {
                return this.BadRequest("New values to edit artist were not passed.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var artistFromDb = this.musicAppData.Artists.Find(id);
            if (null == artistFromDb)
            {
                return this.NotFound();
            }

            if (this.userProvider.GetUserId() != artistFromDb.UserId)
            {
                return this.Unauthorized();
            }

            artistFromDb.Name = artistModel.Name;
            if (null != artistModel.Country)
            {
                artistFromDb.Country = artistModel.Country;
            }

            if (null != artistModel.Songs)
            {
                artistFromDb.Songs = artistModel.Songs;
            }

            if (null != artistModel.Albums)
            {
                artistFromDb.Albums = artistModel.Albums;
            }

            int result = this.musicAppData.SaveChanges();
            if (result > 0)
            {
                var ArtistViewModel = Mapper.Map<ArtistViewModel>(artistFromDb);
                return this.Ok(ArtistViewModel);
            }
            else
            {
                return this.BadRequest("There weren't any changes.");
            }
        }

        [Authorize]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var artistFromDb = this.musicAppData.Artists.Find(id);
            if (null == artistFromDb)
            {
                return this.NotFound();
            }

            if (this.userProvider.GetUserId() != artistFromDb.UserId)
            {
                return this.Unauthorized();
            }

            this.musicAppData.Artists.Delete(artistFromDb);
            this.musicAppData.SaveChanges();

            return this.Ok(id);
        }
    }
}