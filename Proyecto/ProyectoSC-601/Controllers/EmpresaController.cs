using ProyectoSC_601.Entities;
using ProyectoSC_601.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace ProyectoSC_601.Controllers
{
    public class EmpresaController : Controller
    {
        
        EmpresaModel modelEmpresa=new EmpresaModel();



        /*Esto devuelve la vista para registrar una empresa*/
        [HttpGet]
        public ActionResult RegistrarEmpresa()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }




        /*Se llama cuando se envía el formulario para registrar un proveedor*/
        [HttpPost]
        public ActionResult RegistrarEmpresa(EmpresaEnt entidad)
        {
            try
            {
                string respuesta = modelEmpresa.RegistrarEmpresa(entidad);

                if (respuesta == "OK")
                {
                    TempData["RegistroExito"] = "La empresa se registró correctamente.";
                    return RedirectToAction("ConsultaEmpresas", "Empresa");
                }
                else
                {
                    ViewBag.MensajeUsuario = "No se ha podido registrar la informacón de la empresa";
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }



        /* Se llama cuando se solicita la página de consulta de empresas para mostrar los datos de todas las empresas*/
        [HttpGet]
        public ActionResult ConsultaEmpresas()
        {
            try
            {
                var datos = modelEmpresa.ConsultaEmpresas();
                return View(datos);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }



        /*Se llama cuando se desea actualizar la información de una empresa modificada
          Este sirve para Visualizar los datos de la empresa*/
        [HttpGet]
        public ActionResult ActualizarEmpresa(long q)
        {
            try
            {
                var datos = modelEmpresa.ConsultaEmpresa(q);
                return View(datos);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }


        /*Este procesa la actualización de datos de un proveedor desde un formulario y redirige.*/
        [HttpPost]
        public ActionResult ActualizarEmpresa(EmpresaEnt entidad)
        {
            try
            {
                string respuesta = modelEmpresa.ActualizarEmpresa(entidad);

                if (respuesta == "OK")
                {
                    TempData["ActualizacionExito"] = "Empresa actualizada con éxito";
                    return RedirectToAction("ConsultaEmpresas", "Empresa");
                }
                else
                {
                    ViewBag.MensajeUsuario = "No se ha podido actualizar la información de la empresa";
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
        public ActionResult EliminarEmpresa(long q)
        {
            try
            {
                string respuesta = modelEmpresa.EliminarEmpresa(q);

                // Imprime la respuesta en la consola para depuración
                Console.WriteLine($"Respuesta del servicio: {respuesta}");

                if (respuesta == "OK")
                {
                    TempData["ActualizacionExito"] = "Empresa eliminada con éxito";
                    return RedirectToAction("ConsultaEmpresas", "Empresa");
                }
                else
                {
                    ViewBag.MensajeUsuario = "No se ha podido eliminar la empresa.";
                    return View("ConsultarEmpresas", "Empresa");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }





    }
}