﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skyline.Data.Entities
{
    using Microsoft.AspNet.Identity.EntityFramework;
    public class SkylineRole : IdentityRole
    {
        public SkylineRole() : base() { }
        public SkylineRole(string name) : base(name) { }
    }
}