using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skyline.Api.Application.Infrastructure.Identity
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;

    public class SkylineAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            SkylineUserManager storeUserMgr =
                context.OwinContext.Get<SkylineUserManager>(
                    "AspNet.Identity.Owin:" + typeof(SkylineUserManager).AssemblyQualifiedName);
            SkylineUser user = await storeUserMgr.FindAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "The username or password is incorrect");
            }
            else
            {
            }

            ClaimsIdentity ident = await storeUserMgr.CreateIdentityAsync(user, "Custom");
            AuthenticationTicket ticket = new AuthenticationTicket(ident, new AuthenticationProperties());
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(ident);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }
    }
}