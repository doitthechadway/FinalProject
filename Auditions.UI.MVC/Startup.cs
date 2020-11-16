using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Auditions.UI.MVC.Startup))]
namespace Auditions.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
