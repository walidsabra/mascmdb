using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CMDB01.Startup))]
namespace CMDB01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}