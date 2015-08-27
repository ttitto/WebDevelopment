namespace BookShop.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.OData;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Models;
    using BookShop.Models;

    public class CategoriesController : BaseApiController
    {
        public CategoriesController(IBookShopData data)
            : base(data)
        {
        }

        // GET api/categories
        [HttpGet]
        [EnableQuery]
        public IHttpActionResult Get()
        {
            var categories = this.data.Categories.All();
            var viewCategories = Mapper.Map<IEnumerable<CategoryViewModel>>(categories);
            return this.Ok(viewCategories);
        }

        // GET api/categories/{id}
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var category = this.data.Categories
                .Search(c => c.Id == id)
                .Project()
                .To<CategoryViewModel>()
                .FirstOrDefault();

            if (null == category)
            {
                return this.NotFound();
            }

            return this.Ok(category);
        }

        // PUT api/categories/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] CategoryBindingModel bindingModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var dbCategory = this.data.Categories.Find(id);
            if (null == dbCategory)
            {
                return this.NotFound();
            }

            dbCategory.Name = bindingModel.Name;
            var categoryWithSameName = this.data.Categories
                .Search(c => c.Name == dbCategory.Name)
                .FirstOrDefault();
            if (null != categoryWithSameName)
            {
                return this.BadRequest(string.Format("Category with name {0} already exists.", dbCategory.Name));
            }

            var viewCategory = Mapper.Map<CategoryViewModel>(dbCategory);
            this.data.SaveChanges();
            return this.Ok(viewCategory);
        }

        // POST api/categories
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] CategoryBindingModel bindingModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var categoryWithSameName = this.data.Categories
                .Search(c => c.Name == bindingModel.Name)
                .FirstOrDefault();
            if (null != categoryWithSameName)
            {
                return this.BadRequest(string.Format("Category with name {0} already exists.", bindingModel.Name));
            }

            var category = Mapper.Map<Category>(bindingModel);
            this.data.Categories.Add(category);
            this.data.SaveChanges();

            var viewCategory = Mapper.Map<CategoryViewModel>(category);
            return this.Ok(viewCategory);
        }

        // DELETE api/categories/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var dbCategory = this.data.Categories.Find(id);
            if (null == dbCategory)
            {
                return this.NotFound();
            }

            this.data.Categories.Delete(dbCategory);
            this.data.SaveChanges();

            var viewCategory = Mapper.Map<CategoryViewModel>(dbCategory);
            return this.Ok(viewCategory);
        }
    }
}