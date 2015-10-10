using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CDMISrestful
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            //属性路由
            config.MapHttpAttributeRoutes();

            //基于公约的路由
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "Api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.EnableQuerySupport();
        }
    }
}
