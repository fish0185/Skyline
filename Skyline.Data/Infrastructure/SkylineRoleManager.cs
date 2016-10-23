using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skyline.Data.Infrastructure
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;

    using Skyline.Data.Concrete;
    using Skyline.Data.Entities;

    public class SkylineRoleManager : RoleManager<SkylineRole>
    {
        public SkylineRoleManager(RoleStore<SkylineRole> store)
            : base(store)
        {
            
        }
    }
}