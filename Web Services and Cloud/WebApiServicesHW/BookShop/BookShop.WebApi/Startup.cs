using Microsoft.Owin;

[assembly: OwinStartup(typeof(BookShop.WebApi.Startup))]

namespace BookShop.WebApi
{
    using Owin;
    using System.Data.Entity;
    using BookShop.Data;
    using Ninject;
    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;
    using System.Reflection;
    using System.Web.Http;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // Ninject
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(GlobalConfiguration.Configuration);
        }

        // Ninject
        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            RegisterMappings(kernel);
            return kernel;
        }

        // Ninject
        private static void RegisterMappings(StandardKernel kernel)
        {
            kernel.Bind<IBookShopData>().To<BookShopData>().WithConstructorArgument("context", new BookShopDbContext());
        }
    }
}
