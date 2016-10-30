using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Linq.Expressions;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Skyline.Data.Concrete;
    using Skyline.Data.Entities;
    using Skyline.Data.Infrastructure;

    public class SkylineSeedData : CreateDatabaseIfNotExists<SkylineDbContext>
    {
        protected override void Seed(SkylineDbContext context)
        {
            SkylineUserManager userMgr = new SkylineUserManager(new UserStore<SkylineUser>(context));
            SkylineRoleManager roleMgr = new SkylineRoleManager(new RoleStore<SkylineRole>(context));
            string userName = "Admin";
            string password = "secret";
            string email = "admin@example.com";
            string[] roles = { "Administrator", "User", "Editor" };
            foreach (var role in roles)
            {
                if (!roleMgr.RoleExists(role))
                {
                    roleMgr.Create(new SkylineRole(role));
                }
            }

            SkylineUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                // use default validator
                userMgr.UserValidator = new UserValidator<SkylineUser>(userMgr);
                userMgr.PasswordValidator = new PasswordValidator();
                userMgr.Create(new SkylineUser
                {
                    UserName = userName,
                    Email = email,
                    FirstName = "Admin",
                    LastName = "Yu",
                    DOB = null
                }, password);
                user = userMgr.FindByName(userName);
            }
            if (!userMgr.IsInRole(user.Id, "Administrator"))
            {
                userMgr.AddToRole(user.Id, "Administrator");
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
