using System.Web.Http;
using Microsoft.Owin.Security.OAuth;

namespace FitDiary.SecuredApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config) 
        {
            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.EnableCors();
            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
