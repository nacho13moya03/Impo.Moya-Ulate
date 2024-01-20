using ProyectoSC_601.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;

namespace ProyectoSC_601.Models
{
    public class FacturacionModel
    {
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];
        public List<FacturaEnt> ConsultaFacturasCliente(long q)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultaFacturasCliente?q=" + q;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<FacturaEnt>>().Result;
            }
        }

        public List<FacturaEnt> ConsultaFacturasAdmin()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultaFacturasAdmin";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<FacturaEnt>>().Result;
            }
        }

        public List<FacturaEnt> ConsultaDetalleFactura(long q)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultaDetalleFactura?q=" + q;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<FacturaEnt>>().Result;
            }
        }

        public string ConsultarDatosEnviarCorreo(long q)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultarDatosEnviarCorreo?q=" + q;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }
    }
}