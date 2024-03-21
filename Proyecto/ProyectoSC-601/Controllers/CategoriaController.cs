using ProyectoSC_601.Entities;
using ProyectoSC_601.Models;
using System;
using System.Web.Mvc;
using System.Web.Services.Description;



namespace ProyectoSC_601.Controllers
{
    public class CategoriaController : Controller
    {

        CategoriaModel modelCategoria = new CategoriaModel();


        /* Consulta todas las categorias registrados en el sistema */
        [HttpGet]
        public ActionResult ConsultarCategoria(string mensaje)
        {
            if (!string.IsNullOrEmpty(mensaje))
            {
                ViewBag.Mensaje = mensaje;
            }
            var datos = modelCategoria.ConsultarCategoria();
            return View(datos);
        }


        //Devuele la vista de registrar productos con las categorias 
        [HttpGet]
        public ActionResult RegistrarCategoria()
        {
            //ViewBag.Categorias = modelCategoria.ConsultarCategoria();
            return View();
        }

        //Registra un producto 
        [HttpPost]
        public ActionResult RegistrarCategoria(string nombre)
        {
            var entidad = new CategoriaEnt();
            entidad.Nombre_Categoria = nombre;
            entidad.Estado_Categoria = 1;

            string ID_Categoria = modelCategoria.RegistrarCategoria(entidad);

            if (ID_Categoria == "OK")
            {
                return RedirectToAction("ConsultarCategoria", "Categoria");
            }
            else if(ID_Categoria == "Repetida")
            {
                ViewBag.Mensaje = "La categoría ya se encuentra registrada";
                return RedirectToAction("ConsultarCategoria", "Categoria", new { mensaje = ViewBag.Mensaje });

            }
            else
            {
                ViewBag.Mensaje = "No se ha podido registrar la categoría";
                return RedirectToAction("ConsultarCategoria", "Categoria", new { mensaje = ViewBag.Mensaje });

            }
        }

        //Cambia el estado de un producto
        [HttpGet]
        public ActionResult ActualizarEstadoCategoria(int q)
        {
            var entidad = new CategoriaEnt();
            entidad.ID_Categoria = q;

            string respuesta = modelCategoria.ActualizarEstadoCategoria(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("ConsultarCategoria", "Categoria");
            }
            else
            {
                ViewBag.Mensaje = "No se ha podido cambiar el estado de la categoria";
                return View();
            }
        }

        [HttpGet]
        public ActionResult EliminarCategoria(int q)
        {
            var entidad = new CategoriaEnt();
            entidad.ID_Categoria = q;

            try
            {
                string respuesta = modelCategoria.EliminarCategoria(entidad);

                if (respuesta == "OK")
                {
                    return RedirectToAction("ConsultarCategoria", "Categoria");
                }
                else
                {
                    ViewBag.Mensaje = "No se ha podido eliminar la categoría";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error inesperado: " + ex.Message;
                return View();
            }
        }


        [HttpGet]
        public ActionResult VerificarEliminarCategoria(int idCategoria)
        {
            var entidad = new CategoriaEnt { ID_Categoria = idCategoria };

            // Verificar si hay productos vinculados a la categoría usando el API
            bool hayProductosVinculados = modelCategoria.VerificarProductosVinculados(entidad);

            if (hayProductosVinculados)
            {
                return Json(new { success = false, message = "No se puede eliminar la categoría porque tiene productos vinculados. Elimina los productos primero." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        //Muestra una vista con los datos del producto seleccionado para modificarlo
        [HttpGet]
        public ActionResult ModificarCategoria(long q)
        {
            var datos = modelCategoria.ConsultaCategoriaEspecifica(q);
            ViewBag.Categorias = modelCategoria.ConsultarCategoria();
            return View(datos);
        }


        //Actualiza el producto con los nuevos datos ingresados
        [HttpPost]
        public ActionResult ModificarCategoria(CategoriaEnt entidad)
        {

            long ID_Categoria = modelCategoria.ActualizarCategoria(entidad);

            if (ID_Categoria > 0)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "No se ha podido actualizar la categoría." });

            }
        }

        // GET: Categoria
        public ActionResult ConsultarCategorias()
        {
            return View();
        }
    }
}