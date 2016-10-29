using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skyline.Data.Validators
{
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    public class CustomPasswordValidator : PasswordValidator
    {
        public override async Task<IdentityResult> ValidateAsync(string password)
        {
            var result = await base.ValidateAsync(password);
            if (password.Contains("123456"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Password cannot contain numeric sequences");
                result = new IdentityResult(errors);
            }

            return result;
        }
    }
}