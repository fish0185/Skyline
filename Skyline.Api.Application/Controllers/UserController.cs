namespace Skyline.Api.Application.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Skyline.Data.Entities;
    using Skyline.Data.Infrastructure;
    using Skyline.Domain;

    public class UserController : ApiController
    {
        private readonly SkylineUserManager _userManager;
        private readonly SkylineRoleManager _roleManager;

        public UserController(
            SkylineUserManager userManager,
            SkylineRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IHttpActionResult> Post(User newUser)
        {
            if (newUser != null)
            {
                SkylineUser user = new SkylineUser
                                       {
                                           UserName = newUser.Name
                                       };
                var result = await this._userManager.CreateAsync(user, newUser.Password);
                if (result.Succeeded)
                {
                    return this.Ok(newUser);
                }

                AddErrorsToModelState(result);
            }
            else
            {
                ModelState.AddModelError("", "User cannot be null");
            }

            return this.BadRequest(ModelState);
        }

        private void AddErrorsToModelState(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
