using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RealState_WEB.Filtros
{
    public class FiltroAdminAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var idRodl = context.HttpContext.Session.GetInt32("_idRol");

            if (idRodl == null)
            {
                idRodl = 0;
            }

            if (idRodl != 2)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Home" },
                    { "action", "Index" }
                });
            }
        }
    }
}
