using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sozial.Startup))]
namespace Sozial
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
