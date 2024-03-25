using System;
using System.Web;
using System.Web.Mvc;

namespace WEB_ImpoMoyaUlate.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizeRol : AuthorizeAttribute
    {
        private int Rol;

        public AuthorizeRol(int Rol = 1)
        {
            this.Rol = Rol;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                int? idRol = HttpContext.Current.Session["Rol"] as int?;

                // Verifica si el usuario tiene el idRol permitido
                if (idRol != null && idRol == 1) // Solo permitir el acceso si el rol es 1
                {
                    return;
                }
                else
                {
                    // Si el usuario no tiene acceso o el rol no es 1, redirige a una página indicando que no tiene permiso
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
