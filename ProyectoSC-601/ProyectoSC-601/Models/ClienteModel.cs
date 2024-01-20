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
    public class ClienteModel
    {
        //Se hace referencia a la ruta del servidor configurada en Web.config
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];

        //Funcion para registrar al cliente
        public string RegistroCliente(ClienteEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "RegistroCliente";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        //Funcion para iniciar sesion
        public ClienteEnt Login(ClienteEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "Login";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<ClienteEnt>().Result;
            }
        }

        //Funcion para recuperar cuenta
        public string RecuperarCuentaCliente(ClienteEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "RecuperarCuentaCliente?Identificacion=" + entidad.Ced_Cliente;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        //Funcion para mostrar los datos del cliente logueado en el perfil
        public ClienteEnt ConsultaClienteEspecifico(long q)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultaClienteEspecifico?q=" + q;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<ClienteEnt>().Result;
            }
        }

        //Funcion para actualizar los datos del cliente desde el perfil
        public string ActualizarCuentaCliente(ClienteEnt entidad)
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
        public void InactivarCliente(ClienteEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "InactivarCliente";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
            }
        }

        //Funcion para consultar todos los clientes por parte del administrador
        public List<ClienteEnt> ConsultarClientesAdministrador()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultarClientesAdministrador";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<ClienteEnt>>().Result;
            }
        }

        //Funcion para cambiar el estado del cliente por parte del administrador
        public string ActualizarEstadoCliente(ClienteEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ActualizarEstadoCliente";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        //Funcion para comprobar si ya existe el correo en otra cuenta
        public string ComprobarCorreoExistenteCliente(ClienteEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ComprobarCorreoExistenteCliente";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        //Funcion para comprobar si ya existe la cedula en otra cuenta
        public string ComprobarCedulaExistente(ClienteEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ComprobarCedulaExistente";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }
    }
}