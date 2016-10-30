namespace Skyline.Api.Application.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
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

        [Authorize]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateProfile(User user)
        {
            if (user != null)
            {
                if (user.UserName == this.User.Identity.Name)
                {
                    var dbUser = await this._userManager.FindByNameAsync(user.UserName);
                    dbUser.DOB = user.DOB;
                    dbUser.FirstName = user.FirstName;
                    dbUser.MiddleName = user.MiddleName;
                    dbUser.LastName = user.LastName;
                    var result = await this._userManager.UpdateAsync(dbUser);
                    if (result.Succeeded)
                    {
                        return this.Ok(ToUser(dbUser));
                    }

                    AddErrorsToModelState(result);
                }
                else
                {
                     ModelState.AddModelError("", "Only update yourself is allow"); 
                }                            
            }

            return this.BadRequest(ModelState);
        }

        [Route("api/Users")]
        public async Task<IHttpActionResult> GetAll()
        {
            var users = await this._userManager.Users.ToListAsync();
            return this.Ok(users.Select(ToUser));
        }

        public async Task<IHttpActionResult> Get(int pageNum, int pageSize)
        {
            if (pageNum <= 0)
            {
                return this.BadRequest("Page Number must large then 0");
            }

            var users = await this._userManager.Users.OrderBy(user => user.UserName).Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();
            var usersVm = users.Select(this.ToUser).ToList();
            return this.Ok(usersVm);
        }

        private User ToUser(SkylineUser user)
        {
            return new User
                       {
                        UserName = user.UserName,
                        Email = user.Email,
                        DOB = user.DOB,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        MiddleName = user.MiddleName
                       };
        }

        private SkylineUser ToSkylineUser(User user)
        {
            return new SkylineUser
                       {
                           UserName = user.UserName,
                           Email = user.Email,
                           DOB = user.DOB,
                           FirstName = user.FirstName,
                           LastName = user.LastName,
                           MiddleName = user.MiddleName
                       };
        }

        public async Task<IHttpActionResult> Get(string id)
        {
            if (id != null)
            {
                var user = await this._userManager.FindByIdAsync(id);
                if (user != null)
                {
                    User userVM = this.ToUser(user);
                    return this.Ok(userVM);
                }

                return this.NotFound();
            }

            return this.BadRequest("User id must be supplied");
        }

        public async Task<IHttpActionResult> Post(User newUser)
        {
            if (newUser != null)
            {
                SkylineUser user = ToSkylineUser(newUser);
                var result = await this._userManager.CreateAsync(user, newUser.Password);
                if (result.Succeeded)
                {
                    var dbUser = await this._userManager.FindByNameAsync(newUser.UserName);
                    if (dbUser != null)
                    {
                        var addRoleResult = await this._userManager.AddToRoleAsync(dbUser.Id, "User");
                        if (addRoleResult.Succeeded)
                        {
                            return this.Ok(newUser);
                        }

                        AddErrorsToModelState(addRoleResult);                      
                    }              
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
