using ACE.LMS.Core.Infrastructure;
using ACE.LMS.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ACE.LMS.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //initialize engine context
            EngineContext.Initialize(false);

            //set dependency resolver
            var dependencyResolver = new ACEDependencyResolver();
            DependencyResolver.SetResolver(dependencyResolver);
        }
    }
}
