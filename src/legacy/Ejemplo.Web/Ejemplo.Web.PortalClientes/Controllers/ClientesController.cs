using System.Web.Mvc;

namespace Ejemplo.Web.PortalClientes.Controllers
{
    public class ClientesController : Controller
    {
        public ActionResult Index(string busqueda, int page = 1)
        {
            ViewBag.Busqueda = busqueda;
            ViewBag.Page = page;
            return View();
        }

        public ActionResult Detalle(int id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
