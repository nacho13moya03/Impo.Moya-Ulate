using ProyectoSC_601.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace ProyectoSC_601.Models
{
    public class CarritoModel
    {
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];

        public string RegistrarCarrito(CarritoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("11166141:60-dayfreetrial"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

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
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("11166141:60-dayfreetrial"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var urlApi = rutaServidor + "ConsultarCarrito?q=" + q;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<CarritoEnt>>().Result;
            }

        }

        public void EliminarRegistroCarrito(long q)
        {
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("11166141:60-dayfreetrial"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var urlApi = rutaServidor + "EliminarRegistroCarrito?q=" + q;
                var res = client.DeleteAsync(urlApi).Result;
            }
        }

        public string PagarCarrito(CarritoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("11166141:60-dayfreetrial"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var urlApi = rutaServidor + "PagarCarrito";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public long ConsultarFacturaRealizada(long q)
        {
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("11166141:60-dayfreetrial"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var urlApi = rutaServidor + "ConsultarFacturaRealizada?q=" + q;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<long>().Result;
            }
        }

        //De aqui para abajo implementacion de PayPal
        //1era API

        public class PaypalOrder
        {
            public string intent { get; set; }
            public List<PurchaseUnit> purchase_units { get; set; }
            public ApplicationContext application_context { get; set; }

        }


        public class Amount
        {
            public string currency_code { get; set; }
            public string value { get; set; }
        }

        public class PurchaseUnit
        {
            public Amount amount { get; set; }
            public string description { get; set; }
        }

        public class ApplicationContext
        {
            public string brand_name { get; set; }
            public string landing_page { get; set; }
            public string user_action { get; set; }
            public string return_url { get; set; }
            public string cancel_url { get; set; }
        }

    }
}