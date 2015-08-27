namespace BookShop.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;
    using Repositories;

    public class BookShopData : IBookShopData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public BookShopData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IBookShopRepository<Author> Authors
        {
            get
            {
                return this.GetRepository<Author>();
            }
        }

        public IBookShopRepository<Book> Books
        {
            get
            {
                return this.GetRepository<Book>();
            }
        }

        public IBookShopRepository<Category> Categories
        {
            get
            {
                return this.GetRepository<Category>();
            }
        }

        public IBookShopRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IBookShopRepository<Purchase> Purchases
        {
            get
            {
                return this.GetRepository<Purchase>();
            }
        }

        public IBookShopRepository<IdentityRole> Roles
        {
            get
            {
                return this.GetRepository<IdentityRole>();
            }
        }

        public IBookShopRepository<IdentityUserRole> UserRoles
        {
            get
            {
                return this.GetRepository<IdentityUserRole>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IBookShopRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(BookShopRepository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IBookShopRepository<T>)repositories[typeOfRepository];
        }
    }

}
