using ProyectoSC_601.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSC_601.Controllers
{
    public class ComprasController : Controller
    {
        [HttpGet]
        public ActionResult Compras()
        {
            return View();

        }

        [HttpGet]
        public ActionResult ConsultaCompras()
        {
            return View();

        }
    }
}