using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReportApp.WebUI.Startup))]
namespace ReportApp.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
