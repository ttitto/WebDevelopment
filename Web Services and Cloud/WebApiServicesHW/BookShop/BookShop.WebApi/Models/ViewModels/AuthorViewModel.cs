namespace BookShop.WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BookShop.Models;
    using Infrastructure.ModelsMapping;

    public class AuthorViewModel : IMapFrom<Author>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<string> BookTitles { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Author, AuthorViewModel>()
                .ForMember(avm => avm.BookTitles, options => options.MapFrom(a => a.Books.Select(b => b.Title)));
        }
    }
}