namespace XxlStore
{
    public class HostNameConstraint : IRouteConstraint
    {
        protected List<UrlPreset> Presets;

        public HostNameConstraint(List<UrlPreset> presets)
        {
            Presets = presets;
        }

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return Match(httpContext);
        }

        public bool Match(HttpContext httpContext)
        {
            string host = httpContext.Request.Host.Host;
            for (int i = 0; i < Presets.Count; i++) {
                var p = Presets[i];
                if (p.Host == host) {
                    httpContext.Items["UrlPreset"] = p;
                    return true;
                }
            }
            return false;
        }
    }
}
