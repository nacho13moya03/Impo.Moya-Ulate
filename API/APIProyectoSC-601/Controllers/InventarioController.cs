using ProyectoSC_601.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace APIProyectoSC_601.Controllers
{
    public class InventarioController : ApiController
    {
        private readonly Errores log;
        private readonly LogExitos logExitos;

        public InventarioController()
        {
            string rutaErrores = ConfigurationManager.AppSettings["RutaErrores"];
            string rutaExitos = ConfigurationManager.AppSettings["RutaExitos"];


            log = new Errores(rutaErrores);
            logExitos = new LogExitos(rutaExitos);
        }

        //Devuelve una lista con todos los productos registrados en la base de datos

        [HttpGet]
        [Route("ConsultarInventario")]
        public List<ProductoEnt> ConsultarInventario()
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;

                    var producto = (from i in context.Producto
                                    join c in context.Categorias on i.ID_Categoria equals c.ID_Categoria into categoriaJoin
                                    from cat in categoriaJoin.DefaultIfEmpty()
                                    select new ProductoEnt
                                    {
                                        ID_Producto = i.ID_Producto,
                                        ID_Categoria = i.ID_Categoria,
                                        Nombre_Categoria = cat != null ? cat.Nombre_Categoria : "",
                                        Nombre = i.Nombre,
                                        Descripcion = i.Descripcion,
                                        Cantidad = i.Cantidad,
                                        Precio = i.Precio,
                                        SKU = i.SKU,
                                        Imagen = i.Imagen,
                                        Estado = i.Estado
                                    }).ToList();
                    logExitos.Add("ConsultarInventario", "Se consultó satisfactoriamente el inventario de productos.");
                    return producto;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultarInventario: " + ex.Message);
                return new List<ProductoEnt>();
            }

        }

        [HttpGet]
        [Route("ConsultarInventarioCatalogo")]
        public List<ProductoEnt> ConsultarInventarioCatalogo(int categoria)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;

                    var producto = (from i in context.Producto
                                    join c in context.Categorias on i.ID_Categoria equals c.ID_Categoria into categoriaJoin
                                    from cat in categoriaJoin.DefaultIfEmpty()
                                    where i.ID_Categoria == categoria
                                    select new ProductoEnt
                                    {
                                        ID_Producto = i.ID_Producto,
                                        ID_Categoria = i.ID_Categoria,
                                        Nombre_Categoria = cat != null ? cat.Nombre_Categoria : "",
                                        Nombre = i.Nombre,
                                        Descripcion = i.Descripcion,
                                        Cantidad = i.Cantidad,
                                        Precio = i.Precio,
                                        SKU = i.SKU,
                                        Imagen = i.Imagen,
                                        Estado = i.Estado
                                    }).ToList();
                    logExitos.Add("ConsultarInventarioCatalogo", "Se consultó satisfactoriamente el inventario de productos del catálogo específico.");
                    return producto;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultarInventarioCatalogo: " + ex.Message);
                return new List<ProductoEnt>();
            }

        }

        //Verifica si el SKU ya existe
        [HttpPost]
        [Route("ComprobarSKUExistente")]
        public string ComprobarSKUExistente(ProductoEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;

                    // Verificar si la cedula existe antes de ejecutar la consulta
                    bool SKUExiste = context.Producto.Any(x => x.SKU == entidad.SKU && x.ID_Producto != entidad.ID_Producto);

                    if (SKUExiste)
                    {
                        // Devuelve que ya existe la cedula
                        logExitos.Add("ComprobarSKUExistente", $"El SKU {entidad.SKU} ya existe.");
                        return "Existe";
                    }
                    else
                    {
                        logExitos.Add("ComprobarSKUExistente", $"El SKU {entidad.SKU} no existe.");
                        return "NoExiste";
                    }

                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ComprobarSKUExistente: " + ex.Message);
                return null;
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
                    var datos = (from x in context.Categorias where x.Estado_Categoria == 1
                                 select x).ToList();

                    List<System.Web.Mvc.SelectListItem> categorias = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        categorias.Add(new System.Web.Mvc.SelectListItem

                        {

                            Value = item.ID_Categoria.ToString(),
                            Text = item.Nombre_Categoria,


                        });
                    }
                    logExitos.Add("ConsultarCategorias", "Se consultaron satisfactoriamente las categorías de productos.");
                    return categorias;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultarCategorias: " + ex.Message);
                return new List<System.Web.Mvc.SelectListItem>();
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
                    logExitos.Add("RegistrarProducto", $"Se registró satisfactoriamente el producto con ID {producto.ID_Producto}.");
                    return producto.ID_Producto;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en RegistrarProducto: " + ex.Message);
                return 0;
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
                        logExitos.Add("ActualizarRutaProducto", $"Se actualizó la ruta de la imagen del producto con ID {producto.ID_Producto}.");
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
                        logExitos.Add("ActualizarEstadoProducto", $"Se actualizó el estado del producto con ID {producto.ID_Producto}.");
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
                        logExitos.Add("EliminarProducto", $"Se eliminó el producto con ID {producto.ID_Producto}.");
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
                    var producto = (from x in context.Producto
                                    where x.ID_Producto == q
                                    select x).FirstOrDefault();

                    if (producto != null)
                    {
                        logExitos.Add("ConsultaProductoEspecifico", $"Se consultaron los datos del producto con ID {q}.");
                    }

                    return producto;
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
                        datos.SKU = producto.SKU;
                        datos.Cantidad = producto.Cantidad;
                        datos.Precio = producto.Precio;

                        context.SaveChanges();
                        logExitos.Add("ActualizarProducto", $"Se actualizó exitosamente el producto con ID {producto.ID_Producto}.");
                        return producto.ID_Producto;
                    }
                    else
                    {
                        log.Add("Error en ActualizarProducto: Producto no encontrado.");
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
        public string TotalInventario()
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    decimal totalInventario = context.Producto.Sum(x => x.Precio * x.Cantidad);
                    string totalFormateado = totalInventario.ToString("N");
                    logExitos.Add("TotalInventario", $"La cantidad total de los recursos del inventario es {totalInventario:C2}.");
                    return totalFormateado;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en TotalInventario: " + ex.Message);
                return string.Empty;
            }
        }
        [HttpPost]
        [Route("VerificarFacturasVinculadas")]
        public bool VerificarFacturasVinculadas(ProductoEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    bool facturasVinculadas = context.Factura_Detalle.Any(p => p.ID_Producto == entidad.ID_Producto);

                    if (!facturasVinculadas)
                    {
                        logExitos.Add("VerificarFacturasVinculadas", $"No hay facturas vinculadas al producto con ID {entidad.ID_Producto}.");
                    }

                    return facturasVinculadas;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en VerificarFacturasVinculadas: " + ex.Message);
                return false;

            }
        }
        [HttpPost]
        [Route("VerificarCarritoVinculado")]
        public bool VerificarCarritoVinculado(ProductoEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    bool carritoVinculado = context.Carrito.Any(p => p.ID_Producto == entidad.ID_Producto);

                    if (!carritoVinculado)
                    {
                        logExitos.Add("VerificarCarritoVinculado", $"No hay carrito vinculado al producto con ID {entidad.ID_Producto}.");
                    }

                    return carritoVinculado;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en VerificarCarritoVinculado: " + ex.Message);
                return false;

            }
        }

    }
}
