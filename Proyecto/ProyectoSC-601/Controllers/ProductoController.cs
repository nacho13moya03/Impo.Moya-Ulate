using PagedList;
using ProyectoSC_601.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoSC_601.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class ProductoController : Controller
    {
        InventarioModel modelInventario = new InventarioModel();
        IndexModel modelIndex = new IndexModel();

        [HttpGet]
        public ActionResult Catalogo(int pagina = 1, int tamanoPagina = 9, string categorias = null)
        {
            if (Session["ID_Usuario"] != null && Session["Rol"] != null && long.Parse(Session["Rol"].ToString()) == 2)
            {
                // Obtiene la cantidad de productos diferentes en el carrito
                int cantidadProductos = modelIndex.ObtenerCantidadProductosEnCarrito(long.Parse(Session["ID_Usuario"].ToString()));

                // Pasa la cantidad de productos a la vista
                ViewBag.CantidadProductosEnCarrito = cantidadProductos;
            }
            // Consultar productos
            var datos = modelInventario.ConsultarInventario();
            // Obtener info categorías
            ViewBag.Categorias = modelInventario.ConsultarCategorias();
            // Convertir string de categorías a lista
            List<int> idsCategorias = null;
            if (!String.IsNullOrEmpty(categorias))
            {
                idsCategorias = categorias.Split(',')
                                         .Where(x => !string.IsNullOrWhiteSpace(x)) // Filtrar cadenas vacías o nulas
                                         .Select(x => int.Parse(x))
                                         .ToList();
            }

            // Aplicar filtros de búsqueda si hay
            if (idsCategorias != null && idsCategorias.Count > 0)
            {
                datos = datos.Where(p => idsCategorias.Contains(p.ID_Categoria))
                             .ToList();

                // Categorías seleccionadas para la vista  
                ViewBag.CategoriasSeleccionadas = idsCategorias;
            }
            // Paginar datos filtrados
            var productosPaginados = datos.ToPagedList(pagina, tamanoPagina);
            return View(productosPaginados);
        }


        [HttpGet]
        public ActionResult CatalogoMujer(int pagina = 1, int tamanoPagina = 9)
        {
            int categoria = 2;
            var datos = modelInventario.ConsultarInventarioCatalogo(categoria);
            var productosPaginados = datos.ToPagedList(pagina, tamanoPagina);
            return View(productosPaginados);
        }

        [HttpGet]
        public ActionResult CatalogoHombre(int pagina = 1, int tamanoPagina = 9)
        {
            int categoria = 1;
            var datos = modelInventario.ConsultarInventarioCatalogo(categoria);
            var productosPaginados = datos.ToPagedList(pagina, tamanoPagina);
            return View(productosPaginados);
        }

        [HttpGet]
        public ActionResult CatalogoNinos(int pagina = 1, int tamanoPagina = 9)
        {
            int categoria = 3;
            var datos = modelInventario.ConsultarInventarioCatalogo(categoria);
            var productosPaginados = datos.ToPagedList(pagina, tamanoPagina);
            return View(productosPaginados);
        }


    }
}