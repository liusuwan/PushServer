using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PushServer.Startup))]
namespace PushServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
