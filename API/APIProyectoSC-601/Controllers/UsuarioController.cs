using APIProyectoSC_601.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace APIProyectoSC_601.Controllers
{
    public class UsuarioController : ApiController
    {

        private readonly Errores log;
        private readonly LogExitos logExitos;
        Seguridad seguridad = new Seguridad();

        public string RutaErrores { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["RutaErrores"];
        public string RutaExitos { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["RutaExitos"];


        public UsuarioController()
        {
            string rutaErrores = RutaErrores;
            string rutaExitos = RutaExitos;


            log = new Errores(rutaErrores);
            logExitos = new LogExitos(rutaExitos);
        }

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
                    logExitos.Add("ConsultarTiposIdentificaciones", "Se consultaron los tipos de identificaciones exitosamente.");
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

                    context.RegistrarUsuarioSP(entidad.ID_Identificacion, entidad.Identificacion_Usuario, entidad.Nombre_Usuario, entidad.Apellido_Usuario, entidad.Correo_Usuario, entidad.Contrasenna_Usuario, entidad.Telefono_Usuario);
                    logExitos.Add("RegistroUsuario", "Se registró un nuevo usuario exitosamente.");
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
                    var temporal = context.ObtenerTemporal(entidad.Correo_Usuario).FirstOrDefault();

                    if (temporal == 1)
                    {
                        entidad.Contrasenna_Usuario = seguridad.Decrypt(entidad.Contrasenna_Usuario);
                    }
                    var resultado = context.IniciarSesionSP(entidad.Correo_Usuario, entidad.Contrasenna_Usuario).FirstOrDefault();

                    if (resultado != null)
                    {
                        logExitos.Add("Login", $"Inicio de sesión exitoso para el usuario con correo: {entidad.Correo_Usuario}");
                    }

                    else
                    {
                        logExitos.Add("Login", $"Inicio de sesión fallido para el usuario con correo: {entidad.Correo_Usuario}");
                    }

                    return resultado;
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

                        html = html.Replace("@@Nombre", datos.Nombre_Usuario + " " + datos.Apellido_Usuario);
                        html = html.Replace("@@Contrasenna", datos.Contrasenna_Usuario);

                        util.EnviarCorreo(datos.Correo_Usuario, "Contraseña de Acceso", html);
                        logExitos.Add("RecuperarCuentaUsuario", $"Se envió correo de recuperación a {datos.Correo_Usuario}");
                        return "OK";
                    }
                    else
                    {
                        logExitos.Add("RecuperarCuentaUsuario", $"Correo no encontrado: {Correo}");
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
                    int cantidadUsuarios = context.Usuario.Count(x => x.ID_Rol == 2);

                    logExitos.Add("ContarUsuarios", $"Se contaron {cantidadUsuarios} usuarios registrados");

                    return cantidadUsuarios;
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
                        logExitos.Add("ComprobarCorreoExistenteUsuario", $"El correo {entidad.Correo_Usuario} ya existe.");
                        return "Existe";
                    }
                    else
                    {
                        logExitos.Add("ComprobarCorreoExistenteUsuario", $"El correo {entidad.Correo_Usuario} no existe.");
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
                        logExitos.Add("ComprobarCedulaExistente", $"La cédula {entidad.Identificacion_Usuario} ya existe.");
                        return "Existe";
                    }
                    else
                    {
                        logExitos.Add("ComprobarCedulaExistente", $"La cédula {entidad.Identificacion_Usuario} no existe.");
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
                    var usuarios = (from u in context.Usuario
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

                    if (usuarios != null && usuarios.Any())
                    {
                        logExitos.Add("ConsultarUsuariosAdministrador", "Consulta de usuarios administradores exitosa.");
                    }
                    else
                    {
                        logExitos.Add("ConsultarUsuariosAdministrador", "No se encontraron usuarios administradores.");
                    }

                    return usuarios;

                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultarUsuariosAdministrador: " + ex.Message);
                return null;
            }
        }


        //Devuelve todos los clientes registrados, solo rol de usuario
        [HttpGet]
        [Route("GestionDireccion")]
        public UsuarioEnt GestionDireccion(long q)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var usuario = (from u in context.Usuario
                                   join d in context.Direcciones on u.ID_Direccion equals d.ID_Direccion into direccionJoin
                                   from dir in direccionJoin.DefaultIfEmpty() // Left join for Direcciones
                                   join p in context.Provincia on (dir != null ? dir.ID_Provincia : (int?)null) equals p.ID_Provincia into provinciaJoin
                                   from prov in provinciaJoin.DefaultIfEmpty() // Left join for Provincia
                                   join c in context.Canton on (dir != null ? dir.ID_Canton : (int?)null) equals c.ID_Canton into cantonJoin
                                   from cant in cantonJoin.DefaultIfEmpty() // Left join for Canton
                                   join dis in context.Distrito on (dir != null ? dir.ID_Distrito : (int?)null) equals dis.ID_Distrito into distritoJoin
                                   from dist in distritoJoin.DefaultIfEmpty() // Left join for Distrito
                                   where u.ID_Usuario == q
                                   select new UsuarioEnt
                                   {
                                       ID_Usuario = u.ID_Usuario,
                                       Identificacion_Usuario = u.Identificacion_Usuario,
                                       Nombre_Provincia = prov != null ? prov.Nombre : "",
                                       Nombre_Canton = cant != null ? cant.Nombre : "",
                                       Nombre_Distrito = dist != null ? dist.Nombre : "",
                                       Direccion_Exacta = dir != null ? dir.Direccion_Exacta : "",

                                   }).FirstOrDefault();

                    if (usuario != null)
                    {
                        logExitos.Add("ConsultarDireccionUsuario", "Consulta de dirección de usuario exitosa.");
                    }
                    else
                    {
                        logExitos.Add("ConsultarDireccionUsuario", "No se encontró dirección de usuario.");
                    }

                    return usuario;

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
                    logExitos.Add("ActualizarEstadoUsuario", $"Estado del usuario con ID {entidad.ID_Usuario} actualizado exitosamente.");
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
                    logExitos.Add("ActualizarRolUsuario", $"Rol del usuario con ID {entidad.ID_Usuario} actualizado exitosamente.");
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
                    var usuario = (from u in context.Usuario
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

                    if (usuario != null)
                    {
                        log.Add("Consulta exitosa para el usuario con ID: " + q);
                    }

                    return usuario;

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
                    log.Add("Consulta de provincias exitosa.");
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

                    log.Add("Consulta de cantones exitosa.");
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
                    log.Add("Carga de cantones exitosa.");
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

                    log.Add("Consulta de distritos exitosa.");
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
                    log.Add("Carga de distritos exitosa.");
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
                    log.Add($"Usuario con ID {entidad.ID_Usuario} inactivado exitosamente.");
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
                    log.Add($"Datos del cliente con ID {entidad.ID_Usuario} actualizados exitosamente.");
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ActualizarCuentaCliente: " + ex.Message);
                return string.Empty;
            }
        }

       
        [HttpPut]
        [Route("ActualizarContrasenaTemporal")]
        public string ActualizarContrasenaTemporal(Usuario usuario)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var datos = context.Usuario.Where(x => x.ID_Usuario == usuario.ID_Usuario).FirstOrDefault();

                    if (datos != null)
                    {
                        datos.Contrasenna_Usuario = usuario.Contrasenna_Usuario;
                        datos.C_esTemporal = 0;
                      
                        context.SaveChanges();
                        logExitos.Add("ActualizarContrasenaTemporal", $"Se actualizó exitosamente la contraseña del usuario con ID {usuario.ID_Usuario}.");
                        return "Ok";
                    }
                    else
                    {
                        log.Add("Error en ActualizarContrasenaTemporal: Usuario no encontrado.");
                        return string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ActualizarContrasenaTemporal: " + ex.Message);
                return string.Empty;
            }
        }


    }
}
