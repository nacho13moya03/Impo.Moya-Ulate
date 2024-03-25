using APIProyectoSC_601.Entities;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Web.Http;

namespace APIProyectoSC_601.Controllers
{
    public class ContactoController : ApiController
    {
        private readonly Errores log;
        private readonly LogExitos logExitos;

        public string RutaErrores { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["RutaErrores"];
        public string RutaExitos { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["RutaExitos"];

        public ContactoController()
        {
            string rutaErrores = RutaErrores;
            string rutaExitos = RutaExitos;


            log = new Errores(rutaErrores);
            logExitos = new LogExitos(rutaExitos);
        }

        //Se crea instancia para usar herramientas necesarias para enviar correo de contactenos
        Utilitarios util = new Utilitarios();

        //Conexion para enviar la informacion al correo de la empresa
        [HttpPost]
        [Route("EnviarInformacion")]
        public string EnviarInformacion(InfoContacto entidad)
        {
            try
            {

                if (entidad != null)
                {
                    string rutaArchivo = AppDomain.CurrentDomain.BaseDirectory + "Templates\\Contacto.html";
                    string html = File.ReadAllText(rutaArchivo);

                    html = html.Replace("@@Nombre", entidad.Nombre_Completo);
                    html = html.Replace("@@Asunto", entidad.Asunto);
                    html = html.Replace("@@Mensaje", entidad.Mensaje);
                    html = html.Replace("@@correo", entidad.Correo);

                    util.EnviarCorreo("angelicavalerin13@gmail.com", "Solicitud de Contacto", html);

                    logExitos.Add("EnviarInformacion", "Correo de contacto enviado exitosamente");
                    return "OK";
                }
                else
                {
                    return string.Empty;
                }

            }
            catch (Exception ex)
            {
                log.Add("Error en EnviarInformacion: " + ex.Message);
                return string.Empty;
            }
        }
    }
}
