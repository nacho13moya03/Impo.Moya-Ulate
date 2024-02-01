using APIProyectoSC_601.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIProyectoSC_601.Controllers
{
    public class UsuarioController : ApiController
    {
        Errores log = new Errores(@"C:\Users\Angel\Documents\GitHub\Proyecto Diseño\Impo.Moya-Ulate\Logs");

        //Se crea instancia para usar herramientas necesarias para enviar correo de recuperacion al cliente
        Utilitarios util = new Utilitarios();

        [HttpGet]
        [Route("ConsultarTiposIdentificaciones")]
        public List<System.Web.Mvc.SelectListItem> ConsultarTiposIdentificaciones()
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

                    return identificaciones;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultarTiposIdentificaciones: " + ex.Message);
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

        //Conexion a procedimiento para registrar clientes
        [HttpPost]
          [Route("RegistroUsuario")]
          public string RegistroUsuario(UsuarioEnt entidad)
          {
              try
              {
                  //Se asgina inicialmente la direccion y telefono como vacio
                  
                  entidad.Telefono_Usuario = string.Empty;
                  using (var context = new ImportadoraMoyaUlateEntities())
                  {

                      context.RegistrarUsuarioSP(entidad.ID_Identificacion,entidad.Identificacion_Usuario, entidad.Nombre_Usuario, entidad.Apellido_Usuario, entidad.Correo_Usuario, entidad.Contrasenna_Usuario, entidad.Telefono_Usuario);
                      return "OK";
                  }
              }
              catch (Exception ex)
              {
                  log.Add("Error en RegistroUsuario: " + ex.Message);
                  return string.Empty;
              }
          }
        
        //Conexion a procedimiento para verificar los datos del login y permitir o negar el inicio de sesion
        [HttpPost]
        [Route("Login")]
        public IniciarSesionSP_Result Login(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    return context.IniciarSesionSP(entidad.Correo_Usuario, entidad.Contrasenna_Usuario).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en Login: " + ex.Message);
                return null;
            }
        }

        //Se comprueba si existe la cedula ingresada y se envia correo de recuperacion con nombre y contrasenna al cliente
        [HttpGet]
        [Route("RecuperarCuentaUsuario")]
        public string RecuperarCuentaUsuario(string Correo)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var datos = context.RecuperarCuentaUsuarioSP(Correo).FirstOrDefault();

                    if (datos != null)
                    {
                        string rutaArchivo = AppDomain.CurrentDomain.BaseDirectory + "Templates\\Contrasenna.html";
                        string html = File.ReadAllText(rutaArchivo);

                        html = html.Replace("@@Nombre", datos.Nombre_Usuario + " "+datos.Apellido_Usuario);
                        html = html.Replace("@@Contrasenna", datos.Contrasenna_Usuario);

                        util.EnviarCorreo(datos.Correo_Usuario, "Contraseña de Acceso", html);
                        return "OK";
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en RecuperarCuentaUsuario: " + ex.Message);
                return string.Empty;
            }
        }

        //Funcion para contar la cantidad de clientes registrados en la pagina
        [HttpGet]
        [Route("ContarUsuarios")]
        public int ContarUsuarios()
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return context.Usuario.Count(x => x.ID_Rol == 2);
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ContarUsuarios: " + ex.Message);
                return 0;
            }
        }


        //Verifica si el correo ya existe
        [HttpPost]
        [Route("ComprobarCorreoExistenteUsuario")]
        public string ComprobarCorreoExistenteUsuario(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;

                    // Verificar si el correo existe antes de ejecutar la consulta
                    bool correoExiste = context.Usuario.Any(x => x.Correo_Usuario == entidad.Correo_Usuario && x.ID_Usuario != entidad.ID_Usuario);

                    if (correoExiste)
                    {
                        // El correo existe, devuelve un valor que indica que existe
                        return "Existe";
                    }
                    else
                    {
                        return "NoExiste";
                    }

                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ComprobarCorreoExistenteUsuario: " + ex.Message);
                return null;
            }

        }

        //Verifica si la cedula ya existe
        [HttpPost]
        [Route("ComprobarCedulaExistente")]
        public string ComprobarCedulaExistente(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;

                    // Verificar si la cedula existe antes de ejecutar la consulta
                    bool cedulaExiste = context.Usuario.Any(x => x.Identificacion_Usuario == entidad.Identificacion_Usuario && x.ID_Usuario != entidad.ID_Usuario);

                    if (cedulaExiste)
                    {
                        // Devuelve que ya existe la cedula
                        return "Existe";
                    }
                    else
                    {
                        return "NoExiste";
                    }

                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ComprobarCedulaExistente: " + ex.Message);
                return null;
            }

        }

        //Devuelve los datos de la entidad basados en la cedula recibida
        /*      [HttpGet]
              [Route("ConsultaClienteEspecifico")]
              public Clientes ConsultaClienteEspecifico(long q)
              {
                  try
                  {
                      using (var context = new ImportadoraMoyaUlateEntities())
                      {
                          context.Configuration.LazyLoadingEnabled = false;
                          return (from x in context.Clientes
                                  where x.ID_Cliente == q
                                  select x).FirstOrDefault();
                      }
                  }
                  catch (Exception ex)
                  {
                      log.Add("Error en ConsultaClienteEspecifico: " + ex.Message);
                      return null;
                  }
              }

              //Conexion a procedimiento para actualizar los datos del cliente desde el perfil
              [HttpPut]
              [Route("ActualizarCuentaCliente")]
              public string ActualizarCuentaCliente(UsuarioEnt entidad)
              {
                  try
                  {
                      using (var context = new ImportadoraMoyaUlateEntities())
                      {
                          context.ActualizarCuentaClienteSP(entidad.Ced_Cliente, entidad.Nombre_Cliente, entidad.Apellido_Cliente, entidad.Correo_Cliente, entidad.Direccion_Cliente, entidad.Tel_Cliente, entidad.ID_Cliente);
                          return "OK";
                      }
                  }
                  catch (Exception ex)
                  {
                      log.Add("Error en ActualizarCuentaCliente: " + ex.Message);
                      return string.Empty;
                  }
              }

              //Conexion a procedimiento para inactivar al cliente
              [HttpPut]
              [Route("InactivarCliente")]
              public void InactivarCliente(UsuarioEnt entidad)
              {
                  try
                  {
                      using (var context = new ImportadoraMoyaUlateEntities())
                      {
                          context.InactivarClienteSP(entidad.ID_Cliente);
                      }
                  }
                  catch (Exception ex)
                  {
                      log.Add("Error en InactivarCliente: " + ex.Message);
                  }
              }


              //Devuelve todos los clientes registrados, solo rol de usuario
              [HttpGet]
              [Route("ConsultarClientesAdministrador")]
              public List<Clientes> ConsultarClientesAdministrador()
              {
                  try
                  {
                      using (var context = new ImportadoraMoyaUlateEntities())
                      {
                          context.Configuration.LazyLoadingEnabled = false;
                          return (from x in context.Clientes
                                  where x.Rol_Cliente == 2 select x).ToList();
                      }
                  }
                  catch (Exception ex)
                  {
                      log.Add("Error en ConsultarClientesAdministrador: " + ex.Message);
                      return null;
                  }
              }

              // Permite al administrador cambiar el estado del cliente (activar o inactivar)
              [HttpPut]
              [Route("ActualizarEstadoCliente")]
              public string ActualizarEstadoCliente(UsuarioEnt entidad)
              {
                  try
                  {
                      using (var context = new ImportadoraMoyaUlateEntities())
                      {
                          context.ActualizarEstadoClienteSP(entidad.ID_Cliente);
                          return "OK";
                      }
                  }
                  catch (Exception ex)
                  {
                      log.Add("Error en ActualizarEstadoCliente: " + ex.Message);
                      return $"Error al actualizar el estado del cliente: {ex.Message}";
                  }
              }

              

        */
    }
}
