using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BandDrop.Startup))]
namespace BandDrop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
