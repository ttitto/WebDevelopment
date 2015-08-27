namespace BookShop.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using BookShop.Models;

    public class BookShopDbContext : IdentityDbContext<User>
    {
        public BookShopDbContext()
            : base("BookShop", throwIfV1Schema: false)
        {
        }

        public static BookShopDbContext Create()
        {
            return new BookShopDbContext();
        }

        public IDbSet<Book> Books { get; set; }

        public IDbSet<Author> Authors { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Purchase> Purchases { get; set; }
    }
}
