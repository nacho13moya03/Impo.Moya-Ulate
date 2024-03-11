using ProyectoSC_601.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;

namespace ProyectoSC_601.Models
{
    public class PedidosModel
    {
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];

        public string RegistrarPedido(PedidoEnt entidad)
        {
            using (var client = new HttpClient())
            {
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
                var urlApi = rutaServidor + "ConsultarPedido?idtransaccion=" + idtransaccion;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<PedidoEnt>().Result;
            }
        }

        public List<PedidoEnt> ConsultarPedidos()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultarPedidos";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<PedidoEnt>>().Result;
            }
        }

        public string ActualizarEstadoPedido(PedidoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ActualizarEstadoPedido";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }
    }
}