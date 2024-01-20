using ProyectoSC_601.Entities;
using ProyectoSC_601.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSC_601.Controllers
{
    public class ClienteController : Controller
    {
        
        ClienteModel modelCliente = new ClienteModel();

        [HttpGet]
        public ActionResult RegistroCliente()
        {
            return View();
        }

        //Se llama al modelo para registrar al cliente y se devuelven mensajes de exito o error
        [HttpPost]
        public ActionResult RegistroCliente(ClienteEnt entidad)
        {
            string cedulaExistente = modelCliente.ComprobarCedulaExistente(entidad);
            if(cedulaExistente == "Existe")
            {
                ViewBag.MensajeNoExitoso = "El usuario ya está registrado";
                return View();
            }
            else { 
                string correoExistente = modelCliente.ComprobarCorreoExistenteCliente(entidad);
                if (correoExistente == "Existe")
                {
                    ViewBag.MensajeNoExitoso = "Ese correo está asociado a otra cuenta";
                    return View();
                } else {
                    string respuesta = modelCliente.RegistroCliente(entidad);

                    if (respuesta == "OK")
                    {
                        ViewBag.MensajeExitoso = "La información se ha registrado exitosamente";
                        return View();
                    }
                    else
                    {
                        ViewBag.MensajeNoExitoso = "No se ha podido registrar la información";
                        return View();
                    }
                }
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // Se llama al modelo para iniciar sesion y redirije a las paginas correspondientes basado en el rol 
        [HttpPost]
        public ActionResult Login(ClienteEnt entidad)
        {
            var respuesta = modelCliente.Login(entidad);

            if (respuesta != null && respuesta.Rol_Cliente==2)
            {
                Session["ID_Cliente"] = respuesta.ID_Cliente;
                return RedirectToAction("Index", "Home");
            }
            else if (respuesta != null && respuesta.Rol_Cliente==1)
            {
                 Session["ID_Cliente"] = respuesta.ID_Cliente;
                return RedirectToAction("IndexAdmin", "Home");

            }
            else
            {
                ViewBag.MensajeNoExitoso = "Credenciales Inválidos";
                return View();
            }
        }

        [HttpGet]
        public ActionResult RecuperarCuentaCliente()
        {
            return View();
        }


        //Se llama al modelo para recuperar la cuenta, si es exitoso lo redirije al login y si no muestra mensaje de error
        [HttpPost]
        public ActionResult RecuperarCuentaCliente(ClienteEnt entidad)
        {
            string respuesta = modelCliente.RecuperarCuentaCliente(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("Login", "Cliente");
            }
            else
            {
                ViewBag.MensajeNoExitoso = "No se ha podido recuperar su información";
                return View();
            }
        }

        //Devuelve la vista de perfil con los datos del cliente
        [HttpGet]
        public ActionResult PerfilCliente()
        {
            long q = long.Parse(Session["ID_Cliente"].ToString());
            var datos = modelCliente.ConsultaClienteEspecifico(q);
            Session["ID_Cliente"] = datos.ID_Cliente;
            return View(datos);
        }

        //Actualiza los datos del cliente
        [HttpPost]
        public ActionResult PerfilCliente(ClienteEnt entidad)
        {
            string cedulaExistente = modelCliente.ComprobarCedulaExistente(entidad);
            if (cedulaExistente == "Existe")
            {
                ViewBag.MensajeNoExitoso = "El usuario ya está registrado";
                long q = long.Parse(Session["ID_Cliente"].ToString());
                var datos = modelCliente.ConsultaClienteEspecifico(q);
                Session["ID_Cliente"] = datos.ID_Cliente;
                return View(datos);
            }
            else
            {
                string correoExistente = modelCliente.ComprobarCorreoExistenteCliente(entidad);
                if (correoExistente == "Existe")
                {
                    ViewBag.MensajeNoExitoso = "Ese correo está asociado a otra cuenta";
                    long q = long.Parse(Session["ID_Cliente"].ToString());
                    var datos = modelCliente.ConsultaClienteEspecifico(q);
                    Session["ID_Cliente"] = datos.ID_Cliente;
                    return View(datos);
                }
                else
                {
                    string respuesta = modelCliente.ActualizarCuentaCliente(entidad);

                    if (respuesta == "OK")
                    {
                        return RedirectToAction("PerfilCliente", "Cliente");
                    }
                    else
                    {
                        ViewBag.MensajeNoExitoso = "No se ha podido actualizar su información";
                        return View();
                    }
                }
            }
        }

        //Inactiva el usuario segun el id del cliente recibido
        [HttpGet]
        public ActionResult InactivarCliente(long q)
        {
            var entidad = new ClienteEnt();
            entidad.ID_Cliente = q;
            modelCliente.InactivarCliente(entidad);
            return RedirectToAction("CerrarSesionCliente", "Cliente");

        }

         //Limpia los datos de la sesion y lo redirije al index
        [HttpGet]
        public ActionResult CerrarSesionCliente()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        //Muestra los datos de todos los clientes en el administrador
        [HttpGet]
        public ActionResult GestionClientes()
        {
            var datos = modelCliente.ConsultarClientesAdministrador();
            return View(datos);
        }

        //Cambia el estado del cliente desde el admnistrador
        [HttpGet]
        public ActionResult ActualizarEstadoCliente(long q)
        {
            var entidad = new ClienteEnt();
            entidad.ID_Cliente = q;

            string respuesta = modelCliente.ActualizarEstadoCliente(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("GestionClientes", "Cliente");
            }
            else
            {
                ViewBag.Mensaje = "No se ha podido cambiar el estado del cliente";
                return View();
            }
        }
    }
}