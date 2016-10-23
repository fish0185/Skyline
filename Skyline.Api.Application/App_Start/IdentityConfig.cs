using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Owin;

[assembly: OwinStartup(typeof(Skyline.Api.Application.IdentityConfig))]
namespace Skyline.Api.Application
{
    using System.Reflection;
    using System.Web.Http;

    using Autofac;
    using Autofac.Integration.WebApi;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OAuth;

    using Owin;

    using Skyline.Api.Application.Infrastructure.Identity;
    using Skyline.Data.Concrete;
    using Skyline.Data.Infrastructure;
    using Skyline.Data.Repositories;
    using Skyline.Services;

    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {           
            app.UseOAuthBearerTokens(new OAuthAuthorizationServerOptions
            {
                Provider = new SkylineAuthProvider(),
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Authenticate")
            });
        }
    }
}