using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using WebGrease.Activities;

namespace APIProyectoSC_601.Controllers
{

    public class CarritoController : ApiController
    {
        private readonly Errores log;
        private readonly LogExitos logExitos;

        public string RutaErrores { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["RutaErrores"];
        public string RutaExitos { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["RutaExitos"];


        public CarritoController()
        {

            string rutaErrores = RutaErrores;
            string rutaExitos = RutaExitos;


            log = new Errores(rutaErrores);
            logExitos = new LogExitos(rutaExitos);
        }

        [HttpGet]
        [Route("ObtenerCantidadDisponible")]
        public int ObtenerCantidadDisponible(int idProducto)
        {
            try
            {
                using (var context = new db_aa7345_impomucrEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    int cantidad = context.Producto.Where(x => x.ID_Producto == idProducto)
                                                 .Select(x => x.Cantidad)
                                                 .FirstOrDefault();
                    logExitos.Add("ObtenerCantidadDisponible", $"Se obtiene la cantidad disponible del producto");

                    return cantidad;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ObtenerCantidadDisponible: " + ex.Message);
                return 0;
            }
        }

        [HttpGet]
        [Route("ObtenerCantidadProductosEnCarrito")]
        public int ObtenerCantidadProductosEnCarrito(long ID_Usuario)
        {
            try
            {
                using (var context = new db_aa7345_impomucrEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    int cantidadCarrito = context.Carrito.Where(x => x.ID_Usuario == ID_Usuario)
                                                 .Select(x => x.ID_Producto)
                                                 .Distinct()
                                                 .Count();


                    logExitos.Add("ObtenerCantidadProductosEnCarrito", $"Se contaron {cantidadCarrito} productos diferentes en el carrito");

                    return cantidadCarrito;

                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ObtenerCantidadProductosEnCarrito: " + ex.Message);
                return 0;
            }
        }

        [HttpPost]
        [Route("RegistrarCarrito")]
        public string RegistrarCarrito(Carrito carrito)
        {
            try
            {
                using (var context = new db_aa7345_impomucrEntities())
                {
                    var datos = (from x in context.Carrito
                                 where x.ID_Usuario == carrito.ID_Usuario
                                    && x.ID_Producto == carrito.ID_Producto
                                 select x).FirstOrDefault();

                    if (datos == null)
                    {
                        context.Carrito.Add(carrito);
                        context.SaveChanges();
                        logExitos.Add("RegistrarCarrito", "Registro en el carrito realizado exitosamente");
                    }
                    else
                    {
                        datos.Cantidad = carrito.Cantidad;
                        context.SaveChanges();
                        logExitos.Add("RegistrarCarrito", "Actualización en el carrito realizada exitosamente");
                    }
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en RegistrarCarrito: " + ex.Message);
                return "Error: " + ex.Message;
            }
        }

        [HttpPut]
        [Route("ActualizarCarrito")]
        public string ActualizarCarrito(Carrito carrito)
        {
            try
            {
                using (var context = new db_aa7345_impomucrEntities())
                {
                    var datos = (from x in context.Carrito
                                 where x.ID_Usuario == carrito.ID_Usuario
                                    && x.ID_Producto == carrito.ID_Producto
                                 select x).FirstOrDefault();

                    if (datos != null)
                    {
                        datos.Cantidad = carrito.Cantidad;
                        context.SaveChanges();
                        logExitos.Add("ActualizarCarrito", "Actualización en el carrito realizado exitosamente");
                    }
                    else
                    {             
                        log.Add(" Fallo en la actualización del carrito realizada");
                        return string.Empty;
                    }
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ActualizarCarrito: " + ex.Message);
                return "Error: " + ex.Message;
            }
        }


        [HttpGet]
        [Route("ConsultarCarrito")]
        public object ConsultarCarrito(long q)
        {
            try
            {
                using (var context = new db_aa7345_impomucrEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var result = (from x in context.Carrito
                                  join y in context.Producto on x.ID_Producto equals y.ID_Producto
                                  where x.ID_Usuario == q
                                  select new
                                  {
                                      x.ID_Carrito,
                                      x.ID_Usuario,
                                      x.ID_Producto,
                                      x.Cantidad,
                                      x.FechaCarrito,
                                      y.Nombre,
                                      y.Precio,
                                      y.Imagen,
                                      SubTotal = (y.Precio * x.Cantidad),
                                      Impuesto = (y.Precio * x.Cantidad) * 0.13M,
                                      Total = (y.Precio * x.Cantidad) + (y.Precio * x.Cantidad) * 0.13M
                                  }).ToList();

                    logExitos.Add("ConsultarCarrito", $"Consulta de carrito para el usuario {q} realizada exitosamente");

                    return result;
                }

            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultarCarrito: " + ex.Message);
                return "Error: " + ex.Message;
            }
        }


        [HttpDelete]
        [Route("EliminarRegistroCarrito")]
        public void EliminarRegistroCarrito(long q)
        {
            try
            {
                using (var context = new db_aa7345_impomucrEntities())
                {
                    var datos = (from x in context.Carrito
                                 where x.ID_Carrito == q
                                 select x).FirstOrDefault();

                    if (datos != null)
                    {
                        context.Carrito.Remove(datos);
                        context.SaveChanges();
                        logExitos.Add("EliminarRegistroCarrito", $"Registro del carrito con ID {q} eliminado exitosamente");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en EliminarRegistroCarrito: " + ex.Message);
            }
        }


        [HttpPost]
        [Route("PagarCarrito")]
        public string PagarCarrito(Carrito carrito)
        {
            try
            {
                using (var context = new db_aa7345_impomucrEntities())
                {
                    string resultadoPago = context.PagarCarritoSP(carrito.ID_Usuario).FirstOrDefault();

                    logExitos.Add("PagarCarrito", $"Pago del carrito para el usuario con ID {carrito.ID_Usuario} realizado exitosamente");

                    return resultadoPago;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en PagarCarrito: " + ex.Message);
                return "Error: " + ex.Message;
            }
        }

    }
}