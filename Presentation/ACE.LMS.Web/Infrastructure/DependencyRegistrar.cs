using ACE.LMS.Web.Framework.Mvc;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Integration.Mvc;
using ACE.LMS.Core.Infrastructure.DependencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACE.LMS.Web.Controllers;
using ACE.LMS.Core.Caching;
using ACE.LMS.Core;

namespace ACE.LMS.Web.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(Autofac.ContainerBuilder builder, Core.Infrastructure.ITypeFinder typeFinder)
        {
            //we cache presentation models between requests
            builder.RegisterType<HomeController>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("ace_cache_static"));
            builder.RegisterType<LibraryController>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("ace_cache_static"));
            builder.RegisterType<AdminController>()
              .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("ace_cache_static"));
            builder.RegisterType<AccountController>()
             .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("ace_cache_static"));
            builder.RegisterType<StudentController>()
                  .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("ace_cache_static"));

            //builder.RegisterType<RegisterExternalLoginModel>().InstancePerHttpRequest();
            builder.RegisterType<WebHelper>().InstancePerRequest();
        }

        public int Order
        {
            get { return 2; }
        }
    }
}