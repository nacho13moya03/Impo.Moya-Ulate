using ProyectoSC_601.Entities;
using ProyectoSC_601.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoSC_601.Controllers
{
    public class ComprasController : Controller
    {
        ComprasModel modelCompras = new ComprasModel();


        /*Se llama cuando se envía el formulario para registrar un proveedor*/
        [HttpGet]
        public ActionResult RegistrarCompra()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Empresas = modelCompras.ConsultarEmpresas();  // Configurar ViewBag.Empresas
                    return View();
                }
                else
                {
                    ViewBag.combo = modelCompras.ConsultaCompras();
                    ViewBag.Empresas = modelCompras.ConsultarEmpresas();  // Configurar ViewBag.Empresas
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        /*Se llama cuando se envía el formulario para registrar un proveedor*/
        [HttpPost]
        public ActionResult RegistrarCompra(ComprasEnt entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Continuar con el registro solo si la cédula no existe
                    string respuesta = modelCompras.RegistrarCompra(entidad);

                    if (respuesta == "OK")
                    {
                        TempData["RegistroExito"] = "La compra se registró correctamente.";
                        return RedirectToAction("ConsultaCompras");
                    }
                    else
                    {
                        ViewBag.MensajeUsuario = "No se ha podido registrar la compra";
                        ViewBag.Empresas = modelCompras.ConsultarEmpresas();  // Configurar ViewBag.Empresas
                        return View();
                    }
                }
                else
                {
                    ViewBag.combo = modelCompras.ConsultarEmpresas();
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }


        /* Se llama cuando se solicita la página de consulta de proveedores para mostrar los datos de todos los proveedores*/
        public ActionResult ConsultaCompras()
        {
            try
            {
                var empresas = modelCompras.ConsultarEmpresas();

                if (empresas != null && empresas.Any())
                {
                    ViewBag.Empresas = empresas;
                }
                else
                {
                    ViewBag.Empresas = new List<SelectListItem>();
                }

                var datos = modelCompras.ConsultaCompras();
                return View(datos);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }









        /* Se llama cuando se desea actualizar la información de un proveedor modificada
   Este sirve para Visualizar los datos del proveedor y la lista de empresas. */
        [HttpGet]
        public ActionResult ConsultaCompra(long q)
        {
            try
            {
                ViewBag.Empresas = modelCompras.ConsultarEmpresas();  // Configurar ViewBag.Empresas
                var compra = modelCompras.ConsultaCompra(q);
                return View(compra); ;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al cargar los datos de la compra.");
                return View();
            }
        }



        [HttpGet]
        public ActionResult ActualizarCompra(long q)
        {
            try
            {
                ViewBag.Empresas = modelCompras.ConsultarEmpresas();  // Configurar ViewBag.Empresas
                var datos = modelCompras.ConsultaCompra(q);               
                return View(datos);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al cargar los datos de la compra.");
                return View();
            }
        }


        [HttpPost]
        public ActionResult ActualizarCompra(ComprasEnt entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string respuesta = modelCompras.ActualizarCompra(entidad);

                    Console.WriteLine($"Respuesta del servicio al actualizar compra: {respuesta}");

                    if (respuesta == "OK")
                    {
                        TempData["ActualizacionExito"] = "Compra actualizada con éxito";
                        return RedirectToAction("ConsultaCompra", "Compras", new { q = entidad.IdCompras });
                    }
                }
                ViewBag.Empresas = modelCompras.ConsultarEmpresas();  // Configurar ViewBag.Empresas
                TempData["ActualizacionError"] = "Error al actualizar la compra";
                return RedirectToAction("ActualizarCompra", "Compras");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar compra: {ex.Message}");
                return View("Error");
            }
        }




        /*Se llama cuando se solicita eliminar un proveedor.*/
        [HttpGet]
        public ActionResult EliminarCompra(long q)
        {
            try
            {
                string respuesta = modelCompras.EliminarCompra(q);

                // Imprime la respuesta en la consola para depuración
                Console.WriteLine($"Respuesta del servicio: {respuesta}");

                if (respuesta == "OK")
                {
                    TempData["ActualizacionExito"] = "Compra eliminado con éxito";
                    return RedirectToAction("ConsultaCompras", "Compras");
                }
                else
                {
                    ViewBag.MensajeUsuario = "No se ha podido eliminar la compra.";
                    return View("ConsultaCompras", "Compras");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }




    }
}
