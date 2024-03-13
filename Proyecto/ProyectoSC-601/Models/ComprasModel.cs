using ProyectoSC_601.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Web.Mvc;

namespace ProyectoSC_601.Models
{
    public class ComprasModel
    {
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];


        /*Esto envía una solicitud HTTP POST a una API para registrar un proveedor, 
        convierte la respuesta a una cadena y la devuelve. */
        public string RegistrarCompra(ComprasEnt entidad)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("11166141:60-dayfreetrial"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                    var urlApi = rutaServidor + "RegistrarCompra";
                    var jsonData = JsonContent.Create(entidad);
                    var res = client.PostAsync(urlApi, jsonData).Result;
                    return res.Content.ReadFromJsonAsync<string>().Result;
                }
            }
            catch (Exception ex)
            {
                return "Error al intentar comunicarse con el servidor: " + ex.Message;
            }
        }


        /*Es una solicitud HTTP GET a una API para obtener la lista de todos los proveedores como objetos ProveedorEnt*/
        public List<ComprasEnt> ConsultaCompras()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("11166141:60-dayfreetrial"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                    var urlApi = rutaServidor + "ConsultaCompras";
                    var res = client.GetAsync(urlApi).Result;

                    return res.Content.ReadFromJsonAsync<List<ComprasEnt>>().Result;
                }
            }
            catch (Exception)
            {
                return new List<ComprasEnt>();
            }
        }



        /*Este código realiza una solicitud HTTP PUT a una API para actualizar la información completa de un proveedor*/
        public string ActualizarCompra(ComprasEnt entidad)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("11166141:60-dayfreetrial"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                    var urlApi = rutaServidor + "ActualizarCompra";
                    var jsonData = JsonContent.Create(entidad);
                    var res = client.PutAsync(urlApi, jsonData).Result;
                    return res.Content.ReadFromJsonAsync<string>().Result;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }



        /*Esto sirve para hacer una solicitud HTTP GET a una API para consultar la información de un proveedor específico. 
        El parámetro q se utiliza para identificar al proveedor y utilizar la variable q para mantener la privacidad de los nombres*/
        public ComprasEnt ConsultaCompra(long q)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("11166141:60-dayfreetrial"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                    var urlApi = rutaServidor + "ConsultaCompra?q=" + q;
                    var res = client.GetAsync(urlApi).Result;
                    return res.Content.ReadFromJsonAsync<ComprasEnt>().Result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }



        /*Esta parte del código realiza una solicitud HTTP DELETE a una API para eliminar un proveedor. 
        Utiliza el ID del proveedor en la URL de la solicitud como en el actualizar y utilizar la variable q para mantener la privacidad de los nombres*/
        public string EliminarCompra(long idCompra)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("11166141:60-dayfreetrial"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                    var urlApi = rutaServidor + $"EliminarCompra?q={idCompra}";
                    var res = client.DeleteAsync(urlApi).Result;

                    if (res.IsSuccessStatusCode)
                    {
                        var respuestaJson = res.Content.ReadAsStringAsync().Result;

                        if (respuestaJson.Contains("OK"))
                        {
                            return "OK";
                        }
                        else
                        {
                            return "Error en la respuesta del servicio.";
                        }
                    }
                    else
                    {
                        return "Error en la solicitud al servicio.";
                    }
                }
            }
            catch (Exception)
            {
                return "Error en la ejecución del servicio.";
            }
        }


        public List<SelectListItem> ConsultarEmpresas()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("11166141:60-dayfreetrial"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                    var urlApi = rutaServidor + "ConsultarEmpresas";
                    var res = client.GetAsync(urlApi).Result;
                    return res.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
                }
            }
            catch (Exception)
            {
                return new List<SelectListItem>();
            }
        }





    }
}