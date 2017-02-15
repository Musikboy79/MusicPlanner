using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MusicPlanner.Startup))]
namespace MusicPlanner
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
