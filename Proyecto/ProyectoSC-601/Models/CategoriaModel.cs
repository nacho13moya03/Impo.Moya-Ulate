using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;
using ProyectoSC_601.Entities;
using System.Configuration;
using System.Web.Mvc;

namespace ProyectoSC_601.Models
{
    public class CategoriaModel
    {
        //Se hace referencia a la ruta del servidor configurada en Web.config
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];

        //Funcion para consultar todas las categorias por parte del administrador
        public List<CategoriaEnt> ConsultarCategoria()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultarCategoria";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<CategoriaEnt>>().Result;
            }
        }

        //Funcion para registrar la categoria
        public string RegistrarCategoria(CategoriaEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "RegistrarCategoria";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        //Funcion para actualizar el estado de la categoria
        public string ActualizarEstadoCategoria(CategoriaEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ActualizarEstadoCategoria";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }
        //Funcion para mostrar un los datos de una categoria especifica
        public CategoriaEnt ConsultaCategoriaEspecifica(long q)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultaCategoriaEspecifica?q=" + q;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<CategoriaEnt>().Result;
            }
        }

        public bool VerificarProductosVinculados(CategoriaEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "VerificarProductosVinculados";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<bool>().Result;
            }
        }



        //Funcion para eliminar una categoria
        public string EliminarCategoria(CategoriaEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "EliminarCategorias";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        //Funcion para actualizar los datos de un producto
        public long ActualizarCategoria(CategoriaEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ActualizarCategoria";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<long>().Result;
            }
        }
    }
}