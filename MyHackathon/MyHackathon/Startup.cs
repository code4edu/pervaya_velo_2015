using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyHackathon.Startup))]
namespace MyHackathon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
