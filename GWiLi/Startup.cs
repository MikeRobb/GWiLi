using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GWiLi.Startup))]
namespace GWiLi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
