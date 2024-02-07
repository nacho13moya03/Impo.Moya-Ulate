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
    public class CarritoController : ApiController
    {
        private readonly Errores log;

        public CarritoController()
        {
            string rutaDeLogs = ConfigurationManager.AppSettings["RutaDeLogs"];
            log = new Errores(rutaDeLogs);
        }

        [HttpPost]
        [Route("RegistrarCarrito")]
        public string RegistrarCarrito(Carrito carrito)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var datos = (from x in context.Carrito
                                 where x.ID_Usuario == carrito.ID_Usuario
                                    && x.ID_Producto == carrito.ID_Producto
                                 select x).FirstOrDefault();

                    if (datos == null)
                    {
                        context.Carrito.Add(carrito);
                        context.SaveChanges();
                    }
                    else
                    {
                        datos.Cantidad = carrito.Cantidad;
                        context.SaveChanges();
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
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.Carrito
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
                                SubTotal = (y.Precio * x.Cantidad),
                                Impuesto = (y.Precio * x.Cantidad) * 0.13M,
                                Total = (y.Precio * x.Cantidad) + (y.Precio * x.Cantidad) * 0.13M
                            }).ToList();
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
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var datos = (from x in context.Carrito
                                 where x.ID_Carrito == q
                                 select x).FirstOrDefault();

                    if (datos != null)
                    {
                        context.Carrito.Remove(datos);
                        context.SaveChanges();
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
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    return context.PagarCarritoSP(carrito.ID_Usuario).FirstOrDefault();
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