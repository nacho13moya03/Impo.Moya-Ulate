using APIProyectoSC_601.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web.Http;


namespace APIProyectoSC_601.Controllers
{
    public class ComprasController : ApiController
    {
        private readonly Errores log;
        private readonly LogExitos logExitos;

        public string RutaErrores { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["RutaErrores"];
        public string RutaExitos { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["RutaExitos"];


        public ComprasController()
        {
            string rutaErrores = RutaErrores;
            string rutaExitos = RutaExitos;


            log = new Errores(rutaErrores);
            logExitos = new LogExitos(rutaExitos);
        }



        [HttpPost]
        [Route("RegistrarCompra")]
        public string RegistrarCompra(ComprasEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {

                    context.RegistrarCompra(entidad.Empresa, entidad.Fecha, entidad.Concepto, entidad.Cantidad, entidad.Total);
                    logExitos.Add("RegistrarCompra", "Compra registrada exitosamente");
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                // Registrar el error en el log
                log.Add("Error en RegistrarCompra: " + ex.Message);
                return string.Empty;
            }
        }

        [HttpPut]
        [Route("ActualizarCompra")]
        public string ActualizarCompra(ComprasEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.ActualizarCompra(entidad.IdCompras, entidad.Empresa, entidad.Fecha, entidad.Concepto, entidad.Cantidad, entidad.Total);
                    logExitos.Add("ActualizarCompra", $"Actualización de la compra con ID {entidad.IdCompras} realizada exitosamente");
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ActualizarCompra: " + ex.Message);
                return string.Empty;
            }
        }



        /*Esto es para eliminar  un proveedor de la base de datos identificado por su ID. Si la operación tiene éxito, devuelve "OK" y se borra,
         Si el proveedor no se encuentra, devuelve "Proveedor no encontrado".*/

        [HttpDelete]
        [Route("EliminarCompra")]
        public string EliminarCompra(long q)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var compraAEliminar = context.compras.FirstOrDefault(p => p.id_compras == q);

                    if (compraAEliminar != null)
                    {
                        context.compras.Remove(compraAEliminar);
                        context.SaveChanges();
                        logExitos.Add("EliminarCompra", $"Eliminación de la compra con ID {q} realizada exitosamente");
                        return "OK";
                    }
                    else
                    {
                        return "Compra no encontrado.";
                    }
                }
            }
            catch (Exception ex)
            {
                log.Add("Error al EliminarCompra: " + ex.Message);
                return string.Empty;
            }
        }



        [HttpGet]
        [Route("ConsultaCompras")]
        public List<ComprasEnt> ConsultaCompras()
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var compras = context.compras
                        .Select(c => new ComprasEnt
                        {
                            IdCompras = c.id_compras,
                            Empresa = c.Empresa,
                            Fecha = c.fecha,
                            Concepto = c.concepto,
                            Cantidad = c.cantidad,
                            Total = c.total
                        })
                        .ToList();

                    if (compras.Any())
                    {
                        logExitos.Add("ConsultaCompras", $"Se consultaron satisfactoriamente {compras.Count} compras.");
                    }
                    else
                    {
                        logExitos.Add("ConsultaCompras", "No se encontraron compras.");
                    }

                    return compras;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultaCompras: " + ex.Message);
                return new List<ComprasEnt>();
            }
        }



        [HttpGet]
        [Route("ConsultaCompra")]
        public ComprasEnt ConsultaCompra(int q)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var compraEntity = context.compras.FirstOrDefault(c => c.id_compras == q);

                    var compraEnt = (compraEntity != null)
                        ? new ComprasEnt
                        {
                            IdCompras = compraEntity.id_compras,
                            Empresa = compraEntity.Empresa,
                            Fecha = compraEntity.fecha,
                            Concepto = compraEntity.concepto,
                            Cantidad = compraEntity.cantidad,
                            Total = compraEntity.total
                        }
                        : null;

                    if (compraEnt != null)
                    {
                        logExitos.Add("ConsultaCompra", $"Se consultó satisfactoriamente la compra con ID {q}.");
                    }
                    else
                    {
                        logExitos.Add("ConsultaCompra", $"No se encontró la compra con ID {q}.");
                    }

                    return compraEnt;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultaCompra: " + ex.Message);
                return null;
            }
        }




    }
}