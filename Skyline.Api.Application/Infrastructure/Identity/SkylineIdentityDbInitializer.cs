using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skyline.Api.Application.Infrastructure.Identity
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class SkylineIdentityDbInitializer : CreateDatabaseIfNotExists<SkylineIdentityDbContext>
    {
        protected override void Seed(SkylineIdentityDbContext context)
        {
            SkylineUserManager userMgr =
new SkylineUserManager(new UserStore<SkylineUser>(context));
            SkylineRoleManager roleMgr =
            new SkylineRoleManager(new RoleStore<SkylineRole>(context));
            string roleName = "Administrators";
            string userName = "Admin";
            string password = "secret";
            string email = "admin@example.com";
            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new SkylineRole(roleName));
            }
            SkylineUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new SkylineUser
                {
                    UserName = userName,
                    Email = email
                }, password);
                user = userMgr.FindByName(userName);
            }
            if (!userMgr.IsInRole(user.Id, roleName))
            {
                userMgr.AddToRole(user.Id, roleName);
            }
            base.Seed(context);
        }   
    }
}