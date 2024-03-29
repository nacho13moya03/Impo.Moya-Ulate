using ProyectoSC_601.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace APIProyectoSC_601.Controllers
{
    public class CategoriaController : ApiController
    {
        private readonly Errores log;
        private readonly LogExitos logExitos;

        public string RutaErrores { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["RutaErrores"];
        public string RutaExitos { get; } = ((NameValueCollection)ConfigurationManager.GetSection("secureAppSettings"))["RutaExitos"];

        public CategoriaController()
        {
            string rutaErrores = RutaErrores;
            string rutaExitos = RutaExitos;


            log = new Errores(rutaErrores);
            logExitos = new LogExitos(rutaExitos);
        }

        //Devuelve una lista con todos las categorias registradas en la base de datos

        [HttpGet]
        [Route("ConsultarCategoria")]
        public List<Categorias> ConsultarCategoria()
        {
            try
            {
                using (var context = new db_aa61bd_impomyuEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var categorias = context.Categorias.ToList();
                    logExitos.Add("ConsultarCategoria", "Consulta de categorías realizada exitosamente");
                    return categorias;

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
                using (var context = new db_aa61bd_impomyuEntities())
                {
                    // Verificar si ya existe una categoría con el mismo nombre
                    var categoriaExistente = context.Categorias.FirstOrDefault(c => c.Nombre_Categoria == categoria.Nombre_Categoria);

                    // Si la categoría no existe, la registramos
                    if (categoriaExistente == null)
                    {
                        context.Categorias.Add(categoria);
                        context.SaveChanges();
                        logExitos.Add("RegistrarCategoria", $"Registro de categoría '{categoria.Nombre_Categoria}' realizado exitosamente");
                        return "OK";
                    }
                    else
                    {
                        // Devolvemos vacío si la categoría ya existe
                        return "Repetida";
                    }
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
                using (var context = new db_aa61bd_impomyuEntities())
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
                        logExitos.Add("ActualizarEstadoCategoria", $"Estado de categoría '{datos.Nombre_Categoria}' actualizado exitosamente. Nuevo estado: {datos.Estado_Categoria}");
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
                using (var context = new db_aa61bd_impomyuEntities())
                {
                    var categoriaAEliminar = context.Categorias.Find(categoria.ID_Categoria);

                    if (categoriaAEliminar != null)
                    {
                        context.Categorias.Remove(categoriaAEliminar);
                        context.SaveChanges();
                        logExitos.Add("EliminarCategoria", $"Categoría '{categoriaAEliminar.Nombre_Categoria}' eliminada exitosamente.");
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



        [HttpPost]
        [Route("VerificarProductosVinculados")]
        public bool VerificarProductosVinculados(CategoriaEnt entidad)
        {
            try
            {
                using (var context = new db_aa61bd_impomyuEntities())
                {
                    bool productosVinculados = context.Producto.Any(p => p.ID_Categoria == entidad.ID_Categoria);

                    if (!productosVinculados)
                    {
                        logExitos.Add("VerificarProductosVinculados", $"No hay productos vinculados a la categoría con ID {entidad.ID_Categoria}.");
                    }

                    return productosVinculados;
                }
            }
            catch (Exception ex)
            {
                log.Add("Error en VerificarProductosVinculados: " + ex.Message);
                return false;
            }
        }



        //Actualiza los datos de la categoria en la base de datos
        [HttpPut]
        [Route("ActualizarCategoria")]
        public long ActualizarCategoria(Categorias categoria)
        {
            try
            {
                using (var context = new db_aa61bd_impomyuEntities())
                {
                    var datos = context.Categorias.Where(x => x.ID_Categoria == categoria.ID_Categoria).FirstOrDefault();

                    if (datos != null)
                    {
                        var categoriaExistente = context.Categorias.FirstOrDefault(c => c.Nombre_Categoria == categoria.Nombre_Categoria);

                        // Si la categoría no existe, la registramos
                        if (categoriaExistente == null)
                        {
                            datos.Nombre_Categoria = categoria.Nombre_Categoria;

                            context.SaveChanges();
                            logExitos.Add("RegistrarCategoria", $"Registro de categoría '{categoria.Nombre_Categoria}' realizado exitosamente");
                            logExitos.Add("ActualizarCategoria", "Actualización exitosa de la categoría con ID: " + categoria.ID_Categoria);

                            return categoria.ID_Categoria;
                        }
                        else
                        {
                            // Devolvemos vacío si la categoría ya existe
                            return 0;
                        }

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
                using (var context = new db_aa61bd_impomyuEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var categoria = (from x in context.Categorias
                                     where x.ID_Categoria == q
                                     select x).FirstOrDefault();

                    if (categoria != null)
                    {
                        logExitos.Add("ConsultaCategoriaEspecifica", $"Consulta exitosa de la categoría con ID: {q}");
                    }
                    else
                    {
                        logExitos.Add("ConsultaCategoriaEspecifica", $"No se encontró la categoría con ID: {q}");
                    }

                    return categoria;
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
