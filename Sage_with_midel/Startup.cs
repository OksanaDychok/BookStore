using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sage_with_midel.Startup))]
namespace Sage_with_midel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
