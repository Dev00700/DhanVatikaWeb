using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DhanVatikaWeb.Models
{
    public class CustomerFirstLoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session.GetString("IsFirstLogin");

            if (session == "True" || session=="1")
            {
                var path = context.HttpContext.Request.Path.Value.ToLower();

                if (!path.Contains("/ChangePassword"))
                {
                    context.Result = new RedirectResult("/ChangePassword");
                    return;
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
