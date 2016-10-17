using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skyline.Api.Application.Infrastructure.Identity
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;
    public class SkylineIdentityDbContext : IdentityDbContext<SkylineUser>
    {
        public SkylineIdentityDbContext()
            : base("SkylineDb")
        {
            Database.SetInitializer<SkylineIdentityDbContext>(new SkylineIdentityDbInitializer());
        }

        public static SkylineIdentityDbContext Create()
        {
            return new SkylineIdentityDbContext();
        }
    }
}