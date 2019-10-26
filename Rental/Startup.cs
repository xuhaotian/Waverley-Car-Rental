using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rental.Startup))]
namespace Rental
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
