using System.Web.Mvc;

namespace ProyectoSC_601.Controllers
{
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