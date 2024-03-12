using ProyectoSC_601.Entities;
using ProyectoSC_601.Models;
using System.Web.Mvc;

namespace ProyectoSC_601.Controllers
{
    public class PedidosController : Controller
    {

        PedidosModel modelPedidos = new PedidosModel();

        [HttpGet]
        public ActionResult ConsultarPedidos()
        {
            var datos = modelPedidos.ConsultarPedidos();
            return View(datos);
        }

        [HttpGet]
        public ActionResult ActualizarEstadoPedido(long q)
        {
            var entidad = new PedidoEnt();
            entidad.ID_Pedido = q;

            string respuesta = modelPedidos.ActualizarEstadoPedido(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("ConsultarPedidos", "Pedidos");
            }
            else
            {
                ViewBag.Mensaje = "No se ha podido cambiar el estado del pedido";
                return View();
            }
        }
    }
}