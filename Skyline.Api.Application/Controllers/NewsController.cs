using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Skyline.Api.Application.Controllers
{
    using System.Threading.Tasks;
    using System.Web;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    using Skyline.Api.Application.Infrastructure.Identity;
    using Skyline.Domain;
    using Skyline.Services.Abstract;


    public class NewsController : ApiController
    {

        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            this._newsService = newsService;
        }

        public IEnumerable<News> GetNewses()
        {
            var xxx = _newsService.GetNews();
            return new List<News>();
        }

        public async Task Post()
        {
            IAuthenticationManager authMgr = HttpContext.Current.GetOwinContext().Authentication;
            SkylineUserManager userMrg = HttpContext.Current.GetOwinContext().GetUserManager<SkylineUserManager>();
            SkylineUser user = await userMrg.FindAsync("Admin", "secret");
            authMgr.SignIn(await userMrg.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie));
           // return RedirectToAction("Index");
        }
    }
}
