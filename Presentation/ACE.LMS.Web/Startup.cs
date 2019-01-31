using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ACE.LMS.Web.Startup))]
namespace ACE.LMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
