using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleHealthTracking.Web.Startup))]
namespace SimpleHealthTracking.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
