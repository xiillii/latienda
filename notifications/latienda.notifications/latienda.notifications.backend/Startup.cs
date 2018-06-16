using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(latienda.notifications.backend.Startup))]

namespace latienda.notifications.backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}