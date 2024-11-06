using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmyckaReceiptService.Startup))]
namespace SmyckaReceiptService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }


    }
}
