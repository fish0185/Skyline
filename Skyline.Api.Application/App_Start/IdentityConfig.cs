using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Owin;

[assembly: OwinStartup(typeof(Skyline.Api.Application.IdentityConfig))]
namespace Skyline.Api.Application
{
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OAuth;

    using Owin;

    using Skyline.Api.Application.Infrastructure.Identity;

    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<SkylineIdentityDbContext>(SkylineIdentityDbContext.Create);
            app.CreatePerOwinContext<SkylineUserManager>(SkylineUserManager.Create);
            app.CreatePerOwinContext<SkylineRoleManager>(SkylineRoleManager.Create);
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            //});

            app.UseOAuthBearerTokens(new OAuthAuthorizationServerOptions
            {
                Provider = new SkylineAuthProvider(),
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Authenticate")
            });
        }
    }
}