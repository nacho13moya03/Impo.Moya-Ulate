using ProyectoSC_601.Entities;
using ProyectoSC_601.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace ProyectoSC_601.Controllers
{
    public class CategoriaController : Controller
    {
        
        CategoriaModel modelCategoria=new CategoriaModel();


        /* Consulta todas las categorias registrados en el sistema */
        [HttpGet]
        public ActionResult ConsultarCategoria()
        {
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
        public ActionResult RegistrarCategoria(CategoriaEnt entidad)
        {
            entidad.Estado_Categoria  = 1;

            string ID_Categoria = modelCategoria.RegistrarCategoria(entidad);

            if (ID_Categoria == "OK")
            {

                //entidad.ID_Categoria = ID_Categoria;


                return RedirectToAction("ConsultarCategoria", "Categoria");
            }
            else
            {
                ViewBag.Mensaje = "No se ha podido registrar la categoria";
                return View();
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

        //Eliminar un producto 
        [HttpGet]
        public ActionResult EliminarCategoria(int q)
        {
            var entidad = new CategoriaEnt();
            entidad.ID_Categoria = q;

            string respuesta = modelCategoria.EliminarCategoria(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("ConsultarCategoria", "Categoria");
            }
            else
            {
                ViewBag.Mensaje = "No se ha podido eliminar la categoria";
                return View();
            }
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
                return RedirectToAction("ConsultarCategoria", "Categoria");
            }
            else
            {
                ViewBag.Mensaje = "No se ha podido actualizar la categoria";
                return View();
            }
        }

        // GET: Categoria
        public ActionResult ConsultarCategorias()
        {
            return View();
        }
    }
}