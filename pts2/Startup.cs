using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(pts2.Startup))]
namespace pts2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
