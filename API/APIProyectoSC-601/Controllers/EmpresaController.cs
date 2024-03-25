using APIProyectoSC_601.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace APIProyectoSC_601.Controllers
{
    public class EmpresaController : ApiController
    {
        private readonly Errores log;
        private readonly LogExitos logExitos;

        public string RutaErrores { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["RutaErrores"];
        public string RutaExitos { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["RutaExitos"];

        public EmpresaController()
        {
            string rutaErrores = RutaErrores;
            string rutaExitos = RutaExitos;


            log = new Errores(rutaErrores);
            logExitos = new LogExitos(rutaExitos);
        }



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
                    logExitos.Add("RegistrarEmpresa", $"Empresa '{entidad.Nombre_empresa}' registrada exitosamente");
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en RegistrarEmpresa: " + ex.Message);
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
                    context.ActualizarEmpresaSP(entidad.ID_Empresa, entidad.Nombre_empresa, entidad.Descripcion, entidad.Ubicacion);
                    logExitos.Add("ActualizarEmpresa", $"Información de la empresa '{entidad.Nombre_empresa}' actualizada exitosamente.");
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ActualizarEmpresa: " + ex.Message);
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
                        logExitos.Add("EliminarEmpresa", $"Empresa '{empresaAEliminar.Nombre_empresa}' eliminada exitosamente.");
                        return "OK";
                    }
                    else
                    {
                        return "Empresa no encontrado.";
                    }
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en EliminarEmpresa: " + ex.Message);

                return string.Empty;
            }
        }



        [HttpPost]
        [Route("VerificarProveedoresVinculados")]
        public bool VerificarProveedoresVinculados(EmpresaEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    // Verificar si hay proveedores vinculados a la empresa específica
                    bool hayProveedoresVinculados = context.Proveedores.Any(p => p.Empresa == entidad.ID_Empresa);

                    if (hayProveedoresVinculados)
                    {
                        logExitos.Add("VerificarProveedoresVinculados", $"Existen proveedores vinculados a la empresa con ID {entidad.ID_Empresa}.");
                    }
                    else
                    {
                        logExitos.Add("VerificarProveedoresVinculados", $"No hay proveedores vinculados a la empresa con ID {entidad.ID_Empresa}.");
                    }

                    return hayProveedoresVinculados;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en VerificarProveedoresVinculados: " + ex.Message);
                return false;
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
                    var empresas = (from x in context.Empresa
                                    select x).ToList();

                    if (empresas.Count > 0)
                    {
                        logExitos.Add("ConsultaEmpresas", $"Se consultaron satisfactoriamente {empresas.Count} empresas.");
                    }
                    else
                    {
                        logExitos.Add("ConsultaEmpresas", "No se encontraron empresas.");
                    }

                    return empresas;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultaEmpresas: " + ex.Message);
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
                    var empresa = (from x in context.Empresa
                                   where x.ID_Empresa == q
                                   select x).FirstOrDefault();

                    if (empresa != null)
                    {
                        logExitos.Add("ConsultaEmpresa", $"Se consultó satisfactoriamente la empresa con ID {q}.");
                    }
                    else
                    {
                        logExitos.Add("ConsultaEmpresa", $"No se encontró la empresa con ID {q}.");
                    }

                    return empresa;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultaEmpresa: " + ex.Message);
                return null;
            }
        }



        [HttpPost]
        [Route("VerificarComprasVinculadas")]
        public bool VerificarComprasVinculadas(EmpresaEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    // Verificar si hay proveedores vinculados a la empresa específica
                    bool hayComprasVinculadas = context.compras.Any(p => p.Empresa == entidad.ID_Empresa);

                    if (hayComprasVinculadas)
                    {
                        logExitos.Add("VerificarComprasVinculadas", $"Existen compras vinculados a la empresa con ID {entidad.ID_Empresa}.");
                    }
                    else
                    {
                        logExitos.Add("VerificarComprasVinculadas", $"No hay compras vinculados a la empresa con ID {entidad.ID_Empresa}.");
                    }

                    return hayComprasVinculadas;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en VerificarComprasVinculadas: " + ex.Message);
                return false;
            }
        }





    }
}
