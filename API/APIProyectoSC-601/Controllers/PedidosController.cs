using APIProyectoSC_601.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web.Http;


namespace APIProyectoSC_601.Controllers
{
    public class PedidosController : ApiController
    {
        private readonly Errores log;
        private readonly LogExitos logExitos;

        public string RutaErrores { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["RutaErrores"];
        public string RutaExitos { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["RutaExitos"];


        public PedidosController()
        {
            string rutaErrores = RutaErrores;
            string rutaExitos = RutaExitos;


            log = new Errores(rutaErrores);
            logExitos = new LogExitos(rutaExitos);
        }

        [HttpPost]
        [Route("RegistrarPedido")]
        public string RegistrarPedido(Pedidos pedido)
        {
            try
            {
                using (var context = new db_aa61bd_impomyuEntities())
                {
                    context.Pedidos.Add(pedido);
                    context.SaveChanges();
                    logExitos.Add("RegistrarPedido", $"Se registró satisfactoriamente el pedido con ID {pedido.ID_Pedido}.");
                    return "Ok";
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en RegistrarPedido: " + ex.Message);
                return string.Empty;
            }
        }

        [HttpGet]
        [Route("ConsultarPedido")]
        public PedidoEnt ConsultarPedido(string idtransaccion)
        {
            try
            {
                using (var context = new db_aa61bd_impomyuEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var usuario = (from p in context.Pedidos
                                   join u in context.Usuario on p.ID_Cliente equals u.ID_Usuario
                                   join d in context.Direcciones on u.ID_Direccion equals d.ID_Direccion into direccionJoin
                                   from dir in direccionJoin.DefaultIfEmpty() // Left join for Direcciones
                                   where p.ID_Transaccion == idtransaccion
                                   select new PedidoEnt
                                   {
                                       ID_Pedido = p.ID_Pedido,
                                       ID_Transaccion = p.ID_Transaccion,
                                       ID_Factura = p.ID_Factura,
                                       ID_Cliente = p.ID_Cliente,
                                       direccionPedido = dir != null ? dir.Direccion_Exacta : "",
                                   }).FirstOrDefault();

                    if (usuario != null)
                    {
                        log.Add("Consulta exitosa para el pedido con ID: " + idtransaccion);
                    }

                    return usuario;

                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultaPedidoEspecifico: " + ex.Message);
                return null;
            }
        }

        [HttpGet]
        [Route("ConsultarPedidos")]
        public List<PedidoEnt> ConsultarPedidos()
        {
            try
            {
                using (var context = new db_aa61bd_impomyuEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var usuario = (from p in context.Pedidos
                                   join u in context.Usuario on p.ID_Cliente equals u.ID_Usuario
                                   orderby p.Estado ascending, p.ID_Pedido descending
                                   select new PedidoEnt
                                   {
                                       ID_Pedido = p.ID_Pedido,
                                       ID_Transaccion = p.ID_Transaccion,
                                       ID_Factura = p.ID_Factura,
                                       ID_Cliente = p.ID_Cliente,
                                       identificacionCliente = u.Identificacion_Usuario,
                                       Estado = p.Estado
                                   }).ToList();

                    if (usuario != null)
                    {
                        log.Add("Consulta exitosa para los pedidos ");
                    }

                    return usuario;

                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultaPedidos: " + ex.Message);
                return null;
            }
        }

        [HttpPut]
        [Route("ActualizarEstadoPedido")]
        public string ActualizarEstadoPedido(PedidoEnt entidad)
        {
            try
            {
                using (var context = new db_aa61bd_impomyuEntities())
                {
                    var datos = context.Pedidos.Where(x => x.ID_Pedido == entidad.ID_Pedido).FirstOrDefault();

                    if (datos != null)
                    {

                        datos.Estado = 1;
                        context.SaveChanges();
                        logExitos.Add("ActualizarEstadoPedido", $"Se actualizó el estado del pedido con ID {entidad.ID_Pedido}.");
                        return "OK";
                    }
                    else
                    {
                        return "Pedido no encontrado";
                    }
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ActualizarEstadoPedido: " + ex.Message);
                return $"Error al actualizar el estado del pedido: {ex.Message}";
            }
        }

    }
}