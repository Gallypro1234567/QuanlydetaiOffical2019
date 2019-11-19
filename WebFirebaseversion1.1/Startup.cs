using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebFirebaseversion1._1.Startup))]
namespace WebFirebaseversion1._1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
