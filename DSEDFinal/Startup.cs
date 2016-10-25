using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DSEDFinal.Startup))]
namespace DSEDFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
