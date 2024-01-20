using ProyectoSC_601.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSC_601.Controllers
{
    public class FacturacionController : Controller
    {

        FacturacionModel modelFacturacion = new FacturacionModel();

        //Historial de Compras Cliente

        [HttpGet]
        public ActionResult FacturacionCliente()
        {
            var datos = modelFacturacion.ConsultaFacturasCliente(long.Parse(Session["ID_Cliente"].ToString()));
            return View(datos);
        }

        [HttpGet]
        public ActionResult FacturaDetalleCliente(long q)
        {
            var datos = modelFacturacion.ConsultaDetalleFactura(q);
            return View(datos);
        }


        // Consulta Facturas Administrador 

        [HttpGet]
        public ActionResult Facturacion()
        {
            var datos = modelFacturacion.ConsultaFacturasAdmin();
            return View(datos);
        }

        [HttpGet]
        public ActionResult FacturaDetalle(long q)
        {
            var datos = modelFacturacion.ConsultaDetalleFactura(q);
            return View(datos);
        }

    }
}