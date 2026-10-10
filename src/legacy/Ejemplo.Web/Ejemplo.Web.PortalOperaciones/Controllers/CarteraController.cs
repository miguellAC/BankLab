using System.Web.Mvc;

namespace Ejemplo.Web.PortalOperaciones.Controllers
{
    public class CarteraController : Controller
    {
        public ActionResult Index(string filtro)
        {
            ViewBag.Filtro = filtro;
            return View();
        }
    }
}
