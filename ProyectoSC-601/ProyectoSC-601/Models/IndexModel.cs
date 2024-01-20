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
    public class IndexModel
    {
        //Se hace referencia a la ruta del servidor configurada en Web.config
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];

       
        //Funcion para enviar la informacion de contactenos
        public string EnviarInformacion(InfoIndex entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "EnviarInformacion";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        //Funcion para contar la cantidad de clientes y mostrarla en el inicio
        public int ContarClientes()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ContarClientes";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        public int ContarVentas()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ContarVentas";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<int>().Result;
            }
        }

    }
}