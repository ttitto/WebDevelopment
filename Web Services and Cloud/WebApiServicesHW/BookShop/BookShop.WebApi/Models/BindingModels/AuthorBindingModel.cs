namespace BookShop.WebApi.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BookShop.Models;
    using BookShop.WebApi.Infrastructure.ModelsMapping;

    public class AuthorBindingModel : IMapFrom<Author>
    {
        private ICollection<Book> books;

        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public virtual ICollection<Book> Books
        {
            get { return this.books; }
            set { this.books = value; }
        }
    }
}