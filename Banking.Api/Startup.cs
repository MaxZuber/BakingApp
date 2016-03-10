using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Banking.Api.Startup))]

namespace Banking.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
