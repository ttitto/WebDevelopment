namespace BookShop.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Http.Description;
    using System.Web.Http;
    using System.Web.OData;

    using Microsoft.AspNet.Identity;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Models;
    using BookShop.Models;

    public class BooksController : BaseApiController
    {
        public BooksController(IBookShopData data)
            : base(data)
        {
        }

        // GET api/books/{id}
        [HttpGet]
        [ResponseType(typeof(IQueryable<BookViewModel>))]
        public IHttpActionResult Get(int id)
        {
            var book = this.data.Books
                .Search(b => b.Id == id)
                .Project()
                .To<BookViewModel>()
                .FirstOrDefault();

            if (null == book)
            {
                return this.NotFound();
            }

            return this.Ok(book);
        }

        // GET api/books?search={word}
        [HttpGet]
        [EnableQuery]
        public IHttpActionResult GetFilteredByTitleBooks([FromUri]string search)
        {
            var books = this.data.Books
                .Search(b => b.Title.Contains(search))
                .OrderByDescending(b => b.Title)
                .Take(10)
                .Project()
                .To<BookTitleIdViewModel>();

            return this.Ok(books);
        }

        // PUT api/books/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IHttpActionResult Edit(int id, [FromBody] PutBookBindingModel newBook)
        {
            var book = this.data.Books.Find(id);
            if (null == book)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            book.AuthorId = newBook.AuthorId;
            book.Copies = newBook.Copies;
            book.Description = newBook.Description;
            book.EditionType = newBook.EditionType;
            book.Price = newBook.Price;
            book.ReleaseDate = newBook.ReleaseDate;
            book.Title = newBook.Title;

            this.data.SaveChanges();

            var bookViewModel = Mapper.Map<BookViewModel>(book);
            return this.Ok(bookViewModel);
        }

        // DELETE api/books/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var bookToDelete = this.data.Books.Delete(id);
            if (bookToDelete == null)
            {
                return this.NotFound();
            }

            this.data.SaveChanges();
            var bookViewModel = Mapper.Map<BookViewModel>(bookToDelete);
            return this.Ok(bookViewModel);
        }

        // POST api/books
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]PostBookBindingModel newBook)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            Book book = Mapper.Map<Book>(newBook);

            var categoriesNames = newBook.CategoriesNames.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (categoriesNames.Count() > 0)
            {
                book.Categories = new List<Category>();
            }

            foreach (var name in categoriesNames)
            {
                var category = this.data.Categories.Search(c => c.Name == name).FirstOrDefault();
                if (null != category)
                {
                    book.Categories.Add(category);
                }
            }

            this.data.Books.Add(book);
            this.data.SaveChanges();

            var bookViewModel = Mapper.Map<BookViewModel>(book);
            return this.Ok(bookViewModel);
        }

        // PUT api/books/buy/{id}
        [Authorize]
        [HttpPut]
        [Route("api/books/buy/{id}")]
        public IHttpActionResult BuyBook(int id)
        {
            var bookToBuy = this.data.Books.Find(id);
            if (null == bookToBuy)
            {
                return this.NotFound();
            }

            if (bookToBuy.Copies < 1)
            {
                return this.BadRequest("The requested book is currently not present.");
            }

            var currentUserId = this.User.Identity.GetUserId();
            Purchase purchase = new Purchase()
            {
                Book = bookToBuy,
                BookId = bookToBuy.Id,
                Price = bookToBuy.Price,
                PurchaseDate = DateTime.Now,
                UserId = currentUserId,
                User = this.data.Users.Find(currentUserId)
            };

            this.data.Purchases.Add(purchase);
            bookToBuy.Copies--;
            this.data.SaveChanges();

            var viewPurchase = Mapper.Map<PurchaseViewModel>(purchase);
            return this.Ok(viewPurchase);
        }

        // PUT api/books/recall/{id}
        [HttpPut]
        [Authorize]
        [Route("api/books/recall/{id}")]
        public IHttpActionResult RecallBook(int id)
        {
            var bookToRecall = this.data.Books.Find(id);
            if (null == bookToRecall)
            {
                return this.NotFound();
            }

            string currentUserId = this.User.Identity.GetUserId();
            var purchasesToRecall = this.data.Purchases
                .Search(p => p.BookId == id && p.UserId == currentUserId && p.IsRecalled == false && DbFunctions.DiffDays(DateTime.Now, p.PurchaseDate) <= 30);

            if (null == purchasesToRecall || purchasesToRecall.Count() < 1)
            {
                return this.BadRequest(string.Format("You do not have purchases for the book {0} that can be recalled.", bookToRecall.Title));
            }

            foreach (var purchase in purchasesToRecall)
            {
                bookToRecall.Copies++;
                purchase.IsRecalled = true;
            }

            this.data.SaveChanges();

            return this.Ok();
        }

        // GET api/user/{username}/purchases
        [HttpGet]
        [EnableQuery]
        [Route("api/user/{username}/purchases")]
        public IHttpActionResult GetPurchasesPerUser(string username)
        {
            var purchases = this.data.Purchases
                .Search(p => p.User.UserName == username)
                .OrderByDescending(p => p.PurchaseDate)
                .Project()
                .To<PurchaseViewModel>();

            if (purchases == null)
            {
                return this.NotFound();
            }

            return this.Ok(purchases);
        }
    }
}