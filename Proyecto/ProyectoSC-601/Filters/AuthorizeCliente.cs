using System;
using System.Web;
using System.Web.Mvc;

namespace WEB_ImpoMoyaUlate.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizeCliente : AuthorizeAttribute
    {
        private int Rol;

        public AuthorizeCliente(int Rol = 2)
        {
            this.Rol = Rol;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                int? idRol = HttpContext.Current.Session["Rol"] as int?;

                // Verifica si el usuario tiene el idRol permitido
                if (idRol != null && idRol == 2) // Solo permitir el acceso si el rol es 2
                {
                    return;
                }
                else
                {
                    // Si el usuario no tiene acceso o el rol no es 2, redirige a una página indicando que no tiene permiso
                    filterContext.Result = new RedirectResult("~/Seguridad/NoAcceso");
                }
            }
            catch (Exception)
            {
                // Maneja cualquier excepción y redirige a una página de error
                filterContext.Result = new RedirectResult("~/Seguridad/NoAcceso");
            }
        }
    }
}

