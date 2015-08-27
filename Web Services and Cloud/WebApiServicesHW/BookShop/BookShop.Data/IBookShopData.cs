namespace BookShop.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Repositories;

    public interface IBookShopData
    {
        IBookShopRepository<User> Users { get; }

        IBookShopRepository<Book> Books { get; }

        IBookShopRepository<Category> Categories { get; }

        IBookShopRepository<Author> Authors { get; }

        IBookShopRepository<Purchase> Purchases { get; }

        IBookShopRepository<IdentityRole> Roles { get; }

        IBookShopRepository<IdentityUserRole> UserRoles { get; }

        int SaveChanges();
    }
}
