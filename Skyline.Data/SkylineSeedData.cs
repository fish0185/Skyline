using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Skyline.Data.Concrete;
    using Skyline.Data.Entities;
    using Skyline.Data.Infrastructure;

    public class SkylineSeedData : DropCreateDatabaseIfModelChanges<SkylineDbContext>
    {
        protected override void Seed(SkylineDbContext context)
        {
            SkylineUserManager userMgr = new SkylineUserManager(new UserStore<SkylineUser>(context));
            SkylineRoleManager roleMgr = new SkylineRoleManager(new RoleStore<SkylineRole>(context));
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

            GetNews().ForEach(news=>context.News.Add(news));
            context.SaveChanges();
        }

        private static List<News> GetNews()
        {
            return new List<News>
                       {
                           new News
                               {
                                   NewsTitle = "Unit 807 For Sale"
                               },
                           new News
                               {
                                   NewsTitle = "Fire alarm check"
                               },
                           new News
                               {
                                   NewsTitle = "Lift Broken"
                               }
                       };
        }
    }
}
