using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AspChat {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var mvcRouteHandler = new MvcRouteHandler();

            routes.Add("Empty", 
                       new Route(
                           "", 
                           new RouteValueDictionary(new { controller = "Home", action = "Index" }), 
                           mvcRouteHandler
                       )
            );

            routes.Add("Auth", 
                       new Route(
                           "{action}", 
                           new RouteValueDictionary(new { controller = "Auth" }),
                           mvcRouteHandler
                       )
            );
        }
    }
}
