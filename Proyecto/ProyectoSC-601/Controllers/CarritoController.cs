using Newtonsoft.Json;
using ProyectoSC_601.Entities;
using ProyectoSC_601.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
/*PayPal*/
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WEB_ImpoMoyaUlate.Filters;
using static ProyectoSC_601.Models.CarritoModel;
/*PayPal*/

namespace ProyectoSC_601.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class CarritoController : Controller
    {


        CarritoModel modelCarrito = new CarritoModel();
        PedidosModel modelPedidos = new PedidosModel();
        UsuarioModel modelUsuario = new UsuarioModel();
        FacturacionModel modelFacturacion = new FacturacionModel();
        InventarioModel modelInventario = new InventarioModel();
        IndexModel modelIndex = new IndexModel();
        
        public string PayPalUserName { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["PayPalUserName"];
        public string PayPalPassword { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["PayPalPassword"];




        [AuthorizeCliente(2)]
        private string ObtenerImagenProducto(long ID_Producto)
        {
            var producto = modelInventario.ConsultaProductoEspecifico(ID_Producto);

            if (producto != null)
            {
                // Verificar si hay una imagen disponible
                if (!string.IsNullOrEmpty(producto.Imagen))
                {
                    string extension = Path.GetExtension(producto.Imagen);

                    if (!string.IsNullOrEmpty(extension) && (extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                                              extension.Equals(".png", StringComparison.OrdinalIgnoreCase) ||
                                                              extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase)))
                    {
                        return producto.Imagen;
                    }
                }
            }

            return "nombre_del_archivo_imagen.jpg";
        }



        [AuthorizeCliente(2)]
        [HttpGet]
        public ActionResult RegistrarCarrito(int cantidad, long ID_Producto)
        {
            var entidad = new CarritoEnt();
            entidad.ID_Usuario = long.Parse(Session["ID_Usuario"].ToString());
            entidad.ID_Producto = ID_Producto;
            entidad.Cantidad = cantidad;
            entidad.FechaCarrito = DateTime.Now;
            entidad.Imagen = ObtenerImagenProducto(ID_Producto);

            modelCarrito.RegistrarCarrito(entidad);

            var datos = modelCarrito.ConsultarCarrito(long.Parse(Session["ID_Usuario"].ToString()));
            Session["Cant"] = datos.AsEnumerable().Sum(x => x.Cantidad);
            Session["SubT"] = datos.AsEnumerable().Sum(x => x.SubTotal);
            Session["Img"] = entidad.Imagen;

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [AuthorizeCliente(2)]
        [HttpPost]
        public ActionResult ActualizarCarrito(long idCarrito, long idProducto, int nuevaCantidad)
        {
            var entidad = new CarritoEnt();
            entidad.ID_Carrito = idCarrito;
            entidad.ID_Usuario = long.Parse(Session["ID_Usuario"].ToString());
            entidad.ID_Producto = idProducto;
            entidad.Cantidad = nuevaCantidad;

            modelCarrito.ActualizarCarrito(entidad);

            entidad.Imagen = ObtenerImagenProducto(idProducto);

            var datos = modelCarrito.ConsultarCarrito(long.Parse(Session["ID_Usuario"].ToString()));
            Session["Cant"] = datos.AsEnumerable().Sum(x => x.Cantidad);
            Session["SubT"] = datos.AsEnumerable().Sum(x => x.SubTotal);
            Session["Img"] = entidad.Imagen;

            return Json("OK", JsonRequestBehavior.AllowGet);
        }


        [AuthorizeCliente(2)]
        [HttpGet]
        public ActionResult Carrito()
        {
            if (Session["ID_Usuario"] != null && Session["Rol"] != null && long.Parse(Session["Rol"].ToString()) == 2)
            {
                // Obtiene la cantidad de productos diferentes en el carrito
                int cantidadProductos = modelIndex.ObtenerCantidadProductosEnCarrito(long.Parse(Session["ID_Usuario"].ToString()));

                // Pasa la cantidad de productos a la vista
                ViewBag.CantidadProductosEnCarrito = cantidadProductos;
            }
            var datos = modelCarrito.ConsultarCarrito(long.Parse(Session["ID_Usuario"].ToString()));
            Session["SumaSubTotal"] = datos.AsEnumerable().Sum(x => x.SubTotal);
            Session["TotalPago"] = datos.AsEnumerable().Sum(x => x.Total);
            return View(datos);

        }


        [AuthorizeCliente(2)]
        [HttpGet]
        public ActionResult MetodoPago()
        {
            return View();
        }


        [AuthorizeCliente(2)]
        [HttpGet]
        public ActionResult EliminarRegistroCarrito(long q)
        {
            modelCarrito.EliminarRegistroCarrito(q);

            var datos = modelCarrito.ConsultarCarrito(long.Parse(Session["ID_Usuario"].ToString()));
            Session["Cant"] = datos.AsEnumerable().Sum(x => x.Cantidad);
            Session["SubT"] = datos.AsEnumerable().Sum(x => x.SubTotal);
            return RedirectToAction("Carrito", "Carrito");
        }

        [AuthorizeCliente(2)]
        [HttpGet]
        public JsonResult ObtenerCantidadDisponible(int idProducto)
        {
            // Aquí obtienes la cantidad disponible del producto desde tu base de datos u otra fuente de datos
            // Supongamos que obtienes la cantidad disponible del producto con id igual a idProducto
            int cantidadDisponible = modelCarrito.ObtenerCantidadDisponibleProd(idProducto);
            return Json(cantidadDisponible, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeCliente(2)]
        [HttpPost]
        public ActionResult PagarCarrito()
        {
            var entidad = new CarritoEnt();
            entidad.ID_Usuario = long.Parse(Session["ID_Usuario"].ToString());

            var respuesta = modelCarrito.PagarCarrito(entidad);
            var datos = modelCarrito.ConsultarCarrito(long.Parse(Session["ID_Usuario"].ToString()));
            Session["Cant"] = datos.AsEnumerable().Sum(x => x.Cantidad);
            Session["SubT"] = datos.AsEnumerable().Sum(x => x.SubTotal);
            Session["Img"] = entidad.Imagen;



            if (respuesta == "TRUE")
            {
                var datosCorreo = modelFacturacion.ConsultarDatosEnviarCorreo(long.Parse(Session["ID_Usuario"].ToString()));


                if (datosCorreo == "OK")
                {
                    return RedirectToAction("MetodoPago", "Carrito");
                }
                else
                {
                    ViewBag.MensajeUsuario = "No se ha podido enviar la factura electrónica al correo";
                    return View("Carrito", datos);
                }

            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido procesar su pago, verifique las unidades disponibles";
                return View("Carrito", datos);
            }
        }

        //De aqui para abajo implementacion de PayPal

        [AuthorizeCliente(2)]
        [HttpPost]
        public async Task<JsonResult> Paypal(string precio, string producto)
        {
            bool status = false;
            string respuesta = string.Empty;

            using (var client = new HttpClient())
            {
                var userName = PayPalUserName;
                var passwd = PayPalPassword;

                client.BaseAddress = new Uri("https://api-m.paypal.com");

                var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                // Obtener el tipo de cambio del dólar de Costa Rica
                var tdcResponse = await client.GetAsync("https://apis.gometa.org/tdc/tdc.json");

                if (tdcResponse.IsSuccessStatusCode)
                {
                    var tdcJson = await tdcResponse.Content.ReadAsStringAsync();
                    var tdcData = JsonConvert.DeserializeObject<dynamic>(tdcJson);

                    // Obtener el valor de venta del dólar de Costa Rica
                    var tipoCambioVenta = Convert.ToDecimal(tdcData.venta);

                    // Calcular el valor total usando el tipo de cambio obtenido
                    var datos = modelCarrito.ConsultarCarrito(long.Parse(Session["ID_Usuario"].ToString()));
                    var total = datos.AsEnumerable().Sum(x => x.Total);
                    var valortotal = Math.Round(total / tipoCambioVenta, 2).ToString();

                    // Crear la orden de PayPal con el valor calculado
                    var orden = new PaypalOrder()
                    {
                        intent = "CAPTURE",
                        purchase_units = new List<Models.CarritoModel.PurchaseUnit>() {
                    new Models.CarritoModel.PurchaseUnit() {
                        amount = new Models.CarritoModel.Amount() {
                            currency_code = "USD",
                            value = valortotal
                        },
                        description = producto
                    }
                },
                        application_context = new ApplicationContext()
                        {
                            brand_name = "Importadora Moya y Ulate SA",
                            landing_page = "NO_PREFERENCE",
                            user_action = "PAY_NOW",
                            return_url = "https://www.impomucr.com/Carrito/CheckPago",
                            cancel_url = "https://www.impomucr.com/carrito/carrito"
                        }
                    };

                    var json = JsonConvert.SerializeObject(orden);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    // Realizar la solicitud a la API de PayPal
                    HttpResponseMessage response = await client.PostAsync("/v2/checkout/orders", data);

                    status = response.IsSuccessStatusCode;

                    if (status)
                    {
                        respuesta = response.Content.ReadAsStringAsync().Result;
                    }
                }
                else
                {
                    // Si no se puede obtener el tipo de cambio, manejar el error aquí
                }
            }

            return Json(new { status = status, respuesta = respuesta }, JsonRequestBehavior.AllowGet);
        }


        [AuthorizeCliente(2)]
        public async Task<ActionResult> CheckPago()
        {

            //id de la autorizacion para obtener el dinero

            string token = Request.QueryString["token"];


            bool status = false;

            using (var client = new HttpClient())
            {
                var userName = PayPalUserName;
                var passwd = PayPalPassword;

                client.BaseAddress = new Uri("https://api-m.paypal.com");

                var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                var datos = modelCarrito.ConsultarCarrito(long.Parse(Session["ID_Usuario"].ToString()));


                var data = new StringContent("{}", Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"/v2/checkout/orders/{token}/capture", data);

                status = response.IsSuccessStatusCode;


                ViewData["Status"] = status;
                if (status)
                {
                    var jsonRespuesta = response.Content.ReadAsStringAsync().Result;

                    PayPal2Model objeto = JsonConvert.DeserializeObject<PayPal2Model>(jsonRespuesta);

                    ViewData["IdTransaccion"] = objeto.purchase_units[0].payments.captures[0].id;

                    Session["IdTransaccion"] = ViewData["IdTransaccion"];
                    PagarCarrito();
                }

            }


            string idtransaccion = Session["IdTransaccion"].ToString();
            long numfactura = modelCarrito.ConsultarFacturaRealizada(long.Parse(Session["ID_Usuario"].ToString()));
            long idcliente = long.Parse(Session["ID_Usuario"].ToString());

            PedidoEnt pedidoEnt = new PedidoEnt
            {
                ID_Transaccion = idtransaccion,
                ID_Factura = numfactura,
                ID_Cliente = idcliente,
                Estado = 0
            };

            var respuestaRegistroPedido = modelPedidos.RegistrarPedido(pedidoEnt);
            var respuestaConsultaPedido = modelPedidos.ConsultarPedido(idtransaccion);


            return View(respuestaConsultaPedido);
        }

    }

    
}


