using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyStockScreener.Startup))]
namespace MyStockScreener
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
