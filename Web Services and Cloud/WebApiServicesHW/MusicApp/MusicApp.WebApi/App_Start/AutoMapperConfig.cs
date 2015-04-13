namespace MusicApp.WebApi.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;

    using MusicApp.Models;
    using MusicApp.WebApi.Models;

    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<Artist, ArtistViewModel>();
            Mapper.CreateMap<ArtistDataModel, Artist>();
            Mapper.CreateMap<ICollection<Artist>, ICollection<ArtistViewModel>>();

            Mapper.CreateMap<Album, AlbumViewModel>();
            Mapper.CreateMap<AlbumDataModel, Album>();
            Mapper.CreateMap<ICollection<Album>, ICollection<AlbumViewModel>>();

            Mapper.CreateMap<Song, SongViewModel>();
            Mapper.CreateMap<SongDataModel, Song>();
            Mapper.CreateMap<ICollection<Song>, ICollection<SongViewModel>>();
        }
    }
}