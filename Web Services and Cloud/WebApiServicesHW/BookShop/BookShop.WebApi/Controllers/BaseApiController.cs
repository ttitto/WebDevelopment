namespace BookShop.WebApi.Controllers
{
    using Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    public class BaseApiController : ApiController
    {
        protected IBookShopData data;

        public BaseApiController(IBookShopData data)
        {
            this.data = data;
        }
    }
}