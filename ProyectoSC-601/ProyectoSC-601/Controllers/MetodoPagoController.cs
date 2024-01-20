using ProyectoSC_601.Entities;
using ProyectoSC_601.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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