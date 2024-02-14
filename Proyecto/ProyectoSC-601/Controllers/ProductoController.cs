using ProyectoSC_601.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ProyectoSC_601.Controllers
{
    public class ProductoController : Controller
    {
        InventarioModel modelInventario = new InventarioModel();

        [HttpGet]
        public ActionResult Catalogo(int pagina = 1, int tamanoPagina = 9)
        {
            var datos = modelInventario.ConsultarInventario();
            ViewBag.Categorias = modelInventario.ConsultarCategorias();
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

        [HttpGet]
        public ActionResult ProductoDetalle(long q)
        {
            var datos = modelInventario.ConsultaProductoEspecifico(q);
            return View(datos);
        }

    }
}