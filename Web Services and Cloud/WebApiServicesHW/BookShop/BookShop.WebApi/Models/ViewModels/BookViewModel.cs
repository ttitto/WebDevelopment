using BookShop.Models;
using BookShop.WebApi.Infrastructure.ModelsMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace BookShop.WebApi.Models
{
    public class BookViewModel : IMapFrom<Book>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public EditionType EditionType { get; set; }

        public decimal Price { get; set; }

        public int Copies { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public ICollection<string> CategoriesNames { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Book, BookViewModel>()
                .ForMember(bvm => bvm.CategoriesNames, options => options.MapFrom(b => b.Categories == null ? null : b.Categories.Select(c => c.Name)));
            configuration.CreateMap<Book, BookViewModel>()
                .ForMember(bvm => bvm.AuthorName, options => options.MapFrom(b => b.Author.FirstName == null ? b.Author.LastName : string.Concat(b.Author.FirstName, " ", b.Author.LastName)));
        }
    }
}