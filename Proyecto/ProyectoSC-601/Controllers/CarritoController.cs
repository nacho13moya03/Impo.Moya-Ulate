using Newtonsoft.Json;
using ProyectoSC_601.Entities;
using ProyectoSC_601.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
/*PayPal*/
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using static ProyectoSC_601.Models.CarritoModel;
/*PayPal*/

namespace ProyectoSC_601.Controllers
{
    public class CarritoController : Controller
    {


        CarritoModel modelCarrito = new CarritoModel();
        PedidosModel modelPedidos = new PedidosModel();
        UsuarioModel modelUsuario = new UsuarioModel();
        FacturacionModel modelFacturacion = new FacturacionModel();
        InventarioModel modelInventario = new InventarioModel();

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

        [HttpGet]
        public ActionResult Carrito()
        {

            var datos = modelCarrito.ConsultarCarrito(long.Parse(Session["ID_Usuario"].ToString()));
            Session["SumaSubTotal"] = datos.AsEnumerable().Sum(x => x.SubTotal);
            Session["TotalPago"] = datos.AsEnumerable().Sum(x => x.Total);
            return View(datos);

        }

        [HttpGet]
        public ActionResult MetodoPago()
        {
            return View();
        }


        [HttpGet]
        public ActionResult EliminarRegistroCarrito(long q)
        {
            modelCarrito.EliminarRegistroCarrito(q);

            var datos = modelCarrito.ConsultarCarrito(long.Parse(Session["ID_Usuario"].ToString()));
            Session["Cant"] = datos.AsEnumerable().Sum(x => x.Cantidad);
            Session["SubT"] = datos.AsEnumerable().Sum(x => x.SubTotal);
            return RedirectToAction("Carrito", "Carrito");
        }

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

        [HttpPost]
        public async Task<JsonResult> Paypal(string precio, string producto)
        {

            bool status = false;
            string respuesta = string.Empty;

            using (var client = new HttpClient())
            {
                var userName = "ASMuZ2JIH6RMroddxn_QDja6RoNSFyoAi3zJRoO4jxtqT0Vezq4fDAFQMP41krlWHkipksJ03aCPpniY";
                var passwd = "EHdDA3W-uwKW9i34JrQ26bH_Cml16G-TJiAzSaWZBDuxhUETz4cdn8HftHYV3doZ2kMMq76L6GfH_4G4";

                client.BaseAddress = new Uri("https://api-m.sandbox.paypal.com");

                var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                var datos = modelCarrito.ConsultarCarrito(long.Parse(Session["ID_Usuario"].ToString()));

                var total = datos.AsEnumerable().Sum(x => x.Total);

                var valortotal = Math.Round(total / 522, 2).ToString();

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
                        user_action = "PAY_NOW", //Accion para que paypal muestre el monto de pago
                        return_url = "http://nacho13-001-site1.ltempurl.com/Carrito/CheckPago",// cuando se aprovo la solicitud del cobro
                        cancel_url = "http://nacho13-001-site1.ltempurl.com/"// cuando cancela la operacion
                    }
                };

                var json = JsonConvert.SerializeObject(orden);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("/v2/checkout/orders", data);

                status = response.IsSuccessStatusCode;

                if (status)
                {
                    respuesta = response.Content.ReadAsStringAsync().Result;
                }

            }

            return Json(new { status = status, respuesta = respuesta }, JsonRequestBehavior.AllowGet);

        }

        public async Task<ActionResult> CheckPago()
        {

            //id de la autorizacion para obtener el dinero

            string token = Request.QueryString["token"];


            bool status = false;

            using (var client = new HttpClient())
            {
                var userName = "ASMuZ2JIH6RMroddxn_QDja6RoNSFyoAi3zJRoO4jxtqT0Vezq4fDAFQMP41krlWHkipksJ03aCPpniY";
                var passwd = "EHdDA3W-uwKW9i34JrQ26bH_Cml16G-TJiAzSaWZBDuxhUETz4cdn8HftHYV3doZ2kMMq76L6GfH_4G4";

                client.BaseAddress = new Uri("https://api-m.sandbox.paypal.com");

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


