using APIProyectoSC_601.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIProyectoSC_601.Controllers
{
    public class EmpresaController : ApiController
    {

        /*En este metodo post se van a hacer todos los registros de empresa
         procesa la solicitud de registro de una empresa, interactúa con la base de 
         datos a través de un procedimiento almacenado y devuelve un resultado indicando el éxito o fallo de la operación.*/

        [HttpPost]
        [Route("RegistrarEmpresa")]
        public string RegistrarEmpresa(EmpresaEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {

                    context.RegistrarEmpresaSP(entidad.Nombre_empresa, entidad.Descripcion, entidad.Ubicacion);
                    return "OK";
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }



        /*Sirve para actualizar la información de una empresa en la base de datos mediante un procedimiento almacenado. Si la operación tiene éxito, 
        devuelve "OK". En caso de errores, captura excepciones y devuelve una caden*/

        [HttpPut]
        [Route("ActualizarEmpresa")]
        public string ActualizarEmpresa(EmpresaEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.ActualizarEmpresaSP(entidad.ID_Empresa,entidad.Nombre_empresa, entidad.Descripcion, entidad.Ubicacion);
                    return "OK";
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }



        /*Esto es para eliminar  una empresa de la base de datos identificada por su ID. Si la operación tiene éxito, devuelve "OK" y se borra,
         Si la empresa no se encuentra, devuelve "Empresa no encontrado".*/

        [HttpDelete]
        [Route("EliminarEmpresa")]
        public string EliminarEmpresa(long q)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var empresaAEliminar = context.Empresa.FirstOrDefault(p => p.ID_Empresa == q);

                    if (empresaAEliminar != null)
                    {
                        context.Empresa.Remove(empresaAEliminar);
                        context.SaveChanges();
                        return "OK";
                    }
                    else
                    {
                        return "Empresa no encontrado.";
                    }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        [HttpGet]
        [Route("ConsultaEmpresas")]
        public List<Empresa> ConsultaEmpresas()
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.Empresa
                            select x).ToList();
                }
            }
            catch (Exception)
            {
                return new List<Empresa>();
            }
        }

        [HttpGet]
        [Route("ConsultaEmpresa")]
        public Empresa ConsultaEmpresa(long q)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.Empresa
                            where x.ID_Empresa == q
                            select x).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }




    }
}
