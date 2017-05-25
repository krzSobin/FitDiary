using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(FitDiary.SecuredApi.Startup))]

namespace FitDiary.SecuredApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
