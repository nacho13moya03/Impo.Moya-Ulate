using ProyectoSC_601.Entities;
using ProyectoSC_601.Models;
using System.Web.Mvc;

namespace ProyectoSC_601.Controllers
{
    public class CompartidoController : Controller
    {

        IndexModel modelIndex = new IndexModel();

        [HttpGet]
        public ActionResult Acerca()
        {
            if (Session["ID_Usuario"] != null && Session["Rol"] != null && long.Parse(Session["Rol"].ToString()) == 2)
            {
                // Obtiene la cantidad de productos diferentes en el carrito
                int cantidadProductos = modelIndex.ObtenerCantidadProductosEnCarrito(long.Parse(Session["ID_Usuario"].ToString()));

                // Pasa la cantidad de productos a la vista
                ViewBag.CantidadProductosEnCarrito = cantidadProductos;
            }
            return View();
        }

        [HttpGet]
        public ActionResult Contacto()
        {
            if (Session["ID_Usuario"] != null && Session["Rol"] != null && long.Parse(Session["Rol"].ToString()) == 2)
            {
                // Obtiene la cantidad de productos diferentes en el carrito
                int cantidadProductos = modelIndex.ObtenerCantidadProductosEnCarrito(long.Parse(Session["ID_Usuario"].ToString()));

                // Pasa la cantidad de productos a la vista
                ViewBag.CantidadProductosEnCarrito = cantidadProductos;
            }
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
                ViewBag.CantidadClientes = modelIndex.ContarUsuarios();
                if (Session["ID_Usuario"] != null && Session["Rol"] != null && long.Parse(Session["Rol"].ToString()) == 2)
                {
                    // Obtiene la cantidad de productos diferentes en el carrito
                    int cantidadProductos = modelIndex.ObtenerCantidadProductosEnCarrito(long.Parse(Session["ID_Usuario"].ToString()));

                    // Pasa la cantidad de productos a la vista
                    ViewBag.CantidadProductosEnCarrito = cantidadProductos;
                }
                return View();
            }
            else
            {
                ViewBag.MensajeNoExitoso = "No se ha podido enviar la informacion";
                ViewBag.CantidadClientes = modelIndex.ContarUsuarios();
                if (Session["ID_Usuario"] != null && Session["Rol"] != null && long.Parse(Session["Rol"].ToString()) == 2)
                {
                    // Obtiene la cantidad de productos diferentes en el carrito
                    int cantidadProductos = modelIndex.ObtenerCantidadProductosEnCarrito(long.Parse(Session["ID_Usuario"].ToString()));

                    // Pasa la cantidad de productos a la vista
                    ViewBag.CantidadProductosEnCarrito = cantidadProductos;
                }
                return View();
            }
        }

    }
}