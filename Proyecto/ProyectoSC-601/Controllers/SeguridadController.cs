using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB_ImpoMoyaUlate.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class SeguridadController : Controller
    {
        // GET: Seguridad
        [HttpGet]
        public ActionResult NoAcceso()
        {
            Session.Clear();
            return View();
        }
    }
}
