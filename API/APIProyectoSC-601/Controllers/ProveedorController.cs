using APIProyectoSC_601.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace APIProyectoSC_601.Controllers
{
    public class ProveedorController : ApiController
    {

        private readonly Errores log;
        private readonly LogExitos logExitos;

        public string RutaErrores { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["RutaErrores"];
        public string RutaExitos { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["RutaExitos"];


        public ProveedorController()
        {

            string rutaErrores = RutaErrores;
            string rutaExitos = RutaExitos;


            log = new Errores(rutaErrores);
            logExitos = new LogExitos(rutaExitos);
        }


        /*En este metodo post se van a hacer todos los registros de proveedor
         procesa la solicitud de registro de un proveedor, interactúa con la base de 
         datos a través de un procedimiento almacenado y devuelve un resultado indicando el éxito o fallo de la operación.*/

        [HttpPost]
        [Route("RegistrarProveedor")]
        public string RegistrarProveedor(ProveedorEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {

                    context.RegistrarProveedorSP(entidad.Nombre_Proveedor, entidad.Apellido_Proveedor, entidad.ID_Identificacion, entidad.Cedula_Proveedor, entidad.Direccion_Exacta, entidad.Estado_Proveedor, entidad.Empresa, entidad.Telefono, entidad.Correo);
                    logExitos.Add("RegistrarProveedor", "Proveedor registrado exitosamente");
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                // Registrar el error en el log
                log.Add("Error en RegistrarProveedor: " + ex.Message);
                return string.Empty;
            }
        }



        /*Esto una lista de elementos SelectListItem que sirve para poblar un cuadro de 
         selección HTML en una interfaz de usuario, permitiendo al usuario elegir una empresa de una lista predefinida.*/

        [HttpGet]
        [Route("ConsultarEmpresas")]
        public List<System.Web.Mvc.SelectListItem> ConsultarEmpresas()
        {
            try
            {
                using (var contexto = new ImportadoraMoyaUlateEntities())
                {
                    var datos = (from x in contexto.Empresa
                                 select x).ToList();

                    var combo = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        combo.Add(new System.Web.Mvc.SelectListItem
                        {
                            Value = item.ID_Empresa.ToString(),
                            Text = item.Nombre_empresa
                        });
                    }

                    logExitos.Add("ConsultarEmpresas", "Consulta de empresas realizada exitosamente");

                    return combo;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultarEmpresas: " + ex.Message);
                // Devuelve una lista vacía en caso de error.
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }



        /*lista de objetos Proveedores que representan la información de todos los proveedores almacenados en la base de datos. 
         Si se produce alguna excepción durante la consulta, devuelve una lista vacía.*/
        [HttpGet]
        [Route("ConsultaProveedores")]
        public List<ProveedorEnt> ConsultaProveedores()
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;

                    var proveedores = (from p in context.Proveedores
                                       join i in context.Identificacion on p.ID_Identificacion equals i.ID_Identificacion into identificacionJoin
                                       from ident in identificacionJoin.DefaultIfEmpty()
                                       select new ProveedorEnt
                                       {
                                           ID_Proveedor = p.ID_Proveedor,
                                           ID_Identificacion = p.ID_Identificacion,
                                           Nombre_Identificacion = ident != null ? ident.Nombre : "",
                                           Nombre_Proveedor = p.Nombre_Proveedor,
                                           Apellido_Proveedor = p.Apellido_Proveedor,
                                           Cedula_Proveedor = p.Cedula_Proveedor,
                                           Direccion_Exacta = p.Direccion_Exacta,
                                           Estado_Proveedor = p.Estado_Proveedor,
                                           Empresa = p.Empresa,
                                           Telefono = p.Telefono,
                                           Correo = p.Correo,
                                       }).ToList();
                    logExitos.Add("ConsultaProveedores", "Consulta de proveedores realizada exitosamente");
                    return proveedores;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultaProveedores: " + ex.Message);
                return new List<ProveedorEnt>();
            }
        }


        /*Este metodo es para proporcionar la información de un proveedor específico
         según el ID proporcionado. Si se produce alguna excepción durante la consulta, devuelve null.*/

        [HttpGet]
        [Route("ConsultaProveedor")]
        public ProveedorEnt ConsultaProveedor(long q)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;

                    var proveedor = (from p in context.Proveedores
                                     join i in context.Identificacion on p.ID_Identificacion equals i.ID_Identificacion into identificacionJoin
                                     from ident in identificacionJoin.DefaultIfEmpty()
                                     where p.ID_Proveedor == q
                                     select new ProveedorEnt
                                     {
                                         ID_Proveedor = p.ID_Proveedor,
                                         ID_Identificacion = p.ID_Identificacion,
                                         Nombre_Identificacion = ident != null ? ident.Nombre : "",
                                         Nombre_Proveedor = p.Nombre_Proveedor,
                                         Apellido_Proveedor = p.Apellido_Proveedor,
                                         Cedula_Proveedor = p.Cedula_Proveedor,
                                         Direccion_Exacta = p.Direccion_Exacta,
                                         Estado_Proveedor = p.Estado_Proveedor,
                                         Empresa = p.Empresa,
                                         Telefono = p.Telefono,
                                         Correo = p.Correo,
                                     }).FirstOrDefault();
                    logExitos.Add("ConsultaProveedor", $"Consulta de proveedor con ID {q} realizada exitosamente");
                    return proveedor;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultaProveedor: " + ex.Message);
                return null;
            }
        }




        /*Este metodo es para actualizar el estado de un proveedor utilizando un procedimiento almacenado en una base de datos.
        Si la operación tiene éxito, devuelve "OK". En caso de errores, captura excepciones y devuelve un mensaje de error detallado.*/

        [HttpPut]
        [Route("ActualizarEstadoProveedor")]
        public string ActualizarEstadoProveedor(ProveedorEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.ActualizarEstadoProveedorSP(entidad.ID_Proveedor);
                    logExitos.Add("ActualizarEstadoProveedor", $"Actualización de estado del proveedor con ID {entidad.ID_Proveedor} realizada exitosamente");
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ActualizarEstadoProveedor: " + ex.Message);
                return $"Error al actualizar el estado del proveedor: {ex.Message}";
            }
        }

        /*Sirve para actualizar la información de un proveedor en la base de datos mediante un procedimiento almacenado. Si la operación tiene éxito, 
        devuelve "OK". En caso de errores, captura excepciones y devuelve una caden*/

        [HttpPut]
        [Route("ActualizarProveedor")]
        public string ActualizarProveedor(ProveedorEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    if (entidad.Nombre_Identificacion.Equals("Cédula Jurídica"))
                    {
                        entidad.Apellido_Proveedor = string.Empty;
                    }
                    context.ActualizarProveedorSP(entidad.ID_Proveedor, entidad.Nombre_Proveedor, entidad.Apellido_Proveedor, entidad.Direccion_Exacta, entidad.Empresa, entidad.Telefono, entidad.Correo);
                    logExitos.Add("ActualizarProveedor", $"Actualización del proveedor con ID {entidad.ID_Proveedor} realizada exitosamente");
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ActualizarProveedor: " + ex.Message);
                return string.Empty;
            }
        }



        /*Esto es para eliminar  un proveedor de la base de datos identificado por su ID. Si la operación tiene éxito, devuelve "OK" y se borra,
         Si el proveedor no se encuentra, devuelve "Proveedor no encontrado".*/

        [HttpDelete]
        [Route("EliminarProveedor")]
        public string EliminarProveedor(long q)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var proveedorAEliminar = context.Proveedores.FirstOrDefault(p => p.ID_Proveedor == q);

                    if (proveedorAEliminar != null)
                    {
                        context.Proveedores.Remove(proveedorAEliminar);
                        context.SaveChanges();
                        logExitos.Add("EliminarProveedor", $"Eliminación del proveedor con ID {q} realizada exitosamente");
                        return "OK";
                    }
                    else
                    {
                        return "Proveedor no encontrado.";
                    }
                }
            }
            catch (Exception ex)
            {
                log.Add("Error al EliminarProveedor: " + ex.Message);
                return string.Empty;
            }
        }

        [HttpGet]
        [Route("ConsultarIdentificacionesProveedor")]
        public List<System.Web.Mvc.SelectListItem> ConsultarIdentificacionesProveedor()
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var datos = (from x in context.Identificacion
                                 select x).ToList();

                    List<System.Web.Mvc.SelectListItem> identificaciones = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        identificaciones.Add(new System.Web.Mvc.SelectListItem
                        {
                            Value = item.ID_Identificacion.ToString(),
                            Text = item.Nombre
                        });
                    }
                    logExitos.Add("ConsultarIdentificacionesProveedor", "Consulta de identificaciones de proveedores realizada exitosamente");
                    return identificaciones;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultarIdentificacionesProveedor: " + ex.Message);
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }


        //Verifica si la cedula ya existe
        [HttpPost]
        [Route("ComprobarCedulaProveedor")]
        public string ComprobarCedulaProveedor(ProveedorEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;

                    // Verificar si la cedula existe antes de ejecutar la consulta
                    bool cedulaProveedorExiste = context.Proveedores.Any(x => x.Cedula_Proveedor == entidad.Cedula_Proveedor && x.ID_Proveedor != entidad.ID_Proveedor);

                    if (cedulaProveedorExiste)
                    {
                        // Devuelve que ya existe la cedula
                        logExitos.Add("ComprobarCedulaProveedor", "Consulta de cédula de proveedor realizada exitosamente, Existe");
                        return "Existe";
                    }
                    else
                    {
                        logExitos.Add("ComprobarCedulaProveedor", "Consulta de cédula de proveedor realizada exitosamente, No existe");
                        return "NoExiste";
                    }

                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ComprobarCedulaProveedor: " + ex.Message);
                return null;
            }

        }




    }
}
