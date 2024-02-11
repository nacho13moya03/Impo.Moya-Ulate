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
    public class FacturacionController : ApiController
    {
        private readonly Errores log;
        private readonly LogExitos logExitos;

        public FacturacionController()
        {
            string rutaErrores = ConfigurationManager.AppSettings["RutaErrores"];
            string rutaExitos = ConfigurationManager.AppSettings["RutaExitos"];


            log = new Errores(rutaErrores);
            logExitos = new LogExitos(rutaExitos);
        }

        Utilitarios util = new Utilitarios();

        [HttpGet]
        [Route("ConsultaFacturasCliente")]
        public List<Factura_Encabezado> ConsultaFacturasCliente(long q)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var facturas = (from x in context.Factura_Encabezado
                                    where x.ID_Usuario == q
                                    select x).OrderByDescending(x => x.FechaCompra).ToList();

                    if (facturas != null && facturas.Any())
                    {
                        logExitos.Add("ConsultaFacturasCliente", $"Se consultaron satisfactoriamente las facturas para el cliente con ID {q}.");
                    }
                    else
                    {
                        logExitos.Add("ConsultaFacturasCliente", $"No se encontraron facturas para el cliente con ID {q}.");
                    }

                    return facturas;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultaFacturasCliente: " + ex.Message);
                return new List<Factura_Encabezado>();
            }
        }


        [HttpGet]
        [Route("ConsultaFacturasAdmin")]
        public object ConsultaFacturasAdmin()
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var facturasAdmin = (from x in context.Factura_Encabezado
                                         join y in context.Usuario on x.ID_Usuario equals y.ID_Usuario
                                         select new
                                         {
                                             x.ID_Factura,
                                             NombreCliente = y.Nombre_Usuario,
                                             ApellidoCliente = y.Apellido_Usuario,
                                             x.FechaCompra,
                                             x.TotalCompra,
                                         }).OrderByDescending(x => x.FechaCompra).ToList();

                    if (facturasAdmin != null && facturasAdmin.Any())
                    {
                        logExitos.Add("ConsultaFacturasAdmin", "Se consultaron satisfactoriamente las facturas para el administrador.");
                    }
                    else
                    {
                        logExitos.Add("ConsultaFacturasAdmin", "No se encontraron facturas para el administrador.");
                    }

                    return facturasAdmin;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultaFacturasAdmin: " + ex.Message);
                return new List<object>();
            }
        }


        [HttpGet]
        [Route("ConsultaDetalleFactura")]
        public object ConsultaDetalleFactura(long q)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var detalleFactura = (from x in context.Factura_Detalle
                                          join y in context.Producto on x.ID_Producto equals y.ID_Producto
                                          where x.ID_Factura == q
                                          select new
                                          {
                                              x.ID_Factura,
                                              y.Nombre,
                                              x.PrecioPagado,
                                              x.CantidadPagado,
                                              x.ImpuestoPagado,
                                              SubTotal = (x.PrecioPagado * x.CantidadPagado),
                                              Impuesto = (x.ImpuestoPagado * x.CantidadPagado),
                                              Total = (x.PrecioPagado * x.CantidadPagado) + (x.ImpuestoPagado * x.CantidadPagado),
                                          }).ToList();

                    if (detalleFactura != null && detalleFactura.Any())
                    {
                        logExitos.Add("ConsultaDetalleFactura", "Se consultaron satisfactoriamente los detalles de la factura.");
                    }
                    else
                    {
                        logExitos.Add("ConsultaDetalleFactura", "No se encontraron detalles de la factura.");
                    }

                    return detalleFactura;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultaDetalleFactura: " + ex.Message);
                return new List<object>();
            }
        }



        [HttpGet]
        [Route("ConsultarDatosEnviarCorreo")]
        public string ConsultarDatosEnviarCorreo(long q)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;

                    decimal factor = 1.13m;

                    var facturaData = (from x in context.Factura_Encabezado
                                       join y in context.Usuario on x.ID_Usuario equals y.ID_Usuario
                                       join z in context.Factura_Detalle on x.ID_Factura equals z.ID_Factura
                                       where x.ID_Usuario == q
                                       select new FacturaEnt
                                       {
                                           ID_Factura = x.ID_Factura,
                                           NombreCliente = y.Nombre_Usuario,
                                           ApellidoCliente = y.Apellido_Usuario,
                                           CorreoCliente = y.Correo_Usuario,
                                           FechaCompra = x.FechaCompra,
                                           TotalCompra = x.TotalCompra,
                                           SubTotal = (x.TotalCompra / factor),
                                           Impuesto = (x.TotalCompra - (x.TotalCompra / factor)),
                                       }).OrderByDescending(x => x.FechaCompra).FirstOrDefault();

                    if (facturaData != null)
                    {
                        string rutaArchivo = AppDomain.CurrentDomain.BaseDirectory + "Templates\\FacturaCorreo.html";
                        string html = File.ReadAllText(rutaArchivo);

                        string numFactura = facturaData.ID_Factura.ToString();
                        string cliente = facturaData.NombreCliente + " " + facturaData.ApellidoCliente;
                        string fecha = facturaData.FechaCompra.ToString();
                        string subtotal = facturaData.SubTotal.ToString("N2");
                        string impuesto = facturaData.Impuesto.ToString("N2");
                        string total = facturaData.TotalCompra.ToString();

                        html = html.Replace("@@Factura", numFactura);
                        html = html.Replace("@@Cliente", cliente);
                        html = html.Replace("@@Fecha", fecha);
                        html = html.Replace("@@Subtotal", subtotal);
                        html = html.Replace("@@Impuesto", impuesto);
                        html = html.Replace("@@Total", total);

                        util.EnviarCorreo(facturaData.CorreoCliente, "Factura Electrónica", html);
                        logExitos.Add("ConsultarDatosEnviarCorreo", "Se consultaron y enviaron los datos de la factura satisfactoriamente.");

                        return "OK";
                    }
                    else
                    {
                        logExitos.Add("ConsultarDatosEnviarCorreo", "No se encontraron datos de factura para enviar.");
                        return string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultarDatosEnviarCorreo: " + ex.Message);
                return "Error: " + ex.Message;
            }
        }



        [HttpGet]
        [Route("ContarVentas")]
        public int ContarVentas()
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    int cantidadVentas = context.Factura_Encabezado.Count();
                    logExitos.Add("ContarVentas", $"Se contaron satisfactoriamente {cantidadVentas} ventas.");
                    return cantidadVentas;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ContarVentas: " + ex.Message);
                return 0;
            }
        }


    }
}