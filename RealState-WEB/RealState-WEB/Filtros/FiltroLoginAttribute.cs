using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RealState_WEB.Filtros
{
    public class FiltroLoginAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var idRol = context.HttpContext.Session.GetInt32("_idRol");

            if (idRol == null)
            {
                idRol = 0;
            }

            if (idRol == 0)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Home" },
                    { "action", "IniciarSesion" }
                });
            }
        }
    }
}
