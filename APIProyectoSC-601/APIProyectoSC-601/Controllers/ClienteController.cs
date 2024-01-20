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
    public class ClienteController : ApiController
    {
        //Se crea instancia para usar herramientas necesarias para enviar correo de recuperacion al cliente
        Utilitarios util = new Utilitarios();


        //Conexion a procedimiento para registrar clientes
        [HttpPost]
        [Route("RegistroCliente")]
        public string RegistroCliente(ClienteEnt entidad)
        {
            try
            {
                //Se asgina inicialmente la direccion y telefono como vacio
                entidad.Direccion_Cliente = string.Empty;
                entidad.Tel_Cliente = string.Empty;
                using (var context = new ImportadoraMoyaUlateEntities())
                {

                    context.RegistrarClienteSP(entidad.Ced_Cliente, entidad.Nombre_Cliente, entidad.Apellido_Cliente, entidad.Correo_Cliente, entidad.Contrasenna_Cliente, entidad.Direccion_Cliente, entidad.Tel_Cliente);
                    return "OK";
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        //Conexion a procedimiento para verificar los datos del login y permitir o negar el inicio de sesion
        [HttpPost]
        [Route("Login")]
        public IniciarSesionSP_Result Login(ClienteEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    return context.IniciarSesionSP(entidad.Correo_Cliente, entidad.Contrasenna_Cliente).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        //Se comprueba si existe la cedula ingresada y se envia correo de recuperacion con nombre y contrasenna al cliente
        [HttpGet]
        [Route("RecuperarCuentaCliente")]
        public string RecuperarCuentaCliente(string Identificacion)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var datos = context.RecuperarCuentaClienteSP(Identificacion).FirstOrDefault();

                    if (datos != null)
                    {
                        string rutaArchivo = AppDomain.CurrentDomain.BaseDirectory + "Templates\\Contrasenna.html";
                        string html = File.ReadAllText(rutaArchivo);

                        html = html.Replace("@@Nombre", datos.Nombre_Cliente + " "+datos.Apellido_Cliente);
                        html = html.Replace("@@Contrasenna", datos.Contrasenna_Cliente);

                        util.EnviarCorreo(datos.Correo_Cliente, "Contraseña de Acceso", html);
                        return "OK";
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        //Devuelve los datos de la entidad basados en la cedula recibida
        [HttpGet]
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
            catch (Exception)
            {
                return null;
            }
        }

        //Conexion a procedimiento para actualizar los datos del cliente desde el perfil
        [HttpPut]
        [Route("ActualizarCuentaCliente")]
        public string ActualizarCuentaCliente(ClienteEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.ActualizarCuentaClienteSP(entidad.Ced_Cliente, entidad.Nombre_Cliente, entidad.Apellido_Cliente, entidad.Correo_Cliente, entidad.Direccion_Cliente, entidad.Tel_Cliente, entidad.ID_Cliente);
                    return "OK";
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        //Conexion a procedimiento para inactivar al cliente
        [HttpPut]
        [Route("InactivarCliente")]
        public void InactivarCliente(ClienteEnt entidad)
        {
            using (var context = new ImportadoraMoyaUlateEntities())
            {
                context.InactivarClienteSP(entidad.ID_Cliente);
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
            catch (Exception)
            {
                return null;
            }
        }

        // Permite al administrador cambiar el estado del cliente (activar o inactivar)
        [HttpPut]
        [Route("ActualizarEstadoCliente")]
        public string ActualizarEstadoCliente(ClienteEnt entidad)
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
                return $"Error al actualizar el estado del cliente: {ex.Message}";
            }
        }

        //Verifica si el correo ya existe
        [HttpPost]
        [Route("ComprobarCorreoExistenteCliente")]
        public string ComprobarCorreoExistenteCliente(ClienteEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;

                    // Verificar si el correo existe antes de ejecutar la consulta
                    bool correoExiste = context.Clientes.Any(x => x.Correo_Cliente == entidad.Correo_Cliente && x.ID_Cliente != entidad.ID_Cliente);

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
            catch (Exception)
            {
                return null;
            }
           
        }

        //Verifica si la cedula ya existe
        [HttpPost]
        [Route("ComprobarCedulaExistente")]
        public string ComprobarCedulaExistente(ClienteEnt entidad)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;

                    // Verificar si la cedula existe antes de ejecutar la consulta
                    bool cedulaExiste = context.Clientes.Any(x => x.Ced_Cliente == entidad.Ced_Cliente && x.ID_Cliente != entidad.ID_Cliente);

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
            catch (Exception)
            {
                return null;
            }

        }

        //Funcion para contar la cantidad de clientes registrados en la pagina
        [HttpGet]
        [Route("ContarClientes")]
        public int ContarClientes()
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return context.Clientes.Count(x => x.Rol_Cliente == 2);
                }
            }
            catch (Exception)
            {
                return 0; 
            }
        }

    }
}
