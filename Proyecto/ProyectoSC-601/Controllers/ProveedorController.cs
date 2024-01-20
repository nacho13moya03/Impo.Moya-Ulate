using ProyectoSC_601.Entities;
using ProyectoSC_601.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace ProyectoSC_601.Controllers
{
    public class ProveedorController : Controller
    {
        
        ProveedorModel modelProveedor=new ProveedorModel();



        /*Esto devuelve la vista para registrar un proveedor*/
        [HttpGet]
        public ActionResult RegistrarProveedor()
        {
            try
            {
                ViewBag.combo = modelProveedor.ConsultarEmpresas();
                return View();
            }
            catch (Exception ex)
            {
                return ViewBag.Empresas;
            }
        }




        /*Se llama cuando se envía el formulario para registrar un proveedor*/
        [HttpPost]
        public ActionResult RegistrarProveedor(ProveedorEnt entidad)
        {
            try
            {
                string respuesta = modelProveedor.RegistrarProveedor(entidad);

                if (respuesta == "OK")
                {
                    TempData["RegistroExito"] = "El proveedor se registró correctamente.";
                    return RedirectToAction("ConsultaProveedores", "Proveedor");
                }
                else
                {
                    ViewBag.MensajeUsuario = "No se ha podido registrar la informacón del proveedor";
                    ViewBag.combo = modelProveedor.ConsultarEmpresas();
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }



        /* Se llama cuando se solicita la página de consulta de proveedores para mostrar los datos de todos los proveedores*/
        [HttpGet]
        public ActionResult ConsultaProveedores()
        {
            try
            {
                var datos = modelProveedor.ConsultaProveedores();
                return View(datos);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }



        /*Se llama cuando se desea actualizar el estado de un proveedor.*/
        [HttpGet]
        public ActionResult ActualizarEstadoProveedor(long q)
        {
            try
            {
                var entidad = new ProveedorEnt();
                entidad.ID_Proveedor = q;

                string respuesta = modelProveedor.ActualizarEstadoProveedor(entidad);

                if (respuesta == "OK")
                {
                    return RedirectToAction("ConsultaProveedores", "Proveedor");
                }
                else
                {
                    ViewBag.MensajeUsuario = "No se ha podido cambiar el estado del proveedor";
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }



        /*Se llama cuando se desea actualizar la información de un proveedor modificada
          Este sirve para Visualizar los datos del proveedor y la lista de empresas.*/
        [HttpGet]
        public ActionResult ActualizarProveedor(long q)
        {
            try
            {
                var datos = modelProveedor.ConsultaProveedor(q);
                ViewBag.combo = modelProveedor.ConsultarEmpresas();
                return View(datos);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }



        /*Este procesa la actualización de datos de un proveedor desde un formulario y redirige.*/
        [HttpPost]
        public ActionResult ActualizarProveedor(ProveedorEnt entidad)
        {
            try
            {
                string respuesta = modelProveedor.ActualizarProveedor(entidad);

                if (respuesta == "OK")
                {
                    TempData["ActualizacionExito"] = "Proveedor actualizado con éxito";
                    return RedirectToAction("ConsultaProveedores", "Proveedor");
                }
                else
                {
                    ViewBag.MensajeUsuario = "No se ha podido actualizar la información del proveedor";
                    ViewBag.combo = modelProveedor.ConsultarEmpresas();
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }



        /*Se llama cuando se solicita eliminar un proveedor.*/
        [HttpGet]
        public ActionResult EliminarProveedor(long q)
        {
            try
            {
                string respuesta = modelProveedor.EliminarProveedor(q);

                // Imprime la respuesta en la consola para depuración
                Console.WriteLine($"Respuesta del servicio: {respuesta}");

                if (respuesta == "OK")
                {
                    TempData["ActualizacionExito"] = "Proveedor eliminado con éxito";
                    return RedirectToAction("ConsultaProveedores", "Proveedor");
                }
                else
                {
                    ViewBag.MensajeUsuario = "No se ha podido eliminar el proveedor.";
                    return View("ConsultaProveedores", "Proveedor");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }





    }
}