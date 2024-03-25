using ProyectoSC_601.Entities;
using ProyectoSC_601.Models;
using System;
using System.Web.Mvc;
using WEB_ImpoMoyaUlate.Filters;



namespace ProyectoSC_601.Controllers
{
    public class EmpresaController : Controller
    {

        EmpresaModel modelEmpresa = new EmpresaModel();



        /*Esto devuelve la vista para registrar una empresa*/
        [AuthorizeRol(1)]
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
        [AuthorizeRol(1)]
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
        [AuthorizeRol(1)]
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
        [AuthorizeRol(1)]
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
        [AuthorizeRol(1)]
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
        [AuthorizeRol(1)]
        [HttpGet]
        public ActionResult EliminarEmpresa(long q)
        {
            try
            {
                string respuesta = modelEmpresa.EliminarEmpresa(q);

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



        [AuthorizeRol(1)]
        [HttpGet]
        public ActionResult VerificarEliminarEmpresa(int idEmpresa)
        {
            var entidad = new EmpresaEnt { ID_Empresa = idEmpresa };

            // Verificar si hay proveedores vinculados a la empresa usando el API
            bool hayProveedoresVinculados = modelEmpresa.VerificarProveedoresVinculados(entidad);
            bool hayComprasVinculadas = modelEmpresa.VerificarComprasVinculadas(entidad);

            if (hayProveedoresVinculados || hayComprasVinculadas)
            {
                string message = "No se puede eliminar la empresa porque ";

                if (hayProveedoresVinculados && hayComprasVinculadas)
                {
                    message += "tiene proveedores y compras vinculados. Elimina los proveedores y las compras primero.";
                }
                else if (hayProveedoresVinculados)
                {
                    message += "tiene proveedores vinculados. Elimina los proveedores primero.";
                }
                else // hayComprasVinculadas
                {
                    message += "tiene compras vinculadas. Elimina las compras primero.";
                }

                return Json(new { success = false, message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }





    }
}