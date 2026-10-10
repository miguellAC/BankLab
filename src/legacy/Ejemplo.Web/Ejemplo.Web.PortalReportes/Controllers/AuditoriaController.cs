using System.Web.Mvc;

namespace Ejemplo.Web.PortalReportes.Controllers
{
    public class AuditoriaController : Controller
    {
        public ActionResult Index(string busqueda)
        {
            ViewBag.Busqueda = busqueda;
            return View();
        }
    }
}
