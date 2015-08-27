using Microsoft.Owin;
[assembly: OwinStartup(typeof(MusicApp.WebApi.Startup))]

namespace MusicApp.WebApi
{
    using Owin;
    using Ninject;
    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;
    using System.Reflection;
    using System.Web.Http;
    using MusicApp.Data;
    using MusicApp.WebApi.Infrastructure;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(GlobalConfiguration.Configuration);
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            RegisterMappings(kernel);
            return kernel;
        }

        private static void RegisterMappings(StandardKernel kernel)
        {
            kernel.Bind<IMusicAppData>().To<MusicAppData>().WithConstructorArgument("context", new MusicAppDbContext());
            kernel.Bind<IUserProvider>().To<AspNetUserProvider>();
        }
    }
}
