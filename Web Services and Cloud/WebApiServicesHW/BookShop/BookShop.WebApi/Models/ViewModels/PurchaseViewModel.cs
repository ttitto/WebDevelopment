namespace BookShop.WebApi.Models
{
    using BookShop.Models;
    using Infrastructure.ModelsMapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;

    public class PurchaseViewModel : IMapFrom<Purchase>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public string UserName { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool IsRecalled { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Purchase, PurchaseViewModel>()
                .ForMember(pvm => pvm.BookTitle, options => options.MapFrom(p => p.Book.Title));
            configuration.CreateMap<Purchase, PurchaseViewModel>()
                .ForMember(pvm => pvm.UserName, options => options.MapFrom(p => p.User.UserName));
        }
    }
}