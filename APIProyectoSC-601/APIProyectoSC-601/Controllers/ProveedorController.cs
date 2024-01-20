using APIProyectoSC_601.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIProyectoSC_601.Controllers
{
    public class ProveedorController : ApiController
    {

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

                    context.RegistrarProveedorSP(entidad.Nombre_Proveedor, entidad.Apellido_Proveedor, entidad.Cedula_Proveedor, entidad.Direccion_Exacta, entidad.Estado_Proveedor, entidad.Empresa);
                    return "OK";
                }
            }
            catch (Exception)
            {
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

                    return combo;
                }
            }
            catch (Exception)
            {
                // Devuelve una lista vacía en caso de error.
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }



        /*lista de objetos Proveedores que representan la información de todos los proveedores almacenados en la base de datos. 
         Si se produce alguna excepción durante la consulta, devuelve una lista vacía.*/

        [HttpGet]
        [Route("ConsultaProveedores")]
        public List<Proveedores> ConsultaProveedores()
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.Proveedores
                            select x).ToList();
                }
            }
            catch (Exception)
            {
                return new List<Proveedores>();
            }
        }


        /*Este metodo es para proporcionar la información de un proveedor específico
         según el ID proporcionado. Si se produce alguna excepción durante la consulta, devuelve null.*/

        [HttpGet]
        [Route("ConsultaProveedor")]
        public Proveedores ConsultaProveedor(long q)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.Proveedores
                            where x.ID_Proveedor == q
                            select x).FirstOrDefault();
                }
            }
            catch (Exception)
            {
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
                    return "OK";
                }
            }
            catch (Exception ex)
            {
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
                    context.ActualizarProveedorSP(entidad.ID_Proveedor, entidad.Nombre_Proveedor, entidad.Apellido_Proveedor, entidad.Cedula_Proveedor, entidad.Direccion_Exacta, entidad.Empresa);
                    return "OK";
                }
            }
            catch (Exception)
            {
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
                        return "OK";
                    }
                    else
                    {
                        return "Proveedor no encontrado.";
                    }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }




    }
}
