using System.Web.Mvc;

namespace Ejemplo.Web.PortalReportes.Controllers
{
    public class ReportesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ejecutar(int reporteId)
        {
            ViewBag.ReporteId = reporteId;
            return View();
        }
    }
}
