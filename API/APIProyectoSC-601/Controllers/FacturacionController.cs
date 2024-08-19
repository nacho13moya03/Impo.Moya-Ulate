using APIProyectoSC_601.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace APIProyectoSC_601.Controllers
{
    public class FacturacionController : ApiController
    {
        private readonly Errores log;
        private readonly LogExitos logExitos;

        public string RutaErrores { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["RutaErrores"];
        public string RutaExitos { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["RutaExitos"];

        public FacturacionController()
        {
            string rutaErrores = RutaErrores;
            string rutaExitos = RutaExitos;


            log = new Errores(rutaErrores);
            logExitos = new LogExitos(rutaExitos);
        }

        Utilitarios util = new Utilitarios();

        [HttpGet]
        [Route("ConsultarFacturaRealizada")]
        public long ConsultarFacturaRealizada(long q)
        {
            try
            {
                using (var context = new db_aa7345_impomucrEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;

                    var ultimaFactura = (from x in context.Factura_Encabezado
                                         where x.ID_Usuario == q
                                         orderby x.FechaCompra descending
                                         select x.ID_Factura).FirstOrDefault();

                    if (ultimaFactura != 0)
                    {
                        logExitos.Add("ConsultaUltimaFacturasCliente", $"Se consultó satisfactoriamente la última factura ({ultimaFactura}) para el cliente con ID {q}.");
                    }
                    else
                    {
                        logExitos.Add("ConsultaUltimaFacturasCliente", $"No se encontró factura para el cliente con ID {q}.");
                    }

                    return ultimaFactura;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultaUltimaFacturasCliente: " + ex.Message);
                return 0;
            }
        }



        [HttpGet]
        [Route("ConsultaFacturasCliente")]
        public List<Factura_Encabezado> ConsultaFacturasCliente(long q)
        {
            try
            {
                using (var context = new db_aa7345_impomucrEntities())
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
                using (var context = new db_aa7345_impomucrEntities())
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
                using (var context = new db_aa7345_impomucrEntities())
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
                using (var context = new db_aa7345_impomucrEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;

                    decimal factor = 1.13m;

                    var facturaData = (from x in context.Factura_Encabezado
                                       join y in context.Usuario on x.ID_Usuario equals y.ID_Usuario
                                       join z in context.Factura_Detalle on x.ID_Factura equals z.ID_Factura
                                       join p in context.Producto on z.ID_Producto equals p.ID_Producto
                                       where x.ID_Usuario == q
                                       group new { p.Nombre, p.Precio, z.CantidadPagado } by new { x.ID_Factura, y.Nombre_Usuario, y.Apellido_Usuario, y.Correo_Usuario, x.FechaCompra, x.TotalCompra } into g
                                       select new FacturaEnt
                                       {
                                           ID_Factura = g.Key.ID_Factura,
                                           NombreCliente = g.Key.Nombre_Usuario,
                                           ApellidoCliente = g.Key.Apellido_Usuario,
                                           CorreoCliente = g.Key.Correo_Usuario,
                                           FechaCompra = g.Key.FechaCompra,
                                           TotalCompra = g.Key.TotalCompra,
                                           PrecioPagado = g.Sum(item => item.Precio * item.CantidadPagado),
                                           precio = g.Select(item => item.Precio * item.CantidadPagado).ToList(),
                                           SubTotal = (g.Key.TotalCompra / factor),
                                           Impuesto = (g.Key.TotalCompra - (g.Key.TotalCompra / factor)),
                                           NombreProducto = g.Select(item => item.Nombre).ToList(),
                                           Cantidad = g.Select(item => item.CantidadPagado).ToList()
                                       }).OrderByDescending(x => x.FechaCompra).FirstOrDefault();



                    if (facturaData != null)
                    {
                        string rutaArchivo = AppDomain.CurrentDomain.BaseDirectory + "Templates\\FacturaCorreo.html";
                        string html = File.ReadAllText(rutaArchivo);

                        string numFactura = facturaData.ID_Factura.ToString();
                        string cliente = facturaData.NombreCliente + " " + facturaData.ApellidoCliente;
                        string fecha = facturaData.FechaCompra.ToString();
                        string nombre = facturaData.Nombre;
                        string cantidad = facturaData.Cantidad.ToString();
                        string subtotal = facturaData.SubTotal.ToString("N2");
                        string impuesto = facturaData.Impuesto.ToString("N2");
                        string total = facturaData.TotalCompra.ToString("N2");

                        List<string> productosCantidades = new List<string>();
                        for (int i = 0; i < facturaData.NombreProducto.Count; i++)
                        {
                            string productoCantidad = $"<tr><td>{facturaData.NombreProducto[i]}</td><td>{facturaData.Cantidad[i]}</td><td>{facturaData.precio[i].ToString("N2")}</td></tr>";
                            productosCantidades.Add(productoCantidad);
                        }

                        html = html.Replace("@@Factura", numFactura);
                        html = html.Replace("@@Cliente", cliente);
                        html = html.Replace("@@Fecha", fecha);
                        html = html.Replace("@@Producto", string.Join("", productosCantidades));
                        html = html.Replace("@@Cantidad", "Cantidad");
                        html = html.Replace("@@Precio", "Precio");
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
                using (var context = new db_aa7345_impomucrEntities())
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