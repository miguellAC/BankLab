using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ejemplo.Web.PortalReportes
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
