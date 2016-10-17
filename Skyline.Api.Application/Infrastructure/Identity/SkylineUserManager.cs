using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skyline.Api.Application.Infrastructure.Identity
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;

    public class SkylineUserManager : UserManager<SkylineUser>
    {
        public SkylineUserManager(IUserStore<SkylineUser> store) : base(store) { }

        public static SkylineUserManager Create(
            IdentityFactoryOptions<SkylineUserManager> options,
            IOwinContext context)
        {
            SkylineIdentityDbContext dbContext = context.Get<SkylineIdentityDbContext>();
            SkylineUserManager manager = new SkylineUserManager(new UserStore<SkylineUser>(dbContext));
            return manager;
        }
    }
}