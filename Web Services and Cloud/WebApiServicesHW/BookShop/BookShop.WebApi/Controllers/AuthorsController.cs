namespace BookShop.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Description;
    using System.Web.OData;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Models;
    using BookShop.Models;

    public class AuthorsController : BaseApiController
    {
        public AuthorsController(IBookShopData data)
            : base(data)
        {
        }

        // GET api/authors/{id}
        [HttpGet]
        [ResponseType(typeof(IQueryable<AuthorViewModel>))]
        public IHttpActionResult Get(int id)
        {
            var author = this.data.Authors
                .Search(a => a.Id == id)
                .Project()
                .To<AuthorViewModel>()
                .FirstOrDefault();

            if (null == author)
            {
                return this.NotFound();
            }

            return this.Ok(author);
        }

        // POST api/authors
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IHttpActionResult Post(AuthorBindingModel author)
        {
            if (null == author)
            {
                return this.BadRequest("Author model is not provided.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            Author newAuthor = Mapper.Map<Author>(author);

            this.data.Authors.Add(newAuthor);
            this.data.SaveChanges();
            return this.Ok(Mapper.Map<AuthorViewModel>(newAuthor));
        }

        // GET api/authors/{id}/books
        [HttpGet]
        [EnableQuery]
        [Route("api/authors/{id}/books")]
        public IHttpActionResult GetAuthorBooks(int id)
        {
            var author = this.data.Authors
                .Search(a => a.Id == id)
                .Include(a => a.Books)
                .FirstOrDefault();

            if (null == author)
            {
                return this.NotFound();
            }

            var authorsBooks = Mapper.Map<IEnumerable<BookViewModel>>(author.Books);

            return this.Ok(authorsBooks);
        }
    }
}