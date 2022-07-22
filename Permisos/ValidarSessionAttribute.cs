using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AeroSpace.Permisos
{
    public class ValidarSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            

            //if (HttpContext.Current == null)
            //{
            //    filterContext.Result = new RedirectResult("~/Acceso/Login");
            //}
            base.OnActionExecuting(filterContext);    
        }
    }
}
