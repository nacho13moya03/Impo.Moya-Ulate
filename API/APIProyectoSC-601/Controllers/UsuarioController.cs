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

        //Devuelve todos los clientes registrados, solo rol de usuario
        [HttpGet]
        [Route("ConsultarUsuariosAdministrador")]
        public List<UsuarioEnt> ConsultarUsuariosAdministrador(long idUsuario)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from u in context.Usuario
                            join i in context.Identificacion on u.ID_Identificacion equals i.ID_Identificacion
                            join d in context.Direcciones on u.ID_Direccion equals d.ID_Direccion into direccionJoin
                            from dir in direccionJoin.DefaultIfEmpty() // Left join for Direcciones
                            join p in context.Provincia on (dir != null ? dir.ID_Provincia : (int?)null) equals p.ID_Provincia into provinciaJoin
                            from prov in provinciaJoin.DefaultIfEmpty() // Left join for Provincia
                            join c in context.Canton on (dir != null ? dir.ID_Canton : (int?)null) equals c.ID_Canton into cantonJoin
                            from cant in cantonJoin.DefaultIfEmpty() // Left join for Canton
                            join dis in context.Distrito on (dir != null ? dir.ID_Distrito : (int?)null) equals dis.ID_Distrito into distritoJoin
                            from dist in distritoJoin.DefaultIfEmpty() // Left join for Distrito
                            where u.ID_Usuario != idUsuario
                            select new UsuarioEnt
                            {
                                ID_Usuario = u.ID_Usuario,
                                Nombre_Identificacion = i.Nombre,
                                Identificacion_Usuario = u.Identificacion_Usuario,
                                Nombre_Usuario = u.Nombre_Usuario,
                                Apellido_Usuario = u.Apellido_Usuario,
                                Correo_Usuario = u.Correo_Usuario,
                                Nombre_Provincia = prov != null ? prov.Nombre : "",
                                Nombre_Canton = cant != null ? cant.Nombre : "",
                                Nombre_Distrito = dist != null ? dist.Nombre : "",
                                Direccion_Exacta = dir != null ? dir.Direccion_Exacta : "",
                                Telefono_Usuario = u.Telefono_Usuario,
                                ID_Estado = u.ID_Estado,
                                ID_Rol = u.ID_Rol,
                            }).ToList();

                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultarUsuariosAdministrador: " + ex.Message);
                return null;
            }
        }

        // Permite al administrador cambiar el estado del cliente (activar o inactivar)
        [HttpPut]
        [Route("ActualizarEstadoUsuario")]
        public string ActualizarEstadoUsuario(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.ActualizarEstadoUsuarioSP(entidad.ID_Usuario);
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ActualizarEstadoUsuario: " + ex.Message);
                return $"Error al actualizar el estado del usuario: {ex.Message}";
            }
        }

        [HttpPut]
        [Route("ActualizarRolUsuario")]
        public string ActualizarRolUsuario(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.ActualizarRolUsuarioSP(entidad.ID_Usuario);
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ActualizarRolUsuario: " + ex.Message);
                return $"Error al actualizar el rol del usuario: {ex.Message}";
            }
        }

        //Devuelve los datos de la entidad basados en la cedula recibida
            [HttpGet]
              [Route("ConsultaClienteEspecifico")]
              public UsuarioEnt ConsultaClienteEspecifico(long q)
              {
                  try
                  {
                      using (var context = new ImportadoraMoyaUlateEntities())
                      {
                          context.Configuration.LazyLoadingEnabled = false;
                    return (from u in context.Usuario
                            join i in context.Identificacion on u.ID_Identificacion equals i.ID_Identificacion
                            join d in context.Direcciones on u.ID_Direccion equals d.ID_Direccion into direccionJoin
                            from dir in direccionJoin.DefaultIfEmpty() // Left join for Direcciones
                            where u.ID_Usuario == q
                            select new UsuarioEnt
                            {
                                ID_Usuario = u.ID_Usuario,
                                Nombre_Identificacion = i.Nombre,
                                Identificacion_Usuario = u.Identificacion_Usuario,
                                Nombre_Usuario = u.Nombre_Usuario,
                                Apellido_Usuario = u.Apellido_Usuario,
                                Correo_Usuario = u.Correo_Usuario,
                                Contrasenna_Usuario = u.Contrasenna_Usuario,
                                ID_Direccion = dir != null ? dir.ID_Direccion : 0,
                                ID_Provincia = dir != null ? dir.ID_Provincia : 0,
                                ID_Canton = dir != null ? dir.ID_Canton : 0,
                                ID_Distrito = dir != null ? dir.ID_Distrito : 0,
                                Direccion_Exacta = dir != null ? dir.Direccion_Exacta : "",
                                Telefono_Usuario = u.Telefono_Usuario,
                                ID_Estado = u.ID_Estado,
                                ID_Rol = u.ID_Rol,
                            }).FirstOrDefault();

                }
            }
                  catch (Exception ex)
                  {
                      log.Add("Error en ConsultaClienteEspecifico: " + ex.Message);
                      return null;
                  }
              }

        [HttpGet]
        [Route("ConsultarProvincias")]
        public List<System.Web.Mvc.SelectListItem> ConsultarProvincias()
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var datos = (from x in context.Provincia
                                 select x).ToList();

                    List<System.Web.Mvc.SelectListItem> provincias = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        provincias.Add(new System.Web.Mvc.SelectListItem
                        {
                            Value = item.ID_Provincia.ToString(),
                            Text = item.Nombre
                        });
                    }

                    return provincias;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultarProvincias: " + ex.Message);
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

        [HttpGet]
        [Route("ConsultarCantones")]
        public List<System.Web.Mvc.SelectListItem> ConsultarCantones()
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var datos = (from x in context.Canton
                                 select x).ToList();

                    List<System.Web.Mvc.SelectListItem> cantones = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        cantones.Add(new System.Web.Mvc.SelectListItem
                        {
                            Value = item.ID_Canton.ToString(),
                            Text = item.Nombre
                        });
                    }

                    return cantones;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultarCantones: " + ex.Message);
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

        [HttpGet]
        [Route("cargarCantones")]
        public List<System.Web.Mvc.SelectListItem> cargarCantones(int q)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var datos = (from x in context.Canton
                                 where x.ID_Provincia == q
                                 select x).ToList();

                    List<System.Web.Mvc.SelectListItem> cantones = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        cantones.Add(new System.Web.Mvc.SelectListItem
                        {
                            Value = item.ID_Canton.ToString(),
                            Text = item.Nombre
                        });
                    }

                    return cantones;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultarCantonesPorProvincia: " + ex.Message);
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

        [HttpGet]
        [Route("ConsultarDistritos")]
        public List<System.Web.Mvc.SelectListItem> ConsultarDistritos()
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var datos = (from x in context.Distrito
                                 select x).ToList();

                    List<System.Web.Mvc.SelectListItem> distritos = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        distritos.Add(new System.Web.Mvc.SelectListItem
                        {
                            Value = item.ID_Distrito.ToString(),
                            Text = item.Nombre
                        });
                    }

                    return distritos;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultarDistritos: " + ex.Message);
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

        [HttpGet]
        [Route("cargarDistritos")]
        public List<System.Web.Mvc.SelectListItem> cargarDistritos(int q)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var datos = (from x in context.Distrito
                                 where x.ID_Canton == q
                                 select x).ToList();

                    List<System.Web.Mvc.SelectListItem> distritos = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        distritos.Add(new System.Web.Mvc.SelectListItem
                        {
                            Value = item.ID_Distrito.ToString(),
                            Text = item.Nombre
                        });
                    }

                    return distritos;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultarDistritosPorCanton: " + ex.Message);
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }


        //Conexion a procedimiento para inactivar al cliente
        [HttpPut]
        [Route("InactivarUsuario")]
        public void InactivarUsuario(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.InactivarUsuarioSP(entidad.ID_Usuario);
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en InactivarUsuario: " + ex.Message);
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
                       context.ActualizarCuentaUsuarioSP(entidad.ID_Usuario, entidad.Nombre_Usuario, entidad.Apellido_Usuario, entidad.Correo_Usuario, entidad.NuevaContrasenna_Usuario, entidad.Telefono_Usuario, entidad.ID_Provincia, entidad.ID_Canton, entidad.ID_Distrito, entidad.Direccion_Exacta);
                       return "OK";
                   }
               }
               catch (Exception ex)
               {
                   log.Add("Error en ActualizarCuentaCliente: " + ex.Message);
                   return string.Empty;
               }
           }

    }
}
