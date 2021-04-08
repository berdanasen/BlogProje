using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogProje.Startup))]
namespace BlogProje
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
