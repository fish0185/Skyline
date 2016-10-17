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

    public class SkylineRoleManager : RoleManager<SkylineRole>
    {
        public SkylineRoleManager(RoleStore<SkylineRole> store) : base(store){ }

        public static SkylineRoleManager Create(
            IdentityFactoryOptions<SkylineRoleManager> options,
            IOwinContext context)
        {
            return new SkylineRoleManager(new RoleStore<SkylineRole>(context.Get<SkylineIdentityDbContext>()));
        }
    }
}