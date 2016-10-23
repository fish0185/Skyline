using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skyline.Api.Application
{
    using System.Data.Entity;
    using System.Reflection;
    using System.Web.Http;

    using Autofac;
    using Autofac.Integration.WebApi;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.DataProtection;

    using Skyline.Api.Application.Infrastructure.Identity;
    using Skyline.Data.Concrete;
    using Skyline.Data.Entities;
    using Skyline.Data.Infrastructure;
    using Skyline.Data.Repositories;
    using Skyline.Services;

    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            // Repositories
            builder.RegisterAssemblyTypes(typeof(NewsRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            // Services
            builder.RegisterAssemblyTypes(typeof(NewsService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();

            // Register Identity
            builder.RegisterType<SkylineDbContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<SkylineRoleManager>().AsSelf().InstancePerRequest(); 
            //builder.RegisterType<RoleStore<SkylineRole>>().AsSelf().InstancePerRequest();
            builder.RegisterType<SkylineUserManager>().AsSelf().InstancePerRequest();
            //builder.RegisterType<UserStore<SkylineUser>>().AsSelf().InstancePerRequest();
            builder.Register(ctx => new RoleStore<SkylineRole>(ctx.Resolve<SkylineDbContext>())).AsSelf().InstancePerRequest();
            builder.Register(ctx => new UserStore<SkylineUser>(ctx.Resolve<SkylineDbContext>())).AsSelf().AsImplementedInterfaces().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();        

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}