using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Semana09ASPNet.Startup))]
namespace Semana09ASPNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
