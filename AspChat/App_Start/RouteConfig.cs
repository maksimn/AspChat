using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AspChat {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.Add("Empty", new Route(url: "", routeHandler: new ChatRouteHandler()));
        }
    }

    class ChatRouteHandler : IRouteHandler {
        public IHttpHandler GetHttpHandler(RequestContext requestContext) {
            var url = requestContext.HttpContext.Request.Url != null ? requestContext.HttpContext.Request.Url : null;
            if (url != null && url.AbsolutePath.Length == 1) {
                requestContext.RouteData.Values["controller"] = "home";
                requestContext.RouteData.Values["action"] = "index";                
            }
            return new MvcHandler(requestContext);
        }
    }
}
