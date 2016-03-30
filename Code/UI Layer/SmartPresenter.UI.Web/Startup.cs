using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartPresenter.UI.Web.Startup))]
namespace SmartPresenter.UI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
