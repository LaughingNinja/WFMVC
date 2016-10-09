using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WFMVCProject.Startup))]
namespace WFMVCProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
