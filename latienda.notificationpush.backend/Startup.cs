using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(latienda.notificationpush.backend.Startup))]

namespace latienda.notificationpush.backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}