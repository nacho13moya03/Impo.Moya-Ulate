using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace APIProyectoSC_601.Controllers
{

    public class CarritoController : ApiController
    {
        private readonly Errores log;
        private readonly LogExitos logExitos;


        public CarritoController()
        {
            string rutaErrores = ConfigurationManager.AppSettings["RutaErrores"];
            string rutaExitos = ConfigurationManager.AppSettings["RutaExitos"];


            log = new Errores(rutaErrores);
            logExitos = new LogExitos(rutaExitos);
        }

        [HttpPost]
        [Route("RegistrarCarrito")]
        public string RegistrarCarrito(Carrito carrito)
        {
            try
            {
                using (var context = new db_aa61bd_impomuEntities())
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


        [HttpGet]
        [Route("ConsultarCarrito")]
        public object ConsultarCarrito(long q)
        {
            try
            {
                using (var context = new db_aa61bd_impomuEntities())
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
                using (var context = new db_aa61bd_impomuEntities())
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
                using (var context = new db_aa61bd_impomuEntities())
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