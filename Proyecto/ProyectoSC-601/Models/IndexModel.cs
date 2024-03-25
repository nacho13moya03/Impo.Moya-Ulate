using ProyectoSC_601.Entities;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace ProyectoSC_601.Models
{
    public class IndexModel
    {
        //Se hace referencia a la ruta del servidor configurada en Web.config
        public string rutaServidor { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["RutaApi"];
        public string  CredentialsSmarter { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["Credentials"];
        public string HeaderlsSmarter { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["AuthorizationHeader"];


        public int ObtenerCantidadProductosEnCarrito(long ID_Usuario)
        {
            using (var client = new HttpClient())
            {
                var credentials = CredentialsSmarter;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var urlApi = rutaServidor + "ObtenerCantidadProductosEnCarrito?ID_Usuario=" + ID_Usuario;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<int>().Result;
            }
        }



        //Funcion para enviar la informacion de contactenos
        public string EnviarInformacion(InfoIndex entidad)
        {
            using (var client = new HttpClient())
            {
                var credentials = CredentialsSmarter;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var urlApi = rutaServidor + "EnviarInformacion";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        //Funcion para contar la cantidad de clientes y mostrarla en el inicio
        public int ContarUsuarios()
        {
            using (var client = new HttpClient())
            {
                var credentials = CredentialsSmarter;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var urlApi = rutaServidor + "ContarUsuarios";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        public int ContarVentas()
        {
            using (var client = new HttpClient())
            {
                var credentials = CredentialsSmarter;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var urlApi = rutaServidor + "ContarVentas";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<int>().Result;
            }
        }

    }
}