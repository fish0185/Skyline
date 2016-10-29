using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skyline.Data.Validators
{
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;

    using Skyline.Data.Entities;
    using Skyline.Data.Infrastructure;

    public class CustomUserValidator : UserValidator<SkylineUser>
    {
        public CustomUserValidator(SkylineUserManager mgr)
            : base(mgr)
        {
            
        }

        public override async Task<IdentityResult> ValidateAsync(SkylineUser user )
        {
            IdentityResult result = await base.ValidateAsync(user);
            
            // Custom Validation
            if (user.UserName.ToLowerInvariant() == "admin")
            {
                var errors = result.Errors.ToList();
                errors.Add("admin is not a valid user name");
                result = new IdentityResult(errors);
            }
            return result;
        }
    }
}