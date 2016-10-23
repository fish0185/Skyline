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
    using Skyline.Data.Entities;
    using Skyline.Data.Infrastructure;
    using Skyline.Services.Abstract;

    using News = Skyline.Domain.News;

    public class NewsController : ApiController
    {

        private readonly INewsService _newsService;
        private readonly SkylineUserManager _manager;
        private readonly SkylineRoleManager _roleManager;
        private readonly IAuthenticationManager _authManager;

        public NewsController(
            INewsService newsService,
            SkylineUserManager manager,
            SkylineRoleManager roleManager,
            IAuthenticationManager authManager)
        {
            this._newsService = newsService;
            this._manager = manager;
            this._roleManager = roleManager;
            this._authManager = authManager;
        }

        public IEnumerable<News> GetNewses()
        {
            var xxx = _newsService.GetNews();
            var mans = _manager.FindByName("Admin");
            var roles = this._roleManager.Roles;
            return new List<News>();
        }

        [Authorize]
        public void Delete()
        {
            Console.WriteLine("Test");
            _authManager.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalBearer);
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
