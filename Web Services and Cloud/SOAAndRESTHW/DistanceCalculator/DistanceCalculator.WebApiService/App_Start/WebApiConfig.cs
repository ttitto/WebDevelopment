using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace DistanceCalculator.WebApiService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DistanceCalculator",
                routeTemplate: "api/{controller}/{ax}/{ay}/{bx}/{by}",
                defaults: new { ax = RouteParameter.Optional, ay = RouteParameter.Optional, bx = RouteParameter.Optional, by = RouteParameter.Optional }
            );

            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            //var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            //config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}
