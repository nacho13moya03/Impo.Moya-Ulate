using Newtonsoft.Json;
using ProyectoSC_601.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.EnterpriseServices.Internal;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSC_601.Models
{
    public class UsuarioModel
    {
        //Se hace referencia a la ruta del servidor configurada en Web.config
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];

        public List<SelectListItem> ConsultarTiposIdentificaciones()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultarTiposIdentificaciones";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        //Funcion para registrar al cliente
         public string RegistroUsuario(UsuarioEnt entidad)
         {
             using (var client = new HttpClient())
             {
                 var urlApi = rutaServidor + "RegistroUsuario";
                 var jsonData = JsonContent.Create(entidad);
                 var res = client.PostAsync(urlApi, jsonData).Result;
                 return res.Content.ReadFromJsonAsync<string>().Result;
             }
         }
        
        //Funcion para iniciar sesion
        public UsuarioEnt Login(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "Login";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<UsuarioEnt>().Result;
            }
        }

        //Funcion para recuperar cuenta
        public string RecuperarCuentaUsuario(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "RecuperarCuentaUsuario?Correo=" + entidad.Correo_Usuario;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        //Funcion para comprobar si ya existe el correo en otra cuenta
        public string ComprobarCorreoExistenteUsuario(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ComprobarCorreoExistenteUsuario";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        //Funcion para comprobar si ya existe la cedula en otra cuenta
        public string ComprobarCedulaExistente(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ComprobarCedulaExistente";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        //Funcion para consultar todos los clientes por parte del administrador
        public List<UsuarioEnt> ConsultarUsuariosAdministrador(long idUsuario)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultarUsuariosAdministrador?idUsuario=" + idUsuario;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<UsuarioEnt>>().Result;
            }
        }

        //Funcion para cambiar el estado del cliente por parte del administrador
        public string ActualizarEstadoUsuario(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ActualizarEstadoUsuario";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public string ActualizarRolUsuario(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ActualizarRolUsuario";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }
        //Funcion para mostrar los datos del cliente logueado en el perfil
        /* public UsuarioEnt ConsultaClienteEspecifico(long q)
         {
             using (var client = new HttpClient())
             {
                 var urlApi = rutaServidor + "ConsultaClienteEspecifico?q=" + q;
                 var res = client.GetAsync(urlApi).Result;
                 return res.Content.ReadFromJsonAsync<UsuarioEnt>().Result;
             }
         }

         //Funcion para actualizar los datos del cliente desde el perfil
         public string ActualizarCuentaCliente(UsuarioEnt entidad)
         {
             using (var client = new HttpClient())
             {
                 var urlApi = rutaServidor + "ActualizarCuentaCliente";
                 var jsonData = JsonContent.Create(entidad);
                 var res = client.PutAsync(urlApi, jsonData).Result;
                 return res.Content.ReadFromJsonAsync<string>().Result;
             }
         }

         //Funcion para inactivar el cliente
         public void InactivarCliente(UsuarioEnt entidad)
         {
             using (var client = new HttpClient())
             {
                 var urlApi = rutaServidor + "InactivarCliente";
                 var jsonData = JsonContent.Create(entidad);
                 var res = client.PutAsync(urlApi, jsonData).Result;
             }
         }

         

         

         */
    }
}