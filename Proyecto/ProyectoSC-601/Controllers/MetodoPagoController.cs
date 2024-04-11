using System.Web.Mvc;

namespace ProyectoSC_601.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class MetodoPagoController : Controller
    {
        // GET: MetodoPago
        [HttpGet]
        public ActionResult MetodoPago()
        {
            return View();
        }
    }
}