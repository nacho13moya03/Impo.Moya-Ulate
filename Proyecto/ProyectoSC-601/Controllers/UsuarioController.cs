using ProyectoSC_601.Entities;
using ProyectoSC_601.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSC_601.Controllers
{
    public class UsuarioController : Controller
    {
        
        UsuarioModel modelUsuario = new UsuarioModel();

        [HttpGet]
        public ActionResult RegistroUsuario()
        {
            ViewBag.Identificaciones = modelUsuario.ConsultarTiposIdentificaciones();
            return View();
        }

        //Se llama al modelo para registrar al cliente y se devuelven mensajes de exito o error
       [HttpPost]
        public ActionResult RegistroUsuario(UsuarioEnt entidad)
        {
            ModelState.Remove("NuevaContrasenna_Usuario");
            ModelState.Remove("ID_Provincia");
            ModelState.Remove("ID_Canton");
            ModelState.Remove("ID_Distrito");
            ModelState.Remove("Direccion_Exacta");
            ModelState.Remove("Telefono_Usuario");
            if (ModelState.IsValid)
            {
                string cedulaExistente = modelUsuario.ComprobarCedulaExistente(entidad);
                if(cedulaExistente == "Existe")
                {
                    ViewBag.MensajeNoExitoso = "El usuario ya está registrado";
                    ViewBag.Identificaciones = modelUsuario.ConsultarTiposIdentificaciones();
                    return View();
                }
                else { 
                    string correoExistente = modelUsuario.ComprobarCorreoExistenteUsuario(entidad);
                    if (correoExistente == "Existe")
                    {
                        ViewBag.MensajeNoExitoso = "Ese correo está asociado a otra cuenta";
                        ViewBag.Identificaciones = modelUsuario.ConsultarTiposIdentificaciones();
                        return View();
                    } else {
                        string respuesta = modelUsuario.RegistroUsuario(entidad);

                        if (respuesta == "OK")
                        {
                            ModelState.Clear();
                            ViewBag.MensajeExitoso = "La información se ha registrado exitosamente";
                            ViewBag.Identificaciones = modelUsuario.ConsultarTiposIdentificaciones();
                            return View();
                        }
                        else
                        {
                            ViewBag.MensajeNoExitoso = "No se ha podido registrar la información";
                            ViewBag.Identificaciones = modelUsuario.ConsultarTiposIdentificaciones();
                            return View();
                        }
                    }
                }
            }
            else
                {
                ViewBag.Identificaciones = modelUsuario.ConsultarTiposIdentificaciones();
                return View(entidad);
                }
            }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // Se llama al modelo para iniciar sesion y redirije a las paginas correspondientes basado en el rol 
        [HttpPost]
        public ActionResult Login(UsuarioEnt entidad)
        {
            ModelState.Remove("Identificacion_Usuario");
            ModelState.Remove("Nombre_Usuario");
            ModelState.Remove("Apellido_Usuario");
            ModelState.Remove("NuevaContrasenna_Usuario");
            ModelState.Remove("ID_Provincia");
            ModelState.Remove("ID_Canton");
            ModelState.Remove("ID_Distrito");
            ModelState.Remove("Direccion_Exacta");
            ModelState.Remove("Telefono_Usuario");
            if (ModelState.IsValid)  
            {
               
                    var respuesta = modelUsuario.Login(entidad);

                if (respuesta != null && respuesta.ID_Rol == 2)
                {
                    Session["ID_Usuario"] = respuesta.ID_Usuario;
                    return RedirectToAction("Index", "Home");
                }
                else if (respuesta != null && respuesta.ID_Rol == 1)
                {
                    Session["ID_Usuario"] = respuesta.ID_Usuario;
                    return RedirectToAction("IndexAdmin", "Home");

                }
                else
                {
                    ViewBag.MensajeNoExitoso = "Credenciales Inválidos";
                    return View();
                }
            }
            
            else { 
                return View(entidad);
            }
        }

        [HttpGet]
        public ActionResult RecuperarCuentaUsuario()
        {
            return View();
        }


        //Se llama al modelo para recuperar la cuenta, si es exitoso lo redirije al login y si no muestra mensaje de error
        [HttpPost]
        public ActionResult RecuperarCuentaUsuario(UsuarioEnt entidad)
        {
            ModelState.Remove("Nombre_Usuario");
            ModelState.Remove("Apellido_Usuario");
            ModelState.Remove("Identificacion_Usuario");  
            ModelState.Remove("Contrasenna_Usuario");
            ModelState.Remove("NuevaContrasenna_Usuario");
            ModelState.Remove("ID_Provincia");
            ModelState.Remove("ID_Canton");
            ModelState.Remove("ID_Distrito");
            ModelState.Remove("Direccion_Exacta");
            ModelState.Remove("Telefono_Usuario");

            if (ModelState.IsValid)
            {
                string respuesta = modelUsuario.RecuperarCuentaUsuario(entidad);

                if (respuesta == "OK")
                {
                    return RedirectToAction("Login", "Usuario");
                }
                else
                {
                    ViewBag.MensajeNoExitoso = "No se ha podido recuperar su información";
                    return View();
                }

            }
            else
            {
                return View(entidad);
            }
        }

        //Muestra los datos de todos los clientes en el administrador
        [HttpGet]
        public ActionResult GestionUsuarios()
        {
            long idUsuario = long.Parse(Session["ID_Usuario"].ToString());
            var datos = modelUsuario.ConsultarUsuariosAdministrador(idUsuario);
            return View(datos);
        }

        public ActionResult cargarCantones(int idProvincia)
        {
            var cantones = modelUsuario.cargarCantones(idProvincia);
            return Json(cantones, JsonRequestBehavior.AllowGet);
        }

        public ActionResult cargarDistritos(int idCanton)
        {
            var distritos = modelUsuario.cargarDistritos(idCanton);
            return Json(distritos, JsonRequestBehavior.AllowGet);
        }

        //Devuelve la vista de perfil con los datos del cliente
        [HttpGet]
          public ActionResult PerfilCliente()
          {
                long q = long.Parse(Session["ID_Usuario"].ToString());
                var datos = modelUsuario.ConsultaClienteEspecifico(q);
                Session["ID_Usuario"] = datos.ID_Usuario;
                ViewBag.Provincias = modelUsuario.ConsultarProvincias();
                ViewBag.Cantones = "";
;                ViewBag.Distritos = "";
                return View(datos);
          }

      

        //Actualiza los datos del cliente
        [HttpPost]
         public ActionResult PerfilCliente(UsuarioEnt entidad)
         {
            if (ModelState.IsValid)
            {

                string correoExistente = modelUsuario.ComprobarCorreoExistenteUsuario(entidad);
                 if (correoExistente == "Existe")
                 {
                     ViewBag.MensajeNoExitoso = "Ese correo está asociado a otra cuenta";
                     long q = long.Parse(Session["ID_Usuario"].ToString());
                     var datos = modelUsuario.ConsultaClienteEspecifico(q);
                     Session["ID_Usuario"] = datos.ID_Usuario;
                     return View(datos);
                 }
                 else
                 {
                    if(entidad.NuevaContrasenna_Usuario==null || entidad.NuevaContrasenna_Usuario.Length == 0)
                    {
                        entidad.NuevaContrasenna_Usuario = entidad.Contrasenna_Usuario;
                    }

                    if (entidad.Apellido_Usuario == null || entidad.Apellido_Usuario.Length == 0)
                    {
                        entidad.Apellido_Usuario = string.Empty;
                    }

                string respuesta = modelUsuario.ActualizarCuentaCliente(entidad);

                    if (respuesta == "OK")
                    {
                        return RedirectToAction("PerfilCliente", "Usuario");
                    }
                    else
                    {
                        ViewBag.MensajeNoExitoso = "No se ha podido actualizar su información";
                        return View();
                    }

                }
            }
            else
            {
                return View(entidad);
            }

        }

        //Inactiva el usuario segun el id del cliente recibido
        [HttpGet]
          public ActionResult InactivarUsuario(long q)
          {
              var entidad = new UsuarioEnt();
              entidad.ID_Usuario = q;
              modelUsuario.InactivarUsuario(entidad);
              return RedirectToAction("CerrarSesionUsuario", "Usuario");

          }
        
        //Limpia los datos de la sesion y lo redirije al index
        [HttpGet]
        public ActionResult CerrarSesionUsuario()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        

      //Cambia el estado del cliente desde el admnistrador
        [HttpGet]
        public ActionResult ActualizarEstadoUsuario(long q)
        {
            var entidad = new UsuarioEnt();
            entidad.ID_Usuario = q;

            string respuesta = modelUsuario.ActualizarEstadoUsuario(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("GestionUsuarios", "Usuario");
            }
            else
            {
                ViewBag.Mensaje = "No se ha podido cambiar el estado del usuario";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ActualizarRolUsuario(long q)
        {
            var entidad = new UsuarioEnt();
            entidad.ID_Usuario = q;

            string respuesta = modelUsuario.ActualizarRolUsuario(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("GestionUsuarios", "Usuario");
            }
            else
            {
                ViewBag.Mensaje = "No se ha podido cambiar el rol del usuario";
                return View();
            }
        }
    }
}