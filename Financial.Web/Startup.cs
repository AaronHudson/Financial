using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Financial.Web.Startup))]
namespace Financial.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
