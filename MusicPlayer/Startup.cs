using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MusicPlayer.Startup))]
namespace MusicPlayer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
