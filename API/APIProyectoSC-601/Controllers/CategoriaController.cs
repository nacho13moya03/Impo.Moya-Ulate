using APIProyectoSC_601.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIProyectoSC_601.Controllers
{
    public class CategoriaController : ApiController
    {
        private readonly Errores log;

        public CategoriaController()
        {
            string rutaDeLogs = ConfigurationManager.AppSettings["RutaDeLogs"];
            log = new Errores(rutaDeLogs);
        }

        //Devuelve una lista con todos las categorias registradas en la base de datos

        [HttpGet]
        [Route("ConsultarCategoria")]
        public List<Categorias> ConsultarCategoria()
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return context.Categorias.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultarCategoria: " + ex.Message);
                return new List<Categorias>();
            }

        }
        //Conexion a procedimiento para registrar categoria
        [HttpPost]
        [Route("RegistrarCategoria")]
        public string RegistrarCategoria(Categorias categoria)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Categorias.Add(categoria);
                    context.SaveChanges();
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en RegistrarCategoria: " + ex.Message);
                return string.Empty;
            }
        }
        //Actualiza el estado de la categoria en la base de datos
        [HttpPut]
        [Route("ActualizarEstadoCategoria")]
        public string ActualizarEstadoCategoria(Categorias categoria)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var datos = context.Categorias.Where(x => x.ID_Categoria == categoria.ID_Categoria).FirstOrDefault();

                    if (datos != null)
                    {
                        if (datos.Estado_Categoria == 1)
                        {
                            datos.Estado_Categoria = 0;
                        }
                        else
                        {
                            datos.Estado_Categoria = 1;
                        }
                        context.SaveChanges();
                        return "OK";
                    }
                    else
                    {
                        // Puedes manejar el caso en que no se encuentre la categoria.
                        return "Categoria no encontrada";
                    }
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ActualizarEstadoCategoria: " + ex.Message);
                return "Error: " + ex.Message;
            }
        }


        //Elimina el producto en la base de datos
        [HttpPut]
        [Route("EliminarCategorias")]
        public string EliminarCategoria(Categorias categoria)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var categoriaAEliminar = context.Categorias.Find(categoria.ID_Categoria);

                    if (categoriaAEliminar != null)
                    {
                        context.Categorias.Remove(categoriaAEliminar);
                        context.SaveChanges();
                        return "OK";
                    }
                    else
                    {
                        return "Categoria no encontrada";
                    }
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en EliminarProducto: " + ex.Message);
                return "Error: " + ex.Message;
            }
        }


        //Actualiza los datos de la categoria en la base de datos
        [HttpPut]
        [Route("ActualizarCategoria")]
        public long ActualizarCategoria(Categorias categoria)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    var datos = context.Categorias.Where(x => x.ID_Categoria == categoria.ID_Categoria).FirstOrDefault();

                    if (datos != null)
                    {
                        datos.Nombre_Categoria = categoria.Nombre_Categoria;

                        context.SaveChanges();  

                        return categoria.ID_Categoria;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ActualizarCategoria: " + ex.Message);
                return -1;
            }
        }

        //Devuelve los datos de una categoria segun su ID
        [HttpGet]
        [Route("ConsultaCategoriaEspecifica")]
        public Categorias ConsultaCategoriaEspecifica(int q)
        {
            try
            {
                using (var context = new ImportadoraMoyaUlateEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.Categorias
                            where x.ID_Categoria == q
                            select x).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en ConsultaCategoriaEspecifica: " + ex.Message);
                return null;
            }
        }

    }
}
