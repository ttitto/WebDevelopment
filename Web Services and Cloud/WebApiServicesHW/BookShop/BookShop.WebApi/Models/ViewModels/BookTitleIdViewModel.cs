namespace BookShop.WebApi.Models
{
    using BookShop.Models;
    using Infrastructure.ModelsMapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class BookTitleIdViewModel : IMapFrom<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}