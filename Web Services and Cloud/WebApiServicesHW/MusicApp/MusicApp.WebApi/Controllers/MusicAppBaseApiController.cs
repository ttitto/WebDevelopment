namespace MusicApp.WebApi.Controllers
{
    using System.Web.Http;

    using MusicApp.Data;
    using MusicApp.WebApi.Infrastructure;

    public class MusicAppBaseApiController : ApiController
    {
        protected IMusicAppData musicAppData;
        protected IUserProvider userProvider;

        public MusicAppBaseApiController(IMusicAppData musicAppData, IUserProvider userProvider)
        {
            this.musicAppData = musicAppData;
            this.userProvider = userProvider;
        }
    }
}