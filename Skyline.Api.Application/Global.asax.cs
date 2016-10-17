using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Skyline.Api.Application
{
    using Skyline.Data;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Init database
            System.Data.Entity.Database.SetInitializer(new SkylineSeedData());

            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Autofac
            Bootstrapper.Run();
        }
    }
}
