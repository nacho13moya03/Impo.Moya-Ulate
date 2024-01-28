using ProyectoSC_601.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSC_601.Models
{
    public class EmpresaModel
    {
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];


        /*Esto envía una solicitud HTTP POST a una API para registrar una empresa, 
         convierte la respuesta a una cadena y la devuelve. */
        public string RegistrarEmpresa(EmpresaEnt entidad)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var urlApi = rutaServidor + "RegistrarEmpresa";
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
        public List<EmpresaEnt> ConsultaEmpresas()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var urlApi = rutaServidor + "ConsultaEmpresas";
                    var res = client.GetAsync(urlApi).Result;
                    return res.Content.ReadFromJsonAsync<List<EmpresaEnt>>().Result;
                }
            }
            catch (Exception)
            {
                return new List<EmpresaEnt>();
            }
        }


        /*Esto sirve para hacer una solicitud HTTP GET a una API para consultar la información de una empresa específico. 
        El parámetro q se utiliza para identificar a la empresa y utilizar la variable q para mantener la privacidad de los nombres*/
        public EmpresaEnt ConsultaEmpresa(long q)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var urlApi = rutaServidor + "ConsultaEmpresa?q=" + q;
                    var res = client.GetAsync(urlApi).Result;
                    return res.Content.ReadFromJsonAsync<EmpresaEnt>().Result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        /*Este código realiza una solicitud HTTP PUT a una API para actualizar la información completa de una empresa*/
        public string ActualizarEmpresa(EmpresaEnt entidad)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var urlApi = rutaServidor + "ActualizarEmpresa";
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



        /*Esta parte del código realiza una solicitud HTTP DELETE a una API para eliminar una empresa. 
        Utiliza el ID de la empresa en la URL de la solicitud como en el actualizar y utilizar la variable q para mantener la privacidad de los nombres*/
        public string EliminarEmpresa(long idEmpresa)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var urlApi = rutaServidor + $"EliminarEmpresa?q={idEmpresa}";
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





    }
}