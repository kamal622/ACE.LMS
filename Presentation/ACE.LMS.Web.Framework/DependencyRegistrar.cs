using ACE.LMS.Core.Models.Library;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Integration.Mvc;
using ACE.LMS.Core.Infrastructure;
using ACE.LMS.Core.Infrastructure.DependencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Reflection;
using ACE.LMS.Core;
using ACE.LMS.Services.Library;
using ACE.LMS.Data;
using ACE.LMS.Core.Data;
using ACE.LMS.Core.Caching;
using ACE.LMS.Services.Logging;
using ACE.LMS.Core.Fakes;
using ACE.LMS.Services.Directory;

namespace ACE.LMS.Web.Framework
{
     public class DependencyRegistrar : IDependencyRegistrar
    {
         public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
         {
             //HTTP context and other related stuff
             builder.Register(c =>
                 //register FakeHttpContext when HttpContext is not available
                 HttpContext.Current != null ?
                 (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
                 (new FakeHttpContext("~/") as HttpContextBase))
                 .As<HttpContextBase>()
                 .InstancePerRequest();
             builder.Register(c => c.Resolve<HttpContextBase>().Request)
                 .As<HttpRequestBase>()
                 .InstancePerRequest();
             builder.Register(c => c.Resolve<HttpContextBase>().Response)
                 .As<HttpResponseBase>()
                 .InstancePerRequest();
             builder.Register(c => c.Resolve<HttpContextBase>().Server)
                 .As<HttpServerUtilityBase>()
                 .InstancePerRequest();
             builder.Register(c => c.Resolve<HttpContextBase>().Session)
                 .As<HttpSessionStateBase>()
                 .InstancePerRequest();
             //builder.RegisterType<SettingService>().As<ISettingService>()
             //   .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("ace_cache_static"))
             //   .InstancePerHttpRequest();
             //builder.RegisterSource(new SettingsSource());
             
             builder.Register<IDbContext>(c => new LMSContext()).InstancePerRequest();
             //builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerRequest();
             builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

             //cache manager
             builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("nop_cache_static").SingleInstance();
             builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().Named<ICacheManager>("nop_cache_per_request").InstancePerRequest();

             //web helper
             builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();

             builder.RegisterType<CollegeBranchService>().InstancePerRequest();
             builder.RegisterType<UserService>().InstancePerRequest();
             builder.RegisterType<BookRequestService>().InstancePerRequest();
             builder.RegisterType<BookService>().InstancePerRequest();
             builder.RegisterType<StudentService>().InstancePerRequest();
             builder.RegisterType<BookRequestDetailRepository>().InstancePerRequest();
             builder.RegisterType<UserProfileService>().InstancePerRequest();
             builder.RegisterType<HistoryService>().InstancePerRequest();
             builder.RegisterType<IssueBookService>().InstancePerRequest();
             builder.RegisterType<DirectoryService>().InstancePerRequest();
             builder.RegisterType<LibraryBookService>().InstancePerRequest();
             builder.RegisterType<NoticeBoardService>().InstancePerRequest();
             builder.RegisterType<CategoryService>().InstancePerRequest();
             builder.RegisterType<BookCategoryService>().InstancePerRequest();

             builder.RegisterType<DefaultLogger>().As<ILogger>().InstancePerLifetimeScope();
             builder.RegisterType<BaseEntity>();
         }

         public int Order
         {
             get { return 0; }
         }
    }

     //public class SettingsSource : IRegistrationSource
     //{
     //    static readonly MethodInfo BuildMethod = typeof(SettingsSource).GetMethod(
     //        "BuildRegistration",
     //        BindingFlags.Static | BindingFlags.NonPublic);

     //    public IEnumerable<IComponentRegistration> RegistrationsFor(
     //            Service service,
     //            Func<Service, IEnumerable<IComponentRegistration>> registrations)
     //    {
     //        var ts = service as TypedService;
     //        if (ts != null && typeof(ISettings).IsAssignableFrom(ts.ServiceType))
     //        {
     //            var buildMethod = BuildMethod.MakeGenericMethod(ts.ServiceType);
     //            yield return (IComponentRegistration)buildMethod.Invoke(null, null);
     //        }
     //    }

     //    static IComponentRegistration BuildRegistration<TSettings>() where TSettings : ISettings, new()
     //    {
     //        return RegistrationBuilder
     //            .ForDelegate((c, p) =>
     //            {
     //                //var currentStoreId = c.Resolve<IClientContext>().CurrentClient.Id;
     //                //uncomment the code below if you want load settings per store only when you have two stores installed.
     //                //var currentStoreId = c.Resolve<IClientService>().GetAllStores().Count > 1 ? c.Resolve<IClientContext>().CurrentStore.Id : 0;

     //                //although it's better to connect to your database and execute the following SQL:
     //                //DELETE FROM [Setting] WHERE [StoreId] > 0
     //                return c.Resolve<ISettingService>().LoadSetting<TSettings>();
     //            })
     //            .InstancePerHttpRequest()
     //            .CreateRegistration();
     //    }

     //    public bool IsAdapterForIndividualComponents { get { return false; } }
     //}
}
