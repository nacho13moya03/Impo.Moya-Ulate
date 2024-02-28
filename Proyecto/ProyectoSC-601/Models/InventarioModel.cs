using ProyectoSC_601.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web.Mvc;

namespace ProyectoSC_601.Models
{
    public class InventarioModel
    {
        //Se hace referencia a la ruta del servidor configurada en Web.config
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];

        //Funcion para consultar todos los productos por parte del administrador
        public List<InventarioEnt> ConsultarInventario()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultarInventario";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<InventarioEnt>>().Result;
            }
        }

        public List<InventarioEnt> ConsultarInventarioCatalogo(int categoria)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultarInventarioCatalogo?categoria=" + categoria;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<InventarioEnt>>().Result;
            }
        }

        //Funcion para consultar las categorias
        public List<SelectListItem> ConsultarCategorias()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultarCategorias";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        //Funcion para registrar el producto
        public long RegistrarProducto(InventarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "RegistrarProducto";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<long>().Result;
            }
        }

        //Funcion para actualizar la ruta de la imagen del producto
        public string ActualizarRutaProducto(InventarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ActualizarRutaProducto";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        //Funcion para actualizar el estado del producto
        public string ActualizarEstadoProducto(InventarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ActualizarEstadoProducto";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        //Funcion para eliminar un producto
        public string EliminarProducto(InventarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "EliminarProducto";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        //Funcion para mostrar un los datos de un producto especifico
        public InventarioEnt ConsultaProductoEspecifico(long q)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultaProductoEspecifico?q=" + q;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<InventarioEnt>().Result;
            }
        }

        //Funcion para actualizar los datos de un producto
        public long ActualizarProducto(InventarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ActualizarProducto";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<long>().Result;
            }
        }

        //Funcion para contar el total de recursos del inventario
        public String ContarTotalInventario()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "TotalInventario";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<String>().Result;
            }
        }

        //Funcion para registrar el producto
        public long RegistrarCategoria(InventarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "RegistrarCategoria";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<long>().Result;
            }
        }

        //Funcion para eliminar un producto
        public string EliminarCategoria(InventarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "EliminarCategoria";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }
        public bool VerificarFacturasVinculadas(InventarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "VerificarFacturasVinculadas";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<bool>().Result;
            }
        }


    }
}