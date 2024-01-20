﻿using Newtonsoft.Json;
using ProyectoSC_601.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.EnterpriseServices.Internal;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web.Mvc;

namespace ProyectoSC_601.Models
{
    public class CarritoModel
    {
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];

        public string RegistrarCarrito(CarritoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "RegistrarCarrito";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public List<CarritoEnt> ConsultarCarrito(long q)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultarCarrito?q=" + q;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<CarritoEnt>>().Result;
            }

        }

        public void EliminarRegistroCarrito(long q)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "EliminarRegistroCarrito?q=" + q;
                var res = client.DeleteAsync(urlApi).Result;
            }
        }

        public string PagarCarrito(CarritoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "PagarCarrito";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }
    }
}