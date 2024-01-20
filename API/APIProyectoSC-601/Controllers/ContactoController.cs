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
    public class ContactoController : ApiController
    {
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
                    return "OK";
                }
                else
                {
                    return string.Empty;
                }
                
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
