using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GuanaHospi.Utilidades
{
    public class ValidarSesionAttribute : ActionFilterAttribute
    {
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.Session.GetInt32("Usuario");
            if (user == null)
            {
                context.Result = new RedirectResult("/Auth/Login");
            }
            base.OnActionExecuting(context);
        }
    }
}
