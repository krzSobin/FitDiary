using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(FitDiary.SecuredApi.Startup))]

namespace FitDiary.SecuredApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);
        }
    }
}
