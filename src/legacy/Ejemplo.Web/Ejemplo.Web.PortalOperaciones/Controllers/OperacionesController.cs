using System.Web.Mvc;

namespace Ejemplo.Web.PortalOperaciones.Controllers
{
    public class OperacionesController : Controller
    {
        public ActionResult Index(string busqueda)
        {
            ViewBag.Busqueda = busqueda;
            return View();
        }

        public ActionResult Detalle(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult Cuotas(int operacionId)
        {
            ViewBag.OperacionId = operacionId;
            return View();
        }
    }
}
