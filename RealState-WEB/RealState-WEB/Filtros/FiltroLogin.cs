using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RealState_WEB.Filtros
{
    public class FiltroLoginIActionFilter : Attribute 
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Session.GetInt32("_id") == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Home" },
                    { "action", "Index" }
                });
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
