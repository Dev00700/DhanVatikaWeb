using Microsoft.AspNetCore.Mvc;

namespace DhanVatikaWeb.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
