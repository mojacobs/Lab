using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RideSharePlus.Startup))]
namespace RideSharePlus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
