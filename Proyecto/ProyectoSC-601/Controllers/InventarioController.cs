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
    public class InventarioController : Controller
    {
        
        InventarioModel modelInventario=new InventarioModel();


        /* Consulta todos los productos registrados en el sistema */
        [HttpGet]
        public ActionResult ConsultaInventario()
        {
            ViewBag.TotalInventario = modelInventario.ContarTotalInventario();
            var datos = modelInventario.ConsultarInventario();
            return View(datos);
        }

        //Devuele la vista de registrar productos con las categorias 
        [HttpGet]
        public ActionResult RegistrarProducto()
        {
            ViewBag.Categorias = modelInventario.ConsultarCategorias();
            return View();
        }

        //Registra un producto 
        [HttpPost]
        public ActionResult RegistrarProducto(HttpPostedFileBase Imagen_Nueva, InventarioEnt entidad)
        {
            ModelState.Remove("Imagen");

            if (ModelState.IsValid)
            {
                entidad.Imagen = string.Empty;
                entidad.Estado = 1;
                entidad.Imagen_Nueva = null;

                long ID_Producto = modelInventario.RegistrarProducto(entidad);

                if (ID_Producto > 0)
                {
                    string extension = Path.GetExtension(Path.GetFileName(Imagen_Nueva.FileName));
                    string ruta = AppDomain.CurrentDomain.BaseDirectory + "Images\\" + ID_Producto + extension;
                    Imagen_Nueva.SaveAs(ruta);

                    entidad.Imagen = "/Images/" + ID_Producto + extension;
                    entidad.ID_Producto = ID_Producto;

                    modelInventario.ActualizarRutaProducto(entidad);

                    return RedirectToAction("ConsultaInventario", "Inventario");
                }

                else
                {
                    ViewBag.Categorias = modelInventario.ConsultarCategorias();
                    ViewBag.Mensaje = "No se ha podido registrar el producto";
                    return View();
                }

            }

            else
            {
                ViewBag.Categorias = modelInventario.ConsultarCategorias();
                return View(entidad);
            }
           
        }

        //Cambia el estado de un producto
        [HttpGet]
        public ActionResult ActualizarEstadoProducto(long q)
        {
            var entidad = new InventarioEnt();
            entidad.ID_Producto = q;

            string respuesta = modelInventario.ActualizarEstadoProducto(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("ConsultaInventario", "Inventario");
            }
            else
            {
                ViewBag.Mensaje = "No se ha podido cambiar el estado del producto";
                return View();
            }
        }

        //Cambia el estado de un producto
        [HttpGet]
        public ActionResult EliminarProducto(long q)
        {
            var entidad = new InventarioEnt();
            entidad.ID_Producto = q;

            string respuesta = modelInventario.EliminarProducto(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("ConsultaInventario", "Inventario");
            }
            else
            {
                ViewBag.Mensaje = "No se ha podido eliminar el producto";
                return View();
            }
        }

        //Muestra una vista con los datos del producto seleccionado para modificarlo
        [HttpGet]
        public ActionResult ModificarProducto(long q)
        {
            var datos = modelInventario.ConsultaProductoEspecifico(q);
            ViewBag.Categorias = modelInventario.ConsultarCategorias();
            return View(datos);
        }


        //Actualiza el producto con los nuevos datos ingresados
        [HttpPost]
        public ActionResult ModificarProducto(HttpPostedFileBase Imagen_Nueva, InventarioEnt entidad)
        {

            ModelState.Remove("Imagen");
            ModelState.Remove("Imagen_Nueva");

            if (ModelState.IsValid)
            {
                entidad.Imagen_Nueva = null;

                if (Imagen_Nueva != null)
                {
                    string extension = Path.GetExtension(Imagen_Nueva.FileName);
                    string rutaNuevaImagen = Path.Combine(Server.MapPath("~/Images/"), entidad.ID_Producto + extension);

                    Imagen_Nueva.SaveAs(rutaNuevaImagen);

                    entidad.Imagen = "/Images/" + entidad.ID_Producto + extension;
                }

                long ID_Producto = modelInventario.ActualizarProducto(entidad);

                if (ID_Producto > 0)
                {
                    return RedirectToAction("ConsultaInventario", "Inventario");
                }
                else
                {
                    ViewBag.Mensaje = "No se ha podido actualizar el producto";
                    return View();
                }
            }
            else
                {
                    ViewBag.Categorias = modelInventario.ConsultarCategorias();
                    return View(entidad);
                }
            }

        [HttpGet]
        public ActionResult VerificarEliminarProducto(int idProducto)
        {
            var entidad = new InventarioEnt { ID_Producto = idProducto };

            bool facturasVinculadas = modelInventario.VerificarFacturasVinculadas(entidad);

            if (facturasVinculadas)
            {
                return Json(new { success = false, message = "No se puede eliminar el producto por información relacionada en las facturas" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: Categoria
        public ActionResult ConsultarCategoria()
        {
            return View();
        }


    }
}