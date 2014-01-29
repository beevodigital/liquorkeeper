using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LiquorKeeper.Startup))]
namespace LiquorKeeper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
