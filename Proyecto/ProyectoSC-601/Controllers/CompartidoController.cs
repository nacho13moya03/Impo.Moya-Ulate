using ProyectoSC_601.Entities;
using ProyectoSC_601.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSC_601.Controllers
{
    public class CompartidoController : Controller
    {

        IndexModel modelIndex = new IndexModel();

        [HttpGet]
        public ActionResult Acerca()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contacto()
        {
            return View();
        }

        //Se llama al modelo para enviar la informacion de la seccion contactenos
        [HttpPost]
        public ActionResult Contacto(InfoIndex entidad)
        {
            string respuesta = modelIndex.EnviarInformacion(entidad);

            if (respuesta == "OK")
            {
                ViewBag.MensajeExitoso = "La información se ha enviado con éxito";
                ViewBag.CantidadClientes = modelIndex.ContarClientes();
                return View();
            }
            else
            {
                ViewBag.MensajeNoExitoso = "No se ha podido enviar la informacion";
                ViewBag.CantidadClientes = modelIndex.ContarClientes();
                return View();
            }
        }

    }
}