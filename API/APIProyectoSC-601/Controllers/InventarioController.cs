using APIProyectoSC_601.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIProyectoSC_601.Controllers
{
    public class InventarioController : ApiController
    {
        private readonly Errores log;

        public InventarioController()
        {
            string rutaDeLogs = ConfigurationManager.AppSettings["RutaDeLogs"];
            log = new Errores(rutaDeLogs);
        }

        //Devuelve una lista con todos los productos registrados en la base de datos

        [HttpGet]
        [Route("ConsultarInventario")]
        public List<Producto> ConsultarInventario()
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return context.Producto.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultarInventario: " + ex.Message);
                return new List<Producto>();
            }
           
        }

        //Devuelve una lista con las categorias que existen para los productos
        [HttpGet]
        [Route("ConsultarCategorias")]
        public List<System.Web.Mvc.SelectListItem> ConsultarCategorias()
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var datos = (from x in context.Categorias
                                 select x).ToList();

                    List<System.Web.Mvc.SelectListItem> categorias = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        categorias.Add(new System.Web.Mvc.SelectListItem
                        {
                            Value = item.ID_Categoria.ToString(),
                            Text = item.Nombre
                        });
                    }

                    return categorias;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultarCategorias: " + ex.Message);
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

        //Conexion a procedimiento para registrar categorias
        [HttpPost]
        [Route("RegistrarCategoria")]
        public long RegistrarCategoria(Producto categoria)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Producto.Add(categoria);
                    context.SaveChanges();
                    return categoria.ID_Categoria;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en RegistrarCategoria: " + ex.Message);
                return -1;
            }
        }


        //Elimina la categoria en la base de datos
        [HttpPut]
        [Route("EliminarCategoria")]
        public string EliminarCategoria(Producto categoria)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var categoriaAEliminar = context.Producto.Find(categoria.ID_Producto);

                    if (categoriaAEliminar != null)
                    {
                        context.Producto.Remove(categoriaAEliminar);
                        context.SaveChanges();
                        return "OK";
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en EliminarCategoria: " + ex.Message);
                return "Error: " + ex.Message;
            }
        }


        //Conexion a procedimiento para registrar productos
        [HttpPost]
        [Route("RegistrarProducto")]
        public long RegistrarProducto(Producto producto)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Producto.Add(producto);
                    context.SaveChanges();
                    return producto.ID_Producto;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en RegistrarProducto: " + ex.Message);
                return -1;
            }
        }


        //Actualiza la ruta de la imagen del producto en la base de datos
        [HttpPut]
        [Route("ActualizarRutaProducto")]
        public string ActualizarRutaProducto(Producto producto)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var datos = context.Producto.Where(x => x.ID_Producto == producto.ID_Producto).FirstOrDefault();

                    if (datos != null)
                    {
                        datos.Imagen = producto.Imagen;
                        context.SaveChanges();
                        return "OK";
                    }
                    else
                    {
                        return "Producto no encontrado";
                    }
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ActualizarRutaProducto: " + ex.Message);
                return "Error: " + ex.Message;
            }
        }


        //Actualiza el estado del producto en la base de datos
        [HttpPut]
        [Route("ActualizarEstadoProducto")]
        public string ActualizarEstadoProducto(Producto producto)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var datos = context.Producto.Where(x => x.ID_Producto == producto.ID_Producto).FirstOrDefault();

                    if (datos != null)
                    {
                        if (datos.Estado == 1)
                        {
                            datos.Estado = 0;
                        }
                        else
                        {
                            datos.Estado = 1;
                        }
                        context.SaveChanges();
                        return "OK";
                    }
                    else
                    {
                        // Puedes manejar el caso en que no se encuentre el producto.
                        return "Producto no encontrado";
                    }
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ActualizarEstadoProducto: " + ex.Message);
                return "Error: " + ex.Message;
            }
        }


        //Elimina el producto en la base de datos
        [HttpPut]
        [Route("EliminarProducto")]
        public string EliminarProducto(Producto producto)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var productoAEliminar = context.Producto.Find(producto.ID_Producto);

                    if (productoAEliminar != null)
                    {
                        context.Producto.Remove(productoAEliminar);
                        context.SaveChanges();
                        return "OK";
                    }
                    else
                    {
                        return "Producto no encontrado";
                    }
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en EliminarProducto: " + ex.Message);
                return "Error: " + ex.Message;
            }
        }


        //Devuelve los datos de un producto segun su ID
        [HttpGet]
        [Route("ConsultaProductoEspecifico")]
        public Producto ConsultaProductoEspecifico(long q)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.Producto
                            where x.ID_Producto == q
                            select x).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultaProductoEspecifico: " + ex.Message);
                return null;
            }
        }


        //Actualiza los datos del producto en la base de datos
        [HttpPut]
        [Route("ActualizarProducto")]
        public long ActualizarProducto(Producto producto)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var datos = context.Producto.Where(x => x.ID_Producto == producto.ID_Producto).FirstOrDefault();

                    if (datos != null)
                    {
                        if (!string.IsNullOrEmpty(producto.Imagen))
                        {
                            datos.Imagen = producto.Imagen;
                        }
                        datos.ID_Categoria = producto.ID_Categoria;
                        datos.Nombre = producto.Nombre;
                        datos.Descripcion = producto.Descripcion;
                        datos.Cantidad = producto.Cantidad;
                        datos.Precio = producto.Precio;

                        context.SaveChanges();

                        return producto.ID_Producto;
                    }
                    else
                    {
                        return -1; 
                    }
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ActualizarProducto: " + ex.Message);
                return -1;
            }
        }


        //Devuelve la cantidad total de los recursos del inventario
        [HttpGet]
        [Route("TotalInventario")]
        public decimal TotalInventario()
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return context.Producto.Sum(x => x.Precio * x.Cantidad);
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en TotalInventario: " + ex.Message);
                return -1;
            }
        }

    }
}
