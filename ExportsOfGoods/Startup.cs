using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExportsOfGoods.Startup))]
namespace ExportsOfGoods
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
