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
    public class PedidosModel
    {
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];

        public string RegistrarPedido(PedidoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("11166141:60-dayfreetrial"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var urlApi = rutaServidor + "RegistrarPedido";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public PedidoEnt ConsultarPedido(string idtransaccion)
        {
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("11166141:60-dayfreetrial"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var urlApi = rutaServidor + "ConsultarPedido?idtransaccion=" + idtransaccion;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<PedidoEnt>().Result;
            }
        }

        public List<PedidoEnt> ConsultarPedidos()
        {
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("11166141:60-dayfreetrial"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var urlApi = rutaServidor + "ConsultarPedidos";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<PedidoEnt>>().Result;
            }
        }

        public string ActualizarEstadoPedido(PedidoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("11166141:60-dayfreetrial"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var urlApi = rutaServidor + "ActualizarEstadoPedido";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }
    }
}