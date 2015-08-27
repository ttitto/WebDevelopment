namespace BookShop.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web.Hosting;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<BookShopDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BookShopDbContext context)
        {
            if (!context.Authors.Any())
            {
                this.SeedAuthors(context);
            }

            if (!context.Categories.Any())
            {
                this.SeedCategories(context);
            }

            if (!context.Books.Any())
            {
                this.SeedBooks(context);
            }

            if (!context.Users.Any())
            {
                this.SeedUsers(context);
            }

            if (!context.Roles.Any())
            {
                string adminRoleName = "Admin";
                var adminUsers = new User[] { context.Users.FirstOrDefault() };
                this.SeedRoles(context, adminRoleName, adminUsers);
            }
        }

        private void SeedUsers(BookShopDbContext context)
        {
            UserManager<User> manager = new UserManager<User>(new UserStore<User>(context));
            User firstUser = new User()
            {
                Email = "ttitto@ttitto.com",
                UserName = "ttitto"
            };
            manager.Create(firstUser, "Temp_123");
        }

        private void SeedRoles(BookShopDbContext context, string roleName, User[] adminUsers)
        {
            var role = new IdentityRole(roleName);
            context.Roles.Add(role);
            foreach (var user in adminUsers)
            {
                if (null != user)
                {
                    role.Users.Add(new IdentityUserRole() { UserId = user.Id });
                }
            }

            context.SaveChanges();
        }

        private void SeedAuthors(BookShopDbContext context)
        {
            string seedDataFilePath = HostingEnvironment.MapPath("~/App_Data/SeedDataFiles/authors.txt");
            using (StreamReader reader = new StreamReader(seedDataFilePath, Encoding.UTF8))
            {
                string line = reader.ReadLine();
                // there is a header line
                line = reader.ReadLine();
                while (line != null)
                {
                    string[] lineArr = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    Author newAuthor = new Author
                    {
                        FirstName = lineArr[0],
                        LastName = lineArr[1]
                    };

                    context.Authors.Add(newAuthor);
                    line = reader.ReadLine();
                };
            }

            context.SaveChanges();
        }

        private void SeedCategories(BookShopDbContext context)
        {
            string seedDataFilePath = HostingEnvironment.MapPath("~/App_Data/SeedDataFiles/categories.txt");
            using (StreamReader reader = new StreamReader(seedDataFilePath, Encoding.UTF8))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    Category newCategory = new Category()
                    {
                        Name = line.Trim()
                    };
                    context.Categories.Add(newCategory);
                    line = reader.ReadLine();
                };
            }

            context.SaveChanges();
        }

        private void SeedBooks(BookShopDbContext context)
        {
            string seedDataFilePath = HostingEnvironment.MapPath("~/App_Data/SeedDataFiles/books.txt");
            using (StreamReader reader = new StreamReader(seedDataFilePath, Encoding.UTF8))
            {
                string line = reader.ReadLine();
                // there is a header line
                line = reader.ReadLine();
                while (line != null)
                {
                    string[] lineArr = line.Split(new char[] { ' ' }, 6, StringSplitOptions.RemoveEmptyEntries);

                    var edition = (EditionType)int.Parse(lineArr[0]);

                    CultureInfo enUS = CultureInfo.InvariantCulture;
                    DateTime temp;
                    var releaseDate = DateTime.TryParseExact(lineArr[1], "dd/MM/yyyy", enUS,
                        DateTimeStyles.None, out temp) ? temp : default(DateTime?);

                    Random rnd = new Random();
                    var authors = context.Authors.ToList();
                    Author author = authors[rnd.Next(authors.Count())];

                    var categories = context.Categories.ToList();
                    var currentBookCategories = new List<Category>();
                    for (int i = 0; i < rnd.Next(1, 4); i++)
                    {
                        var category = categories[rnd.Next(categories.Count())];
                        currentBookCategories.Add(category);
                    }

                    Book newBook = new Book
                    {
                        EditionType = edition,
                        ReleaseDate = releaseDate,
                        Copies = int.Parse(lineArr[2]),
                        Price = decimal.Parse(lineArr[3], enUS),
                        Author = author,
                        Categories = currentBookCategories,
                        Title = lineArr[5]
                    };

                    context.Books.Add(newBook);
                    line = reader.ReadLine();
                };
            }

            context.SaveChanges();
        }
    }
}
