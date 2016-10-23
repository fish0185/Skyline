using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skyline.Api.Application.Infrastructure.Identity
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;

    using Skyline.Data.Entities;
    using Skyline.Data.Infrastructure;

    public class SkylineAuthProvider : OAuthAuthorizationServerProvider
    {

        //private SkylineUserManager _userManager;

        //private IComparable c;
        //public SkylineAuthProvider(SkylineUserManager manager)
        //{
        //    //this.c = cc;
        //    _userManager = manager;
        //}

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            SkylineUserManager storeUserMgr = (SkylineUserManager)GlobalConfiguration.Configuration.DependencyResolver.BeginScope().GetService(typeof(SkylineUserManager));
            SkylineUser user = await storeUserMgr.FindAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "The username or password is incorrect");
            }
            else
            {
                //_userManager.CreateIdentity()
                ClaimsIdentity ident = await storeUserMgr.CreateIdentityAsync(user, "Custom");
                AuthenticationTicket ticket = new AuthenticationTicket(ident, new AuthenticationProperties());
                context.Validated(ticket);
                context.Request.Context.Authentication.SignIn(ident);
            }
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }
    }
}