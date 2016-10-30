using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Skyline.Api.Application.Controllers
{
    public class AdminController : ApiController
    {
        [Authorize]
        public string Get()
        {
            return "Admin";
        }

        [Authorize(Roles = "Administrator")]
        public string Delete()
        {
            return "Deleted";
        }
    }
}
