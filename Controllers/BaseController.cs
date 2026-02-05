using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DhanVatikaWeb.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            var session = httpContextAccessor.HttpContext.Session;
            var customerGuid = session.GetString("CustomerGuid");

            if (string.IsNullOrEmpty(customerGuid))
            {
                httpContextAccessor.HttpContext.Response.Redirect("/Login");
            }
        }
    }
}
